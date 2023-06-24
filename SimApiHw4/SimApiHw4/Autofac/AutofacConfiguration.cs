using Autofac;
using AutoMapper;
using SimApiHw4.Data;
using SimApiHw4.Operation;
using SimApiHw4.Schema;

namespace SimApiHw4.Service;

public class AutofacConfiguration : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DapperCategoryService>().As<IDapperCategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        #region DapperDbContext 
        builder.RegisterType<DapperDbContext>()
            .AsSelf()
            .InstancePerLifetimeScope();
        #endregion
        #region Mapper
        builder.Register(context =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperInfo());
            });

            return config.CreateMapper();
        })
       .As<IMapper>()
       .InstancePerLifetimeScope();
        #endregion
    }

}

