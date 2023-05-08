using System.Collections.Generic;

namespace Dragonfly.Application.Contracts.Course
{
    public interface ICourseAppService
    {
        IEnumerable<Dragonfly.Domain.Model.Course> GetAll();
        void Add(Dragonfly.Domain.Model.Course course);
        void Update(Dragonfly.Domain.Model.Course course);
    }
}
