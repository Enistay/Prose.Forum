using Prose.Core.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace Prose.Infrastructure.Persistency
{
    public class ProseDbContext : DbContext
    {
        public ProseDbContext() : base("name=ProseConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProseDbContext, Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Topic> Topic { get; set; }
        public DbSet<User> User { get; set; }

    }
}
