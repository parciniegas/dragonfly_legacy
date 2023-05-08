using System.Data.Entity;
using Dragonfly.Core;
using Dragonfly.Core.Configuration;
using Dragonfly.DataAccess.EF.Base;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class TestContext : BaseContext
    {
        public TestContext(IApplicationEnvironment applicationEnvironment) : base(applicationEnvironment)
        {
        }

        public DbSet<Form> Forms { get; set; }
    }
}
