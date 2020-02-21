using Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DomainDatabaseEF.Extensions
{
    public static partial class EFExtensions
    {
        public static Tuple<string, string> GetTableName<T>(this DbSet<T> dbSet) where T : class, IDataBaseEntity
        {
            var context = dbSet.GetDbContext();

            var result = context.GetTableName<T>();

            return new Tuple<string, string>(result.Item1, result.Item2);
        }

        public static Tuple<string, string> GetTableName<T>(this DbContext context) where T : class, IDataBaseEntity
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();

            return new Tuple<string, string>(schema, tableName);
        }
    }
}
