using System.Collections.Generic;

namespace Dragonfly.Domain.Contracts.Instructor
{
    public interface IInstructorDomain
    {
        IEnumerable<Dragonfly.Domain.Model.Instructor> GetAll();
        void Add(Dragonfly.Domain.Model.Instructor instructor);
        void Update(Dragonfly.Domain.Model.Instructor instructor);
        void Commit();
    }
}
