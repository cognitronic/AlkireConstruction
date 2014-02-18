using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAM.Infrastructure.Authentication;
using RAM.Core.Domain.User;
using RAM.Services.Implementations;
using RAM.Services.Interfaces;
using StructureMap;
using RAM.Core.Domain.Blog;
using RAM.Core.Domain.Project;
using StructureMap.Configuration.DSL;
using RAM.Infrastructure.Configuration;
using RAM.Infrastructure.Logging;
using RAM.Infrastructure.Email;
using RAM.Controllers.ActionArguments;
using RAM.Infrastructure.UnitOfWork;
using RAM.Repository.NHibernate;
using RAM.Services.Cache;
using System.Web.Security;
using RAM.Web.Security;
using RAM.Repository.NHibernate.Repositories;

namespace RAM.MVC
{
    public class BootStrapper
    {
        public static void ConfigureStructureMap()
        {
            ObjectFactory.Initialize(x => { x.AddRegistry<ApplicationSettingsRegistry>(); });
            ApplicationSettingsFactory.
                InitializeApplicationSettingsFactory
                                  (ObjectFactory.GetInstance<IApplicationSettings>());

            ObjectFactory.Initialize(x => { x.AddRegistry<ModelRegistry>(); });
        }

        public class ModelRegistry : Registry
        {
            public ModelRegistry()
            {
                if (ApplicationSettingsFactory.
                    GetApplicationSettings().
                    PersistenceStrategy.Equals(RAM.Infrastructure.Domain.PersistenceStrategy.NHibernate.ToString()))
                {
                    //Repositories
                    For<IUserRepository>().Use<UserRepository>();
                    For<IBlogRepository>().Use<BlogRepository>();
                    For<IProjectRepository>().Use<ProjectRepository>();
                    For<IBlogCategoryRepository>().Use<BlogCategoryRepository>();

                    For<IUnitOfWork>().Use<NHUnitOfWork>();

                    For<ICacheStorage>().Use<CouchbaseCacheAdapter>();
                }

                //For<MembershipProvider>().Use<AlpineMembershipProvider>();

                //For<RoleProvider>().Use<AlpineRoleProvider>();

                //Models
                For<IUser>().Use<User>();
                For<IBlog>().Use<Blog>();
                For<IProject>().Use<Project>();
                For<IBlogCategory>().Use<BlogCategory>();

                //Services
                For<IUserService>().Use<UserService>();
                For<IBlogService>().Use<BlogService>();
                For<IProjectService>().Use<ProjectService>();
                For<IBlogCategoryService>().Use<BlogCategoryService>();

                // Logger
                For<ILogger>().Use
                          <Log4NetAdapter>();
                // Email Service                 
                For<IEmailService>().Use
                        <SMTPService>();
                //Authentication
                For<IExternalAuthenticationService>().Use<JanrainAuthenticationService>();
                For<IFormsAuthentication>().Use<AspFormsAuthentication>();
                For<ILocalAuthenticationService>().Use<HRRMarketingAuthenticationService>();
                //Controller Helpers
                For<IActionArguments>().Use<HttpRequestActionArguments>();
            }
        }

        public class ApplicationSettingsRegistry : Registry
        {
            public ApplicationSettingsRegistry()
            {
                // Application Settings                 
                For<IApplicationSettings>().Use
                         <WebConfigApplicationSettings>();
            }
        }

        public static IContainer ConfigureStructureMapWebAPI()
        {
            ObjectFactory.Initialize(x => { x.AddRegistry<ApplicationSettingsRegistry>(); });
            ApplicationSettingsFactory.
                InitializeApplicationSettingsFactory
                                  (ObjectFactory.GetInstance<IApplicationSettings>());
            if (ApplicationSettingsFactory.
                    GetApplicationSettings().
                    PersistenceStrategy.Equals(RAM.Infrastructure.Domain.PersistenceStrategy.NHibernate.ToString()))
            {
                var container = new Container(x =>
                {
                    x.For<IUserService>().Use<UserService>();
                    x.For<IBlogService>().Use<BlogService>();
                    x.For<IProjectService>().Use<ProjectService>();
                    x.For<IBlogCategoryService>().Use<BlogCategoryService>();
                    x.For<IUserRepository>().Use<UserRepository>();


                    x.For<IBlogRepository>().Use<BlogRepository>();
                    x.For<IProjectRepository>().Use<ProjectRepository>();
                    x.For<IBlogCategoryRepository>().Use<BlogCategoryRepository>();

                    x.For<IUnitOfWork>().Use<NHUnitOfWork>();

                    x.For<ICacheStorage>().Use<CouchbaseCacheAdapter>();

                    x.For<IUser>().Use<User>();
                    x.For<IBlog>().Use<Blog>();
                    x.For<IProject>().Use<Project>();
                    x.For<IBlogCategory>().Use<BlogCategory>();

                    x.For<ILogger>().Use<Log4NetAdapter>();

                    x.For<IEmailService>().Use<SMTPService>();

                    x.For<IExternalAuthenticationService>().Use<JanrainAuthenticationService>();
                    x.For<IFormsAuthentication>().Use<AspFormsAuthentication>();
                    x.For<ILocalAuthenticationService>().Use<HRRMarketingAuthenticationService>();

                    x.For<IActionArguments>().Use<HttpRequestActionArguments>();
                });
                return container;
            }
            return null;
        }
    }
}