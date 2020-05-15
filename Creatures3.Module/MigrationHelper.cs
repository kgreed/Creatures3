﻿using System.Data.Entity.Migrations;
using System.Text;
using Creatures3.Module.BusinessObjects;
using Creatures3.Module.Migrations;
 
namespace Creatures.Module
{
    public static class MigrationHelper
    {
        public static string MigrationDescription()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            var pendings = migrator.GetPendingMigrations();
            var sb = new StringBuilder();
            foreach (var pending in pendings)
            {
                sb.AppendLine(pending.ToString());
            }
            return sb.ToString();
        }

        public static void RunMigrations(Creatures3DbContext db)
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            var pendings =  migrator.GetPendingMigrations();
           
            var migratorwithDb = new DbMigrator(configuration, db);
            foreach (var pending in pendings)
            {
                
                migratorwithDb.Update(pending);
            }
        }
    }
}