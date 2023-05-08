using System.Collections.Generic;
using Dragonfly.Application.Contracts.Instructor;
using Dragonfly.Core;
using Dragonfly.Core.Sequencer;
using model = Dragonfly.Domain.Model;
using Dragonfly.Domain.Contracts.Instructor;

namespace Dragonfly.Application.Instructor
{
    public class InstructorAppService : IInstructorAppService
    {
        private readonly IInstructorDomain _instructorDomain;
        private readonly ISequencer _sequencer;

        public InstructorAppService(IInstructorDomain instructorDomain, ISequencer sequencer)
        {
            _instructorDomain = instructorDomain;
            _sequencer = sequencer;
        }

        public IEnumerable<model.Instructor> GetAll()
        {
            return _instructorDomain.GetAll();
        }

        public void Update(model.Instructor instructor)
        {
            _instructorDomain.Update(instructor);
            _instructorDomain.Commit();
        }

        public void Add(model.Instructor instructor)
        {
            instructor.InstructorId = _sequencer.GetNext(typeof (model.Instructor)).ToInt();
            _instructorDomain.Add(instructor);
            _instructorDomain.Commit();
        }
    }
}
