using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AKT.DVDCentral.PL
{
    public partial class DVDCentralEntities : DbContext
    {
        public DVDCentralEntities()
        {
        }

        public DVDCentralEntities(DbContextOptions<DVDCentralEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblCustomer> tblCustomers { get; set; } = null!;
        public virtual DbSet<tblDirector> tblDirectors { get; set; } = null!;
        public virtual DbSet<tblFormat> tblFormats { get; set; } = null!;
        public virtual DbSet<tblGenre> tblGenres { get; set; } = null!;
        public virtual DbSet<tblMovie> tblMovies { get; set; } = null!;
        public virtual DbSet<tblMovieGenre> tblMovieGenres { get; set; } = null!;
        public virtual DbSet<tblOrder> tblOrders { get; set; } = null!;
        public virtual DbSet<tblOrderItem> tblOrderItems { get; set; } = null!;
        public virtual DbSet<tblRating> tblRatings { get; set; } = null!;
        public virtual DbSet<tblUser> tblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=AKT.DVDCentral.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.Zip).HasMaxLength(10);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.tblCustomers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerUserID");
            });

            modelBuilder.Entity<tblDirector>(entity =>
            {
                entity.ToTable("tblDirector");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);
            });

            modelBuilder.Entity<tblFormat>(entity =>
            {
                entity.ToTable("tblFormat");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<tblGenre>(entity =>
            {
                entity.ToTable("tblGenre");

                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblMovie>(entity =>
            {
                entity.ToTable("tblMovie");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.DirectorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieDirectorID");

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.FormatID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieFormatID");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.RatingID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieRatingID");
            });

            modelBuilder.Entity<tblMovieGenre>(entity =>
            {
                entity.ToTable("tblMovieGenre");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.tblMovieGenres)
                    .HasForeignKey(d => d.GenreID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieGenreGenreID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.tblMovieGenres)
                    .HasForeignKey(d => d.MovieID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieGenreMovieID");
            });

            modelBuilder.Entity<tblOrder>(entity =>
            {
                entity.ToTable("tblOrder");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.tblOrders)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCustomerID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.tblOrders)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderUserID");
            });

            modelBuilder.Entity<tblOrderItem>(entity =>
            {
                entity.ToTable("tblOrderItem");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.tblOrderItems)
                    .HasForeignKey(d => d.MovieID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItemMovieID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.tblOrderItems)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItemOrderID");
            });

            modelBuilder.Entity<tblRating>(entity =>
            {
                entity.ToTable("tblRating");

                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
