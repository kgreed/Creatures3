﻿using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Creatures3.Module.BusinessObjects {
    [TypesInfoInitializer(typeof(Creatures3ContextInitializer))]
	public class Creatures3DbContext : DbContext {
		public Creatures3DbContext(String connectionString)
			: base(connectionString) {
		}
		public Creatures3DbContext(DbConnection connection)
			: base(connection, false) {
		}

		// generated by dev express wizard: use this one when creating migrations in Nuget Package Manager
		public Creatures3DbContext()
		{
		}

		// we need to give it the connection string location. Use this one when running migrations in code

		//public Creatures3DbContext()
		//	: base("name=ConnectionString")
		//{
		//}

		public DbSet<Cat> Cats { get; set; }
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	}
}