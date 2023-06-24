using Dapper;
using SimApiHw4.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Data;

public class DapperRepository<Entity> : IDapperRepository<Entity> where Entity : BaseModel
{
    protected readonly DapperDbContext dbContext;
    private bool disposed;

    public DapperRepository(DapperDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Entity> GetAll()
    {
        using (var connection = dbContext.CreateConnection())
        {
            connection.Open();
            var tableName = typeof(Entity).Name;
            var sql = $"SELECT * FROM dbo.\"{tableName}\"";
            var result = connection.Query<Entity>(sql);
            return result.ToList();
        }
    }
    #region Insert
    public async Task InsertAsync(Entity t)
    {
        var insertQuery = GenerateInsertQuery();

        using (var connection = dbContext.CreateConnection())
        {
            await connection.ExecuteAsync(insertQuery, t);
        }
    }
    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {
        return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                select prop.Name).ToList();
    }
    private IEnumerable<PropertyInfo> GetProperties => typeof(Entity).GetProperties();
    private string GenerateInsertQuery()
    {
        var tableName = typeof(Entity).Name;
        var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

        insertQuery.Append("(");

        var properties = GenerateListOfProperties(GetProperties);
        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") VALUES (");

        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(")");

        return insertQuery.ToString();
    }
    #endregion
    #region Update
    public async Task UpdateAsync(Entity t)
    {
        var updateQuery = GenerateUpdateQuery();

        using (var connection = dbContext.CreateConnection())
        {
            await connection.ExecuteAsync(updateQuery, t);
        }
    }

    private string GenerateUpdateQuery()
    {
        var tableName = typeof(Entity).Name;
        var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
        var properties = GenerateListOfProperties(GetProperties);

        properties.ForEach(property =>
        {
            if (!property.Equals("Id"))
            {
                updateQuery.Append($"{property}=@{property},");
            }
        });

        updateQuery.Remove(updateQuery.Length - 1, 1);
        updateQuery.Append(" WHERE Id=@Id");

        return updateQuery.ToString();
    }
    #endregion Update

    public async Task DeleteAsync(int id)
    {
        var tableName = typeof(Entity).Name;
        using (var connection = dbContext.CreateConnection())
        {
            await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id=@Id", new { Id = id });
        }
    }

    public async Task<Entity> GetByIdAsync(int id)
    {
        using (var connection = dbContext.CreateConnection())
        {
            var tableName = typeof(Entity).Name;
            var result = await connection.QuerySingleOrDefaultAsync<Entity>($"SELECT * FROM {tableName} WHERE Id=@Id", new { Id = id });
            
            if (result == null)
                throw new KeyNotFoundException($"{tableName} with id [{id}] could not be found.");
            
            return result;
        }
    }
}

