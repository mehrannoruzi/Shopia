using System;
using Elk.Core;
using Elk.Dapper;
using System.Linq;
using Shopia.Domain;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Notifier.DataAccess.Dapper
{
    public static class ParameterExtension
    {
        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="parameter">list of values</param>
        /// <param name="typeName">database type name</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter ToTableValuedParameter2<T>
            (this List<T> parameter, string typeName)
        {
            var dataTable = new DataTable();
            var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.CanWrite &&
                    (x.PropertyType.FullName.StartsWith("Elk.") ||
                   (x.PropertyType.FullName.StartsWith("System.")
                   && (!x.PropertyType.FullName.Contains("System.Collection"))
                   && x.GetCustomAttribute(typeof(NotMappedAttribute)) == null))).ToArray();

            foreach (var field in fields)
            {
                if (field.PropertyType.IsEnum)
                {
                    dataTable.Columns.Add(field.Name, typeof(byte));
                }
                else
                    dataTable.Columns.Add(field.Name, field.PropertyType);
            }

            foreach (T obj in parameter)
                dataTable.Rows.Add(fields.Select(x => x.GetValue(obj, null)).ToArray());

            return dataTable.AsTableValuedParameter(typeName);
        }

        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="parameter">list of values</param>
        /// <param name="typeName">database type name</param>
        /// <param name="columnNames">if more than one column in a TVP, 
        /// columns order must mtach order of columns in TVP</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter ToTableValuedParameter2<T>
            (this T parameter, string typeName, string columnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType)// || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(columnNames == null ? "NONAME" : columnNames, typeof(T));
                dataTable.Rows.Add(parameter);
            }
            else
            {
                var assemblyName = typeof(T).Assembly.FullName.Split(',')[0];
                var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.CanWrite &&
                    (x.PropertyType.FullName.StartsWith("Elk.") ||
                    x.PropertyType.FullName.StartsWith(assemblyName) ||
                    (x.PropertyType.FullName.StartsWith("System.")
                    && (!x.PropertyType.FullName.Contains("System.Collection"))
                    && x.GetCustomAttribute(typeof(NotMappedAttribute)) == null))).ToArray();

                foreach (var field in fields)
                {
                    if (field.PropertyType.IsEnum)
                        if (field.PropertyType.IsInheritFrom(typeof(byte))) dataTable.Columns.Add(field.Name, typeof(byte));
                        else dataTable.Columns.Add(field.Name, typeof(int));
                    else
                        dataTable.Columns.Add(field.Name, field.PropertyType);
                }

                dataTable.Rows.Add(fields.Select(x => x.GetValue(parameter, null)).ToArray());
            }

            return dataTable.AsTableValuedParameter(typeName);
        }
    }

    public class NotificationRepo : INotificationRepo
    {
        private SqlConnection _sqlConnection;

        public NotificationRepo(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("NotifierDbContext"));
        }


        public async Task<bool> AddAsync(Notification model)
        {
            try
            {
                model.InsertDateMi = DateTime.Now;
                model.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);

                await _sqlConnection.ExecuteSpCommandAsync<int>("[Notifier].[InsertNotification]",
                    new { Notification = model.ToTableValuedParameter("[dbo].[Tvp_Notification]") });

                return true;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                if (e.Message.Contains("unique index")) return true;
                else return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateNotificationDto model)
        {
            try
            {
                await _sqlConnection.ExecuteSpCommandAsync<int>("[Notifier].[UpdateNotification]",
                    new { NotificationId = model.NotificationId, Status = model.Status, SendStatus = model.SendStatus, SendDateMi = model.SendDateMi });

                return true;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return false;
            }
        }

        public async Task<Notification> FindAsync(int notificationId)
        {
            try
            {
                var query = "SELECT * " +
                            "FROM   [Notifier].[Notification] " +
                            "WHERE  [NotificationId] = @NotificationId";
                return await _sqlConnection.ExecuteQuerySingleAsync<Notification>(query, new { NotificationId = notificationId });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return null;
            }
        }

        public async Task<IEnumerable<Notification>> GetAsync(NotificationStatus status, PagingParameter pagingParameter)
        {
            try
            {
                var query = "SELECT		* " +
                            "FROM		[Notifier].[Notification] n " +
                            "WHERE      n.Status = @Status " +
                            "ORDER BY	p.NotificationId ASC " +
                            "OFFSET		@PageSize * (@PageNumber - 1) ROWS " +
                            "FETCH NEXT	@PageSize ROWS ONLY;";
                return await _sqlConnection.ExecuteQueryAsync<Notification>(query,
                    new { Status = status, pagingParameter.PageNumber, pagingParameter.PageSize });
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return null;
            }
        }

        public async Task<IEnumerable<Notification>> GetUnProccessAsync()
        {
            try
            {
                var notifications = await _sqlConnection.ExecuteSpCommandAsync<Notification>("[Notifier].[GetUnProccessNotification]");

                if (notifications.Any()) return notifications;
                return new List<Notification>();
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                return new List<Notification>();
            }
        }

    }
}
