using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class Form : Auditable, ITrackeable
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public long SequenceNumber { get; set; }
        public int FormTypeId { get; set; }
        public int? FormSubtypeId { get; set; }
        public int OfficeId { get; set; }
        public int FormStateId { get; set; }
        public decimal Cost { get; set; }
    }
}
