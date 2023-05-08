using System;
using Dragonfly.Core;
using Dragonfly.Core.Configuration;
using Dragonfly.Core.Sequencer;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class TestSequencer
    {
        private readonly ISequencer _sequencer;

        public TestSequencer()
        {
            IConfigurator configurator = new ApplicationConfigurator();
            _sequencer = new SqlSequencer(configurator);
        }

        public void DoTest()
        {
            var sequence = _sequencer.GetNext("Dragonfly.TestApps.TestDataAccess.Form");
            Console.WriteLine(sequence);
            Console.ReadLine();
        }
    }
}
