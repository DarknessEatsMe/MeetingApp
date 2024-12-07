using System;
using System.Collections.Generic;
using meetingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace meetingApp;

public partial class MeetingAppContext : DbContext
{
    public MeetingAppContext()
    {
    }

    public MeetingAppContext(DbContextOptions<MeetingAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Description> Descriptions { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost.localdomain;Port=5432;Database=meetingApp;Username=postgres;Password=Hui20CMzalUpa");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.IdAdress).HasName("adresses_pkey");

            entity.ToTable("adresses");

            entity.HasIndex(e => e.IdUser, "unique_user_adr").IsUnique();

            entity.Property(e => e.IdAdress)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_adress");
            entity.Property(e => e.IdCity).HasColumnName("id_city");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_id_city_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Adress)
                .HasForeignKey<Adress>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_id_user_fkey");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("chats_pkey");

            entity.ToTable("chats");

            entity.Property(e => e.ChatId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("chat_id");
            entity.Property(e => e.ChatName)
                .HasMaxLength(50)
                .HasColumnName("chat_name");
            entity.Property(e => e.IdMatch).HasColumnName("id_match");

            entity.HasOne(d => d.IdMatchNavigation).WithMany(p => p.Chats)
                .HasForeignKey(d => d.IdMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chats_id_match_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.HasIndex(e => e.Name, "unique_city_name").IsUnique();

            entity.Property(e => e.IdCity)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Description>(entity =>
        {
            entity.HasKey(e => e.DescrId).HasName("descriptions_pkey");

            entity.ToTable("descriptions");

            entity.HasIndex(e => e.IdUser, "unique_user_descr").IsUnique();

            entity.Property(e => e.DescrId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("descr_id");
            entity.Property(e => e.Decsr)
                .HasMaxLength(1000)
                .HasColumnName("decsr");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Description)
                .HasForeignKey<Description>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("descriptions_id_user_fkey");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("logins_pkey");

            entity.ToTable("logins");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.Login1)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'user'::character varying")
                .HasColumnName("role");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Login)
                .HasForeignKey<Login>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_log_fk");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.IdMatch).HasName("matches_pkey");

            entity.ToTable("matches");

            entity.Property(e => e.IdMatch)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_match");
            entity.Property(e => e.IdUser1).HasColumnName("id_user1");
            entity.Property(e => e.IdUser2).HasColumnName("id_user2");
            entity.Property(e => e.StatId)
                .HasDefaultValueSql("2")
                .HasColumnName("stat_id");

            entity.HasOne(d => d.IdUser1Navigation).WithMany(p => p.MatchIdUser1Navigations)
                .HasForeignKey(d => d.IdUser1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matches_id_user1_fkey");

            entity.HasOne(d => d.IdUser2Navigation).WithMany(p => p.MatchIdUser2Navigations)
                .HasForeignKey(d => d.IdUser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matches_id_user2_fkey");

            entity.HasOne(d => d.Stat).WithMany(p => p.Matches)
                .HasForeignKey(d => d.StatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matches_stat_id_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.MsgId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("msg_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Msg)
                .HasMaxLength(10000)
                .HasColumnName("msg");
            entity.Property(e => e.MsgDate).HasColumnName("msg_date");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_chat_id_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_id_user_fkey");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("photos_pkey");

            entity.ToTable("photos");

            entity.HasIndex(e => e.IdUser, "unique_user_photo").IsUnique();

            entity.Property(e => e.PhotoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("photo_id");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.PhotoAdr)
                .HasMaxLength(int.MaxValue)
                .HasColumnName("photo_adr");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.Photo)
                .HasForeignKey<Photo>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photos_id_user_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.HasIndex(e => e.Status1, "statuses_status_key").IsUnique();

            entity.Property(e => e.StatId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("stat_id");
            entity.Property(e => e.Status1)
                .HasMaxLength(30)
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.IdUser)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_user");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("father_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.SecName)
                .HasMaxLength(50)
                .HasColumnName("sec_name");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
