using Dapper;
using Infrastructure.Data.Abstract;
using Infrastructure.Entity;
using Infrastructure.Enumarations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Concrete.Dapper
{
    public class DapperRepositoryBase<T> : IRepository<T>
        where T : BaseEntity, new()
    {

        IDbConnection _connection;

        readonly string connectionString = "server=LAB202-OGRETMEN;database=SparkMarketDB;trusted_connection=true;TrustServerCertificate=true;";   //YOUR_SQL_CONNECTION_STRING

        public DapperRepositoryBase()
        {
            _connection = new SqlConnection(connectionString);
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllByActive(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Active = true, bool Tracking = false, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public PagingResult<T> GetAllPaging(int Page, int PageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public T GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public int GetCount(Expression<Func<T, bool>> filter = null, params string[] includelist)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            int rowsEffected = 0;
            try
            {
                string tableName = GetTableName();
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                rowsEffected = _connection.Execute(query, entity);
            }
            catch (Exception ex) { }

            return entity;
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        private string GetTableName()
        {
            string tableName = "";
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                tableName = tableAttr.Name;
                return tableName;
            }

            return type.Name;
        }
        public static string GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                if (keyAttributes != null && keyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute.Name;
                    }
                    else
                    {
                        return property.Name;
                    }
                }
            }

            return null;
        }


        private string GetColumns(bool excludeKey = false)
        {
            var type = typeof(T);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttr != null ? columnAttr.Name : p.Name;
                }));

            return columns;
        }

        protected string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            var values = string.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));

            return values;
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            return properties;
        }

        protected string GetKeyPropertyName()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (properties.Any())
            {
                return properties.FirstOrDefault().Name;
            }

            return null;
        }
    }
}
