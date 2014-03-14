using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BenchRockers.Common.DataObjects;
using BenchRockers.Common.Interfaces;


namespace BenchRockers.DataAccessLayer
{
    public class BenchRockersDbContext : DbContext, IDataContext
    {
        public BenchRockersDbContext()
            : base("BenchRockersDbContext")
        {
            Database.SetInitializer<BenchRockersDbContext>(new BenchRockersDbInitializer());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        public DbSet<Role> Roles { get; set; }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        public T Add<T>(T item) where T : class
        {
            //Guard.ArgumentNotNull(item, "item");

            var t = Set<T>().Add(item);

            SaveChanges();

            return t;
        }

        public void AddAll<T>(IEnumerable<T> items) where T : class
        {
            //Guard.ArgumentNotNull(items, "items");

            foreach (var item in items)
            {
                Set<T>().Add(item);
            }

            SaveChanges();
        }

        public void Update<T>(T item) where T : class
        {
            //Guard.ArgumentNotNull(item, "item");

            Set<T>().Attach(item);

            // Calling State on an entity in the Detached state will call DetectChanges() 
            // which is required to force an update. 
            Entry(item).State = EntityState.Modified;

            SaveChanges();
        }

        public void UpdateAll<T>(IEnumerable<T> items) where T : class
        {
            //Guard.ArgumentNotNull(items, "items");

            foreach (var item in items)
            {
                Set<T>().Attach(item);
                Entry(item).State = EntityState.Modified;
            }

            SaveChanges();
        }

        public void Delete<T>(T item) where T : class
        {
            //Guard.ArgumentNotNull(item, "item");

            Set<T>().Remove(item);

            Entry(item).State = EntityState.Modified;

            SaveChanges();
        }

        public void DeleteAll<T>(IEnumerable<T> items) where T : class
        {
            //Guard.ArgumentNotNull(items, "items");

            foreach (var item in items)
            {
                Set<T>().Remove(item);
                Entry(item).State = EntityState.Modified;
            }

            SaveChanges();
        }


    }

    public class BenchRockersDbInitializer : DropCreateDatabaseIfModelChanges<BenchRockersDbContext>
    {
        protected override void Seed(BenchRockersDbContext context)
        {
            IList<Skill> defaultSkills = new List<Skill>();

            defaultSkills.Add(new Skill() { Name = "ASP .Net" });
            defaultSkills.Add(new Skill() { Name = "C#" });
            defaultSkills.Add(new Skill() { Name = "VB .Net" });
            defaultSkills.Add(new Skill() { Name = "SQL Server" });
            defaultSkills.Add(new Skill() { Name = "HTML" });
            defaultSkills.Add(new Skill() { Name = "Jquery" });
            defaultSkills.Add(new Skill() { Name = "CSS" });

            foreach (Skill skill in defaultSkills)
                context.Skills.Add(skill);

            IList<Role> defaultRoles = new List<Role>();

            defaultRoles.Add(new Role() { RoleName = "Software Trainee" });
            defaultRoles.Add(new Role() { RoleName = "Software Engineer" });
            defaultRoles.Add(new Role() { RoleName = "Senior Software Engineer" });
            defaultRoles.Add(new Role() { RoleName = "Software Tester" });
            defaultRoles.Add(new Role() { RoleName = "Web Designer" });
            defaultRoles.Add(new Role() { RoleName = "UI Developer" });
            defaultRoles.Add(new Role() { RoleName = "Team Lead" });
            defaultRoles.Add(new Role() { RoleName = "Project Manager" });
            defaultRoles.Add(new Role() { RoleName = "Software Architect" });

            foreach (Role role in defaultRoles)
                context.Roles.Add(role);

            base.Seed(context);

        }
    }
}
