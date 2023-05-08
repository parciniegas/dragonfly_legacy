using System.Collections.Generic;
using System.Linq;
using Dragonfly.Core;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.DataAccess.Core;
using Dragonfly.Domain.Contracts.Instructor;
using Dragonfly.Domain.Model;

namespace Dragonfly.Domain.Services.Instructors
{
    public class InstructorDomain : IInstructorDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorDomain(IUnitOfWork unitOfWork)
        {
            Guard.Check(unitOfWork, "unitOfWork").IsNotNull();
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _unitOfWork.Repository<Instructor>().Get().OrderBy(i => i.Name);
        }

        public void Add(Instructor instructor)
        {
            _unitOfWork.Repository<Instructor>().Add(instructor);
            //_unitOfWork.SaveChanges();
        }

        public void Update(Instructor instructor)
        {
            _unitOfWork.Repository<Instructor>().Update(instructor);
            //_unitOfWork.SaveChanges();
        }

        public void Commit()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
