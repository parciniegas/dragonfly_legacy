using System.Collections.Generic;
using Dragonfly.DataAccess.Core;
using Dragonfly.Domain.Contracts.Course;
using Dragonfly.Domain.Model;

namespace Dragonfly.Domain.Services.Courses
{
    public class CourseDomain : ICourseDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> GetAll()
        {
            return _unitOfWork.Repository<Course>().Get(p => p.Instructor);
        }

        public void Add(Course course)
        {
            _unitOfWork.Repository<Course>().Add(course);
            _unitOfWork.SaveChanges();
        }

        public void Update(Course course)
        {
            _unitOfWork.Repository<Course>().Update(course);
            _unitOfWork.SaveChanges();
        }
    }
}
