using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DevExpress.ExpressApp.EF.DesignTime;
namespace Creatures3.Module.BusinessObjects
{
    public class Creatures3ContextInitializer : DbContextTypesInfoInitializerBase {
        protected override DbContext CreateDbContext() {
            DbContextInfo contextInfo = new DbContextInfo(typeof(Creatures3DbContext), new DbProviderInfo(providerInvariantName: "System.Data.SqlClient", providerManifestToken: "2008"));
            return contextInfo.CreateInstance();
        }
    }
}