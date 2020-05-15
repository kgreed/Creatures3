using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EF.DesignTime;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Creatures3.Module.BusinessObjects {
	public class Creatures3ContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			DbContextInfo contextInfo = new DbContextInfo(typeof(Creatures3DbContext), new DbProviderInfo(providerInvariantName: "System.Data.SqlClient", providerManifestToken: "2008"));
            return contextInfo.CreateInstance();
		}
	}

	[NavigationItem("Creatures")]
    public class cat
    {
		public int Id { get; set; }
		public string Name { get; set; }

		public string tag1234 { get; set; }
    }
    [TypesInfoInitializer(typeof(Creatures3ContextInitializer))]
	public class Creatures3DbContext : DbContext {
		public Creatures3DbContext(String connectionString)
			: base(connectionString) {
		}
		public Creatures3DbContext(DbConnection connection)
			: base(connection, false) {
		}
		public Creatures3DbContext() {
		}
		public DbSet<cat> Cats { get; set; }
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	}
}