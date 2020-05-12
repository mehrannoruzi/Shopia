using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using System.Text;
using Shopia.Domain;
using Shopia.InfraStructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopia.Crawler.Service.Resource;
using Shopia.Crawler.DataAccess.Dapper;
using System.Net;

namespace Shopia.Crawler.Service
{
    public class CrawlerService : ICrawlerService
    {
        private InstagramSetting _instagramSetting;
        private CrawlerUnitOfWork _crawlerUnitOfWork;

        public CrawlerService(InstagramSetting instagramSetting, CrawlerUnitOfWork crawlerUnitOfWork)
        {
            _instagramSetting = instagramSetting;
            _crawlerUnitOfWork = crawlerUnitOfWork;
        }


        private async Task<CrawledPageDto> CrawlPageFromInstagramAsync(string username)
        {
            #region Call Instagram Page Api
            var requestUrl = _instagramSetting.PageUrlPattern.Fill(username);

            //var xxx = new WebClient();
            //xxx.UseDefaultCredentials = true;
            //var xxx1 = xxx.DownloadString(requestUrl);

            //var xxx2 = new WebClient();
            //var yyy = new System.Collections.Specialized.NameValueCollection();
            //yyy.Add("__a", "1");
            //xxx2.BaseAddress = "https://www.instagram.com/Alikarimiiiiiiii8";
            //xxx2.QueryString = yyy;
            //var xxx3 = xxx2.DownloadString(requestUrl);

            var requestResult = await HttpRequestTools.GetAsync(requestUrl, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(requestResult)) return null;
            #endregion

            dynamic store = requestResult.DeSerializeJson<dynamic>();
            return new CrawledPageDto
            {
                #region Set Page Property
                Username = username,
                UniqueId = Parser.ToString(store.graphql.user.id),
                FullName = Parser.ToString(store.graphql.user.full_name),

                Bio = Parser.ToString(store.graphql.user.biography),
                BioUrl = Parser.ToString(store.graphql.user.external_url),
                ProfilePictureUrl = Parser.ToString(store.graphql.user.profile_pic_url_hd),

                IsPrivate = Parser.ToBool(store.graphql.user.is_private),
                IsVerified = Parser.ToBool(store.graphql.user.is_verified),
                IsBlocked = Parser.ToBool(store.graphql.user.blocked_by_viewer),
                IsBusinessAccount = Parser.ToBool(store.graphql.user.is_business_account),

                FolowingCount = Parser.ToInt(store.graphql.user.edge_follow.count),
                FolowerCount = Parser.ToInt(store.graphql.user.edge_followed_by.count),
                PostCount = Parser.ToInt(store.graphql.user.edge_owner_to_timeline_media.count),

                LastCrawlDate = DateTime.Now
                #endregion
            };
        }

        private async Task<InstagramPostInquiry> CrawlPostFromInstagramAsync(string UniqueId, int pageSize, string cursor)
        {
            var requestUrl = _instagramSetting.PostUrlPattern.Fill(_instagramSetting.QueryHash,
                        new { id = UniqueId, first = pageSize, after = cursor }.SerializeToJson().ToEncodedUrl());
            return await HttpRequestTools.GetAsync<InstagramPostInquiry>(requestUrl, Encoding.UTF8);
        }

        private CrawledPostDto ConvertToCrawledPostDto(edges post)
        {
            var newPost = new CrawledPostDto();

            switch (post.node.__typename)
            {
                case "GraphImage":
                    {
                        newPost.Type = FileType.Image;
                        newPost.FileUrl = post.node.display_url;
                        newPost.ThumbnailUrl = post.node.thumbnail_src;
                        newPost.Dimension = post.node.dimensions.width + "*" + post.node.dimensions.height;

                        break;
                    }
                case "GraphVideo":
                    {
                        newPost.Type = FileType.Video;
                        newPost.FileUrl = post.node.video_url;
                        newPost.ThumbnailUrl = post.node.thumbnail_src;
                        newPost.ViewCount = post.node.video_view_count;
                        newPost.Dimension = post.node.dimensions.width + "*" + post.node.dimensions.height;

                        break;
                    }
                case "GraphSidecar":
                    {
                        newPost.IsAlbum = true;
                        newPost.Type = FileType.Unknown;
                        newPost.Items = new List<CrawledPostDto>();

                        foreach (var item in post.node.edge_sidecar_to_children.edges)
                        {
                            #region Set PostItem Property
                            var newPostItem = new CrawledPostDto();
                            switch (item.node.__typename)
                            {
                                case "GraphImage":
                                    {
                                        newPostItem.Type = FileType.Image;
                                        newPostItem.FileUrl = item.node.display_url;
                                        newPostItem.ThumbnailUrl = item.node.thumbnail_src;
                                        newPostItem.Dimension = item.node.dimensions.width + "*" + item.node.dimensions.height;

                                        break;
                                    }
                                case "GraphVideo":
                                    {
                                        newPostItem.Type = FileType.Video;
                                        newPostItem.FileUrl = item.node.video_url;
                                        newPostItem.ThumbnailUrl = item.node.display_url;
                                        newPostItem.ViewCount = item.node.video_view_count;
                                        newPostItem.Dimension = item.node.dimensions.width + "*" + item.node.dimensions.height;

                                        break;
                                    }
                            }
                            newPostItem.UniqueId = item.node.id;
                            newPost.Items.Add(newPostItem);
                            #endregion
                        }

                        break;
                    }
            }

            newPost.UniqueId = post.node.id;
            newPost.LikeCount = post.node.edge_Media_Preview_Like.count;
            newPost.CommentCount = post.node.edge_Media_To_Comment.count;
            newPost.CreateDateMi = Parser.ToDateTime(double.Parse(post.node.taken_at_timestamp));
            newPost.Description = post.node.edge_Media_To_Caption.edges.Count == 0 ? string.Empty : post.node.edge_Media_To_Caption.edges[0].node.text;

            return newPost;
        }

        private async Task<IResponse<bool>> CrawlNewPostAsync(IPostRepo postRepo, Page page, int newPostCount)
        {
            try
            {
                var postCount = 0;
                var haveNextPage = false;
                var cursor = string.Empty;
                var postList = new List<CrawledPostDto>();

                do
                {
                    #region Call Instagram Post Api
                    var postInquiry = await CrawlPostFromInstagramAsync(page.UniqueId, newPostCount, cursor);
                    if (postInquiry.status != "ok") return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.Error };

                    var postCollection = postInquiry.data.user.edge_Owner_To_Timeline_Media.edges;
                    var totalPostCount = postInquiry.data.user.edge_Owner_To_Timeline_Media.count;
                    cursor = postInquiry.data.user.edge_Owner_To_Timeline_Media.page_Info.end_cursor;
                    haveNextPage = postInquiry.data.user.edge_Owner_To_Timeline_Media.page_Info.has_next_page;
                    #endregion

                    foreach (var post in postCollection)
                    {
                        postCount += 1;
                        var newPost = ConvertToCrawledPostDto(post);

                        newPost.PageId = page.PageId;
                        await postRepo.AddAsync(newPost);
                    }

                } while (haveNextPage == true && postCount < newPostCount);

                return new Response<bool> { Result = true, IsSuccessful = true, Message = ServiceMessage.Success };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.Exception };
            }
        }


        public async Task<IResponse<CrawledPageDto>> CrawlPageAsync(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username)) return new Response<CrawledPageDto> { IsSuccessful = false, Message = ServiceMessage.InvalidPageId };

                var page = await CrawlPageFromInstagramAsync(username);
                if (page.IsNull()) return new Response<CrawledPageDto> { IsSuccessful = false, Message = ServiceMessage.InvalidPageId };

                var saveResult = await _crawlerUnitOfWork.PageRepo.AddAsync(page);
                return new Response<CrawledPageDto>
                {
                    Result = saveResult ? page : null,
                    IsSuccessful = saveResult,
                    Message = saveResult ? ServiceMessage.Success : ServiceMessage.Error
                };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<CrawledPageDto> { IsSuccessful = false, Message = ServiceMessage.Exception };
            }
        }

        public async Task<IResponse<bool>> CrawlPostAsync(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username)) return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.InvalidPageId };

                #region Get Page Info
                var page = await _crawlerUnitOfWork.PageRepo.FindAsync(username);
                if (page.IsNull()) return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.RecordNotExist };

                var postCount = 0;
                var haveNextPage = false;
                var cursor = string.Empty;
                var postList = new List<CrawledPostDto>();
                #endregion

                do
                {
                    #region Call Instagram Post Api
                    var postInquiry = await CrawlPostFromInstagramAsync(page.UniqueId, _instagramSetting.CrawledPostPageSize, cursor);
                    if (postInquiry.status != "ok") return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.Error };

                    var postCollection = postInquiry.data.user.edge_Owner_To_Timeline_Media.edges;
                    var totalPostCount = postInquiry.data.user.edge_Owner_To_Timeline_Media.count;
                    cursor = postInquiry.data.user.edge_Owner_To_Timeline_Media.page_Info.end_cursor;
                    haveNextPage = postInquiry.data.user.edge_Owner_To_Timeline_Media.page_Info.has_next_page;
                    #endregion

                    foreach (var post in postCollection)
                    {
                        postCount += 1;
                        var newPost = ConvertToCrawledPostDto(post);

                        newPost.PageId = page.PageId;
                        await _crawlerUnitOfWork.PostRepo.AddAsync(newPost);
                    }

                } while (haveNextPage == true && postCount < _instagramSetting.MaxCrawledPost);

                return new Response<bool> { Result = true, IsSuccessful = true, Message = ServiceMessage.Success };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<bool> { IsSuccessful = false, Message = ServiceMessage.Exception };
            }
        }

        public async Task<IResponse<IEnumerable<Post>>> GetPostAsync(string username, PagingParameter pagingParameter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username)) return new Response<IEnumerable<Post>> { IsSuccessful = false, Message = ServiceMessage.InvalidPageId };

                var posts = await _crawlerUnitOfWork.PostRepo.GetAsync(username, pagingParameter);
                if (posts.IsNull()) return new Response<IEnumerable<Post>> { IsSuccessful = false, Message = ServiceMessage.RecordNotExist };

                return new Response<IEnumerable<Post>>
                {
                    Result = posts,
                    IsSuccessful = true,
                    Message = ServiceMessage.Success
                };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<IEnumerable<Post>> { IsSuccessful = false, Message = ServiceMessage.Exception };
            }
        }

        public async Task UpdatePageAndCrawlNewPostAsync()
        {
            try
            {
                var pageNumber = 0;
                IEnumerable<Page> pages;
                var pageRepo = _crawlerUnitOfWork.PageRepo;
                var postRepo = _crawlerUnitOfWork.PostRepo;

                do
                {
                    pageNumber += 1;
                    pages = await pageRepo.GetAsync(DateTime.Now.Date,
                        new PagingParameter { PageNumber = pageNumber, PageSize = _instagramSetting.CrawledPostPageSize });

                    foreach (var page in pages)
                    {
                        var crawledpage = await CrawlPageFromInstagramAsync(page.Username);
                        if (crawledpage.IsNull()) continue;

                        crawledpage.PageId = page.PageId;
                        await pageRepo.UpdateAsync(crawledpage);
                        if (page.PostCount < crawledpage.PostCount)
                            await CrawlNewPostAsync(postRepo, page, crawledpage.PostCount - page.PostCount);
                    }
                } while (pages.Count() > 0);

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                await Task.FromException(e);
            }
        }
    }
}
