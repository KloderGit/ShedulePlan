using Domain.Core.Interfaces;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DomainDatabaseEF.Extensions
{
    public static partial class EFExtensions
    {
        public static IQueryable<T> GetHierachy<T>(this DbSet<T> dataset) where T : class, INodeHub<T>, IDataBaseEntity
        {
            return dataset.GetHierachy<T>(null);
        }

        public static IQueryable<T> GetHierachy<T>(this DbSet<T> dataset, int? id) where T : class, INodeHub<T>, IDataBaseEntity
        {
            var context = dataset.GetDbContext<T>();

            var array = new List<int>();

            var command = CreateCommand(context);
            if (id.HasValue)
                command.Parameters.Add( AddParam( command.CreateParameter(), id.Value) );

            var tableNameOfDataSet = dataset.GetTableName();
            var queryFilter = GetFilterString(id);
            command.CommandText = CreateQueryString(tableNameOfDataSet, queryFilter);

            context.Database.OpenConnection();

            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    array.Add(reader.GetInt32(0));
                }
            }

            var result = context.Set<T>().Where(x => array.Contains(x.Id));

            return result;
        }

        private static DbCommand CreateCommand(DbContext context)
        {
            return context.Database.GetDbConnection().CreateCommand();
        }

        private static DbParameter AddParam(DbParameter parameter, int id)
        {
            parameter.ParameterName = "@RecordID";
            parameter.DbType = DbType.Int32;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = id;

            return parameter;
        }

        private static string CreateQueryString(Tuple<string, string> titles, string filter)
        {
            var schema = "\"" + titles.Item1 + "\"";
            var tableName = "\"" + titles.Item2 + "\"";

            return
                "WITH RECURSIVE BaseQuery AS (\n" +
                "	SELECT Layout.\"Id\"\n" +
                $"	FROM {schema}.{tableName} Layout \n" + filter +
                "	UNION\n" +
                "	SELECT RecursiveQuery.\"Id\"\n" +
                $"	FROM {schema}.{tableName} RecursiveQuery\n" +
                "	JOIN BaseQuery ON RecursiveQuery.\"ParentId\" = BaseQuery.\"Id\"\n" +
                ") \n" +
                "SELECT * FROM BaseQuery;";
        }

        private static string GetFilterString(int? id)
        {
            return id.HasValue ? $"	WHERE Layout.\"Id\" = @RecordID\n" :
                   $"	WHERE Layout.\"ParentId\" IS NULL\n";
        }
    }
}
