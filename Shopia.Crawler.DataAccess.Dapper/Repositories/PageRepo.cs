using System;
using Elk.Core;
using Elk.Dapper;
using Shopia.Domain;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Shopia.Crawler.DataAccess.Dapper
{
    public class PageRepo : IPageRepo
    {
        private SqlConnection _sqlConnection;

        public PageRepo(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("CrawlerDbContext"));
        }


        public async Task<bool> AddAsync(CrawledPageDto model)
        {
            try
            {
                var instagramPage = new Page();
                instagramPage.UpdateWith(model);
                instagramPage.InsertDateMi = instagramPage.ModifyDateMi = DateTime.Now;
                instagramPage.InsertDateSh = instagramPage.ModifyDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[InsertPage]",
                    new { Page = instagramPage.ToTableValuedParameter("[dbo].[Tvp_Page]") });

                return true;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                if (e.Message.Contains("unique index")) return true;
                else return false;
            }
        }

        public async Task<bool> UpdateAsync(CrawledPageDto model)
        {
            try
            {
                var instagramPage = new Page();
                instagramPage.UpdateWith(model);
                instagramPage.ModifyDateMi = DateTime.Now;
                instagramPage.ModifyDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[UpdatePage]",
                    new { Page = instagramPage.ToTableValuedParameter("[dbo].[Tvp_Page]") });

                return true;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string pageId)
        {
            try
            {
                var query = "DELETE [Instagram].[Page] " +
                            "WHERE  [Username] = @Username";
                return await _sqlConnection.ExecuteQueryCommandAsync(query, new { Username = pageId });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return false;
            }
        }

        public async Task<Page> FindAsync(string pageId)
        {
            try
            {
                var query = "SELECT * " +
                            "FROM   [Instagram].[Page] " +
                            "WHERE  [Username] = @Username";
                return await _sqlConnection.ExecuteQuerySingleAsync<Page>(query, new { Username = pageId });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return null;
            }
        }

        public async Task<IEnumerable<Page>> GetAsync(DateTime lastCrawlDate, PagingParameter pagingParameter)
        {
            try
            {
                var query = "SELECT		* " +
                            "FROM		[Instagram].[Page] p " +
                            "WHERE      p.ModifyDateMi <= @lastCrawlDate " +
                            "ORDER BY	p.PageId ASC " +
                            "OFFSET		@PageSize * (@PageNumber - 1) ROWS " +
                            "FETCH NEXT	@PageSize ROWS ONLY;";
                return await _sqlConnection.ExecuteQueryAsync<Page>(query,
                    new { lastCrawlDate = lastCrawlDate, pagingParameter.PageNumber, pagingParameter.PageSize });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return null;
            }
        }
    }
}
