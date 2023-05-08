using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Dragonfly.Application.Contracts.Course;
using Dragonfly.Application.Contracts.Instructor;
using Dragonfly.Application.Course;
using Dragonfly.Application.Instructor;
using Dragonfly.Core.Configuration;
using Dragonfly.Core.Security;
using Dragonfly.Core.Security.Services;
using Dragonfly.Core.Sequencer;
using Dragonfly.DataAccess.Core;
using Dragonfly.DataAccess.EF;
using Dragonfly.DataAccess.EF.Base;
using Dragonfly.Domain.Contracts.Course;
using Dragonfly.Domain.Contracts.Instructor;
using Dragonfly.Domain.Model;
using Dragonfly.Domain.Services.Courses;
using Dragonfly.Domain.Services.Instructors;
using Dragonfly.UI.Mvc.Code;

namespace Dragonfly.UI.Mvc
{
    public class AutofacInitializer
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<OtpService>().As<IOtpService>();
            builder.RegisterType<Authentication>().As<IAuthentication>();
            builder.RegisterType<AuthenticationFactory>().As<IAuthenticationFactory>();
            //builder.RegisterType<SqlSequencer>().As<ISequencer>();
            //builder.RegisterType<SequencerFactory>().As<ISequencerFactory>();
            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<ApplicationConfigurator>().As<IConfigurator>();
            builder.RegisterType<ApplicationEnvironment>().As<IApplicationEnvironment>();
            builder.RegisterType<DataContext>().As<BaseContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<CourseDomain>().As<ICourseDomain>();
            builder.RegisterType<CourseAppService>().As<ICourseAppService>();
            builder.RegisterType<InstructorDomain>().As<IInstructorDomain>();
            builder.RegisterType<InstructorAppService>().As<IInstructorAppService>();

            builder.RegisterInstance(AutoMapperInitializer.Mapper).As<IMapper>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
