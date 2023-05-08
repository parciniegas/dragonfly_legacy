using AutoMapper;

namespace Dragonfly.UI.Mvc
{
    public class AutoMapperInitializer
    {
        private static MapperConfiguration _mapperConfiguration;

        public static void Configure()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CoursesMappingProfile());
            });

            Mapper = _mapperConfiguration.CreateMapper();
        }


        public static IMapper Mapper { get; private set; }
    }
}