using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.EFData
{
    public partial class VietUsaDatabaseContext : DbContext
    {
        private VietUsaDatabaseContext(DbConnection connection, DbCompiledModel model)
            : base(connection, model, contextOwnsConnection: false)
        {

        }

        private static readonly ConcurrentDictionary<Tuple<string, string>, DbCompiledModel> ModelCache = new ConcurrentDictionary<Tuple<string, string>, DbCompiledModel>();

        /// <summary>
        /// Creates a context that will access the specified tenant
        /// </summary>
        public static VietUsaDatabaseContext Create(string tenantSchema, DbConnection connection)
        {
            var compiledModel = ModelCache.GetOrAdd(
                Tuple.Create(connection.ConnectionString, tenantSchema),
                t =>
                {
                    var builder = new DbModelBuilder();
                    builder.Conventions.Remove<IncludeMetadataConvention>();
                    builder.Entity<Group>().ToTable("Group", tenantSchema);
                    builder.Entity<GroupRole>().ToTable("GroupRole", tenantSchema);
                    builder.Entity<Log>().ToTable("Log", tenantSchema);
                    builder.Entity<LogDbError>().ToTable("LogDbError", tenantSchema);
                    builder.Entity<Role>().ToTable("Role", tenantSchema);
                    builder.Entity<User>().ToTable("User", tenantSchema);
                    builder.Entity<UserGroup>().ToTable("UserGroup", tenantSchema);
                    builder.Entity<Document>().ToTable("Document", tenantSchema);
                    builder.Entity<Article>().ToTable("Article", tenantSchema);
                    builder.Entity<Contact>().ToTable("Contact", tenantSchema);
                    builder.Entity<Member>().ToTable("Member", tenantSchema);
                    builder.Entity<Category>().ToTable("Category", tenantSchema);
                    builder.Entity<Organisation>().ToTable("Organisation", tenantSchema);

                    builder.Conventions.Remove<PluralizingTableNameConvention>();

                    var model = builder.Build(connection);
                    return model.Compile();
                });
            Database.SetInitializer<VietUsaDatabaseContext>(null);
            return new VietUsaDatabaseContext(connection, compiledModel);
        }

        /// <summary> 
        /// Creates the database and/or tables for a new tenant 
        /// </summary> 
        public static void ProvisionTenant(string tenantSchema, DbConnection connection)
        {
            using (var ctx = Create(tenantSchema, connection))
            {
                if (!ctx.Database.Exists())
                {
                    ctx.Database.Create();
                }
                else
                {
                    var createScript = ((IObjectContextAdapter)ctx).ObjectContext.CreateDatabaseScript();
                    ctx.Database.ExecuteSqlCommand(createScript);
                }
            }
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupRole> GroupRoles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LogDbError> LogDbErrors { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
