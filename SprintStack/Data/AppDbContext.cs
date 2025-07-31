using Microsoft.EntityFrameworkCore;

namespace SprintStack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<IssueFile> IssueFiles { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnName("name")
                      .HasColumnType("character varying")
                      .HasConversion<string>();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name").HasColumnType("character varying");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100).HasColumnName("email").HasColumnType("character varying");
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255).HasColumnName("password").HasColumnType("character varying");
                entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
                entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("name").HasColumnType("character varying");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
                entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.ToTable("team_member");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TeamId).HasColumnName("team_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(e => e.Team)
                    .WithMany(t => t.Members)
                    .HasForeignKey(e => e.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.TeamMembers)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("name").HasColumnType("character varying");
                entity.Property(e => e.StartDate).HasColumnName("start_date").HasColumnType("timestamp");
                entity.Property(e => e.EndDate).HasColumnName("end_date").HasColumnType("timestamp");
                entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
                entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
                entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
                entity.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById);
            });
modelBuilder.Entity<Issue>(entity =>
{
    entity.ToTable("issue");

    entity.HasKey(e => e.Id);

    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.Title).IsRequired().HasMaxLength(100).HasColumnName("title").HasColumnType("character varying");
    entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
    entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
    entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
    entity.Property(e => e.CompletedAt).HasColumnName("completed_at").HasColumnType("timestamp");
    entity.Property(e => e.DevEstimationDay).HasColumnName("dev_estimation_day");
    entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
    entity.Property(e => e.ProjectId).HasColumnName("project_id");
    entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");
    entity.Property(e => e.ReporteeId).HasColumnName("reportee_id");
    entity.Property(e => e.PriorityId).HasColumnName("priority_id");
    entity.Property(e => e.SprintId).HasColumnName("sprint_id");
    entity.Property(e => e.StatusId).HasColumnName("status_id");
    entity.Property(e => e.LabelId).HasColumnName("label_id");
    entity.Property(e => e.QaId).HasColumnName("qa_id");
    entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");

    entity.HasOne(e => e.Project)
          .WithMany(p => p.Issues)
          .HasForeignKey(e => e.ProjectId);

    entity.HasOne(e => e.Assignee)
          .WithMany(u => u.AssignedIssues)
          .HasForeignKey(e => e.AssigneeId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.Reportee)
          .WithMany(u => u.ReportedIssues)
          .HasForeignKey(e => e.ReporteeId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.CreatedBy)
          .WithMany(u => u.CreatedIssues)
          .HasForeignKey(e => e.CreatedById)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.Qa)
          .WithMany(u => u.QaIssues)
          .HasForeignKey(e => e.QaId)
          .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.Priority)
          .WithMany(p => p.Issues)
          .HasForeignKey(e => e.PriorityId);

    entity.HasOne(e => e.Sprint)
          .WithMany(s => s.Issues)
          .HasForeignKey(e => e.SprintId);

    entity.HasOne(e => e.StatusEntity)
          .WithMany(s => s.Issues)
          .HasForeignKey(e => e.StatusId);

    entity.HasOne(e => e.Label)
          .WithMany(l => l.Issues)
          .HasForeignKey(e => e.LabelId);
});

modelBuilder.Entity<Comment>(entity =>
{
    entity.ToTable("comment");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.IssueId).HasColumnName("issue_id");
    entity.Property(e => e.AuthorId).HasColumnName("author_id");
    entity.Property(e => e.Content).IsRequired().HasMaxLength(500).HasColumnName("content").HasColumnType("character varying");
    entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
    entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
    entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
    entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean");

    entity.HasOne(e => e.Issue)
        .WithMany(i => i.Comments)
        .HasForeignKey(e => e.IssueId)
        .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.Author)
        .WithMany(u => u.Comments)
        .HasForeignKey(e => e.AuthorId)
        .OnDelete(DeleteBehavior.Restrict);
});


modelBuilder.Entity<IssueFile>(entity =>
{
    entity.ToTable("issue_files");
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.IssueId).HasColumnName("issue_id");
    entity.Property(e => e.FilePath).IsRequired().HasMaxLength(255).HasColumnName("file_path").HasColumnType("character varying");
    entity.Property(e => e.UploadUserId).HasColumnName("upload_user_id");

    entity.HasOne(e => e.Issue)
        .WithMany(i => i.Files)
        .HasForeignKey(e => e.IssueId)
        .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(e => e.UploadUser)
        .WithMany(u => u.UploadedFiles)
        .HasForeignKey(e => e.UploadUserId)
        .OnDelete(DeleteBehavior.Restrict);
});


            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.ToTable("sprint");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("name").HasColumnType("character varying");
                entity.Property(e => e.Goal).HasColumnName("goal").HasColumnType("text");
                entity.Property(e => e.StartDate).HasColumnName("start_date").HasColumnType("timestamp");
                entity.Property(e => e.EndDate).HasColumnName("end_date").HasColumnType("timestamp");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
                entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
                entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.ToTable("label");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name").HasColumnType("character varying").HasConversion<string>();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.Created).HasColumnName("created").HasColumnType("timestamp");
                entity.Property(e => e.Modified).HasColumnName("modified").HasColumnType("timestamp");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("boolean");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name").HasColumnType("character varying").HasConversion<string>();
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("priorities");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name").HasColumnType("character varying").HasConversion<string>();
            });
        }
    }
}
