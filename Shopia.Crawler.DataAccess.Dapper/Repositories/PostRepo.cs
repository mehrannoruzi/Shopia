using System;
using Elk.Core;
using Elk.Dapper;
using System.Linq;
using Shopia.Domain;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Shopia.Crawler.DataAccess.Dapper
{
    public class PostRepo : IPostRepo
    {
        private SqlConnection _sqlConnection;

        public PostRepo(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("CrawlerDbContext"));
        }


        public async Task<bool> AddAsync(CrawledPostDto model)
        {
            _sqlConnection.Open();
            var transaction = await _sqlConnection.BeginTransactionAsync();
            try
            {
                var post = new Post();
                post.UpdateWith(model);
                post.InsertDateMi = DateTime.Now;
                post.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                var postId = _sqlConnection.ExecuteSpCommand<int>("[Instagram].[InsertPost]",
                    new { Post = post.ToTableValuedParameter("[dbo].[Tvp_Post]") }, transaction).FirstOrDefault();
                if (postId <= 0) return false;

                if (!model.IsAlbum)
                {
                    var postAsset = new PostAsset();
                    postAsset.UpdateWith(model);
                    postAsset.PostId = postId;
                    postAsset.InsertDateMi = DateTime.Now;
                    postAsset.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                    await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[InsertPostAsset]",
                    new { PostAsset = postAsset.ToTableValuedParameter("[dbo].[Tvp_PostAsset]") }, transaction);
                }
                else
                {
                    var postAssetList = new List<PostAsset>();
                    foreach (var item in model.Items)
                    {
                        var postAsset = new PostAsset();
                        postAsset.UpdateWith(item);
                        postAsset.PostId = postId;
                        postAsset.InsertDateMi = DateTime.Now;
                        postAsset.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                        postAssetList.Add(postAsset);
                    }

                    await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[InsertPostAsset]",
                    new { PostAsset = postAssetList.ToTableValuedParameter("[dbo].[Tvp_PostAsset]") }, transaction);
                }

                transaction.Commit();
                _sqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                transaction.Rollback();
                _sqlConnection.Close();

                if (e.Message.Contains("unique index")) return true;
                return false;
            }
        }

        public async Task<IEnumerable<Post>> GetAsync(string username, PagingParameter pagingParameter)
        {
            try
            {
                var query = "SELECT		po.[PostId],[ViewCount],[LikeCount],[CommentCount],[IsAlbum],[CreateDateMi],po.[UniqueId]," +
                            "PostAssets =	(SELECT	[Type],[Dimension],[UniqueId],[FileUrl],[ThumbnailUrl]" +
                            "				FROM	[Instagram].[PostAsset]" +
                            "				WHERE	PostId = po.PostId" +
                            "				For JSON AUTO) " +
                            "FROM		[Instagram].[Page] p " +
                            "INNER JOIN	[Instagram].[Post] po	ON	p.PageId = po.PageId " +
                            "WHERE		p.Username = @Username " +
                            "ORDER BY	po.PostId DESC " +
                            "OFFSET		@PageSize * (@PageNumber - 1) ROWS " +
                            "FETCH NEXT	@PageSize ROWS ONLY;";
                return await _sqlConnection.ExecuteQueryAsync<Post>(query, 
                    new { Username = username, pagingParameter.PageNumber, pagingParameter.PageSize });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return null;
            }
        }
    }
}
