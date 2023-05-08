using System.Collections.Generic;
using Dragonfly.Application.Contracts.Course;
using Dragonfly.Core;
using Dragonfly.Core.Sequencer;
using Dragonfly.Domain.Contracts.Course;
using Model = Dragonfly.Domain.Model;

namespace Dragonfly.Application.Course
{
    public class CourseAppService : ICourseAppService
    {
        private readonly ICourseDomain _courseDomain;
        private readonly ISequencer _sequencer;

        public CourseAppService(ICourseDomain courseDomain, ISequencer sequencer)
        {
            _courseDomain = courseDomain;
            _sequencer = sequencer;
        }

        public IEnumerable<Model.Course> GetAll()
        {
            return _courseDomain.GetAll();
        }

        public void Update(Model.Course course)
        {
            _courseDomain.Update(course);
        }

        public void Add(Model.Course course)
        {
            course.CourseId = _sequencer.GetNext(typeof (Model.Course)).ToInt();
            _courseDomain.Add(course);
        }
    }
}
