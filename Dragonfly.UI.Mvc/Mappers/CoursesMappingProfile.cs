using AutoMapper;
using Dragonfly.Domain.Model;
using Dragonfly.UI.Mvc.Models;

namespace Dragonfly.UI.Mvc
{
    public class CoursesMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Course, ViewCourse>();
        }
    }
}