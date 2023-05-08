using System.Collections.Generic;

namespace Dragonfly.Domain.Contracts.Course
{
    public interface ICourseDomain
    {
        IEnumerable<Dragonfly.Domain.Model.Course> GetAll();
        void Add(Dragonfly.Domain.Model.Course course);
        void Update(Dragonfly.Domain.Model.Course course);
    }
}
