using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimApiHw4.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DapperDbContext dapperDbContext;
    private readonly string databaseType;
    private bool disposed;
   
    public UnitOfWork(DapperDbContext dapperDbContext, IConfiguration configuration)
    {
        this.dapperDbContext = dapperDbContext;
        this.databaseType = configuration.GetConnectionString("DbType");
        

    }

    public IDapperRepository<Entity> DapperRepository<Entity>() where Entity : BaseModel
    {
        if (databaseType == "PostgreSql")
        {
            return new DapperRepository<Entity>(dapperDbContext);
        }
        //this stage is deleted.
        throw new NotSupportedException("Database type not supported");
    }

}
