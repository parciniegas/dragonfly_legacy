using System;

namespace Dragonfly.Core.Sequencer
{
    // ReSharper disable once InconsistentNaming
    public interface ISequencer
    {
        string GetNext(Type type);
        string GetNext(string type);
    }
}
