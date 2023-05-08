using System;

namespace Dragonfly.Core.ArgumentGuard
{
    public abstract class Param
    {
        #region Public Constants
        public const string DefaultName = "";
        #endregion Public Const's

        #region Public Properties
        public string Name { get; private set; }
        public Func<string> ExtraMessageFunc { get; set; }
        #endregion Public Properties

        #region Constructors
        protected Param(string name, Func<string> extraMessageFunc = null)
        {
            Name = name;
            ExtraMessageFunc = extraMessageFunc;
        }
        #endregion Constructors
    }

    public class Param<T> : Param
    {
        #region Public Properties
        public T Value { get; private set; } 
        #endregion

        #region Constructors
        public Param(string name, T value, Func<string> extraMessageFunc = null)
            : base(name, extraMessageFunc)
        {
            Value = value;
        } 
        #endregion
    }
}
