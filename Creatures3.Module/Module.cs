using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using DevExpress.Data.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using Updater = Creatures3.Module.DatabaseUpdate.Updater;
namespace Creatures3.Module
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class Creatures3Module : ModuleBase
    {
        static Creatures3Module()
        {
            CriteriaToEFExpressionConverter.SqlFunctionsType = typeof(SqlFunctions);
            CriteriaToEFExpressionConverter.EntityFunctionsType = typeof(DbFunctions);
            ResetViewSettingsController.DefaultAllowRecreateView = false;
            // Uncomment this code to delete and recreate the database each time the data model has changed.
            // Do not use this code in a production environment to avoid data loss.
            // #if DEBUG
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Creatures3DbContext>());
            // #endif 
        }

        public Creatures3Module()
        {
            InitializeComponent();
            SecurityModule.UsedExportedTypes = UsedExportedTypes.Custom;
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
            return new[] {updater};
        }

        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}