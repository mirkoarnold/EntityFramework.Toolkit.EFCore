﻿using System;
using EFCore.Toolkit;
using EFCore.Toolkit.Auditing;
using EFCore.Toolkit.Auditing.Extensions;
using EFCore.Toolkit.Contracts;
using Microsoft.EntityFrameworkCore;
using ToolkitSample.Model;
using ToolkitSample.Model.Auditing;

namespace ToolkitSample.DataAccess.Context.Auditing
{
    /// <summary>
    /// This data context is used to demonstrate the auditing features.
    /// It is configured using the app.config.
    /// </summary>
    public class TestAuditDbContext : AuditDbContextBase<TestAuditDbContext>
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public DbSet<TestEntityAudit> TestEntityAudits { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeAudit> EmployeeAudits { get; set; }

        public TestAuditDbContext(DbContextOptions dbContextOptions, IDatabaseInitializer<TestAuditDbContext> databaseInitializer)
            : base(dbContextOptions, databaseInitializer)
        {
            //TODO this.Configuration.ProxyCreationEnabled = false;
            this.ConfigureAuditingFromAppConfig();
        }

        public TestAuditDbContext(DbContextOptions dbContextOptions, IDatabaseInitializer<TestAuditDbContext> databaseInitializer, Action<string> log)
            : base(dbContextOptions, databaseInitializer, log)
        {
            //TODO this.Configuration.ProxyCreationEnabled = false;
            this.ConfigureAuditingFromAppConfig();
        }

        public TestAuditDbContext(IDbConnection dbConnection, IDatabaseInitializer<TestAuditDbContext> databaseInitializer, Action<string> log = null)
            : base(dbConnection, databaseInitializer, log)
        {
            //TODO this.Configuration.ProxyCreationEnabled = false;
            this.ConfigureAuditingFromAppConfig();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new PersonEntityConfiguration());
            modelBuilder.AddConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new EmployeeAuditEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new TestEntityEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new TestEntityAuditEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new StudentEntityConfiguration());
            modelBuilder.AddConfiguration(new DepartmentEntityConfiguration());
            modelBuilder.AddConfiguration(new RoomConfiguration());
            modelBuilder.AddConfiguration(new CountryEntityConfiguration());
            modelBuilder.AddConfiguration(new ApplicationSettingEntityTypeConfiguration());
        }
    }
}