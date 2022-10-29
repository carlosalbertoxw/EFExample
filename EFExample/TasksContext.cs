using EFExample.ModelsBE;
using Microsoft.EntityFrameworkCore;

namespace EFExample
{
    public class TasksContext : DbContext
    {
        public DbSet<CategoryBE> CategoryBE { get; set; }
        public DbSet<TaskBE> TaskBE { get; set; }

        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<CategoryBE> categoriesInit = new List<CategoryBE>();
            categoriesInit.Add(new CategoryBE() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Name = "Actividades trabajo", Order = 1 });
            categoriesInit.Add(new CategoryBE() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Name = "Actividades personales", Order = 2 });


            modelBuilder.Entity<CategoryBE>(category =>
            {
                category.ToTable("Categories");
                category.HasKey(p => p.CategoryId);

                category.Property(p => p.Name).IsRequired().HasMaxLength(250);

                category.Property(p => p.Description).IsRequired(false);

                category.Property(p => p.Order);

                category.HasData(categoriesInit);
            });

            List<TaskBE> tasksInit = new List<TaskBE>();

            tasksInit.Add(new TaskBE() { TaskId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), PriorityTask = Priority.Medium, Title = "Terminar modulo proveedores", CreationDate = DateTime.Now });
            tasksInit.Add(new TaskBE() { TaskId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), PriorityTask = Priority.Low, Title = "Hacer ejercicio", CreationDate = DateTime.Now });

            modelBuilder.Entity<TaskBE>(task =>
            {
                task.ToTable("Tasks");
                task.HasKey(p => p.TaskId);

                task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);

                task.Property(p => p.Title).IsRequired().HasMaxLength(250);

                task.Property(p => p.Description).IsRequired(false);

                task.Property(p => p.PriorityTask);

                task.Property(p => p.CreationDate);

                task.Ignore(p => p.Summary);

                task.HasData(tasksInit);

            });

        }

    }
}
