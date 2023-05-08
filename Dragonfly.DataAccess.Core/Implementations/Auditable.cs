using System;

namespace Dragonfly.DataAccess.Core
{
    public abstract class Auditable : IAuditable
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public abstract class Auditable<T> : Auditable, IEntity<T>
    {
        public T Id { get; set; }
    }
}
