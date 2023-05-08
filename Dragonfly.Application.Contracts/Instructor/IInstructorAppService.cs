using System.Collections.Generic;

namespace Dragonfly.Application.Contracts.Instructor
{
    public interface IInstructorAppService
    {
        IEnumerable<Dragonfly.Domain.Model.Instructor> GetAll();
        void Add(Dragonfly.Domain.Model.Instructor instructor);
        void Update(Dragonfly.Domain.Model.Instructor instructor);
    }
}
