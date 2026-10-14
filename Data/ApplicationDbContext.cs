using HomeSharing.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeSharing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<BlockedDate> BlockedDates { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. UNIQUE cho User.Email và Amenity.Name
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Amenity>()
                .HasIndex(a => a.Name)
                .IsUnique();

            // 2. Khóa chính kép cho RoomAmenity (N-N)
            modelBuilder.Entity<RoomAmenity>()
                .HasKey(ra => new { ra.RoomId, ra.AmenityId });

            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(ra => ra.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Amenity)
                .WithMany(a => a.RoomAmenities)
                .HasForeignKey(ra => ra.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Booking 1 - 0..1 Review (1 đơn chỉ đánh giá 1 lần)
            modelBuilder.Entity<Review>()
                .HasIndex(rv => rv.BookingId)
                .IsUnique();

            // 4. Tự tham chiếu User (Admin khóa tài khoản)
            modelBuilder.Entity<User>()
                .HasOne(u => u.LockedByAdmin)
                .WithMany()
                .HasForeignKey(u => u.LockedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // 5. Restrict để tránh lỗi Multiple Cascade Paths trên SQL Server
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Host)
                .WithMany(u => u.Rooms)
                .HasForeignKey(r => r.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Guest)
                .WithMany(u => u.Reviews)
                .HasForeignKey(rv => rv.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Room)
                .WithMany(r => r.Reviews)
                .HasForeignKey(rv => rv.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.Actor)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.ActorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}