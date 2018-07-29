﻿using System;
using System.Data.Entity;
using System.Linq;
using ContactLibrary.Core;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;

namespace ContactLibrary.Data.DataContext
{
    public class SqlDbContext : DbContext, IDbContext
    {
        public SqlDbContext() : base("")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Entitybase
        {
            return base.Set<TEntity>();
        }
    }
}
