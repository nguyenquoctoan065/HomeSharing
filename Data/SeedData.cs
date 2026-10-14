using HomeSharing.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeSharing.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Tự tạo/cập nhật database nếu chưa có
            context.Database.Migrate();

            // Nếu đã có dữ liệu User thì không seed lại
            if (context.Users.Any())
            {
                return;
            }

            // 1. Người dùng (Admin, Host, Guest)
            var admin = new User
            {
                FullName = "Quản trị viên (Admin)",
                Email = "admin@homesharing.vn",
                PasswordHash = "$2a$11$SimulatedBCryptHashForAdminAccount123",
                Phone = "0912345678",
                Role = UserRole.Admin,
                IsLocked = false
            };

            var host = new User
            {
                FullName = "Nguyễn Quốc Toản (Host)",
                Email = "toan.host@homesharing.vn",
                PasswordHash = "$2a$11$SimulatedBCryptHashForHostAccount123",
                Phone = "0987654321",
                Role = UserRole.Host,
                IsLocked = false
            };

            var guest = new User
            {
                FullName = "Nguyễn Mai Trang (Guest)",
                Email = "trang.guest@homesharing.vn",
                PasswordHash = "$2a$11$SimulatedBCryptHashForGuestAccount123",
                Phone = "0901122334",
                Role = UserRole.Guest,
                IsLocked = false
            };

            context.Users.AddRange(admin, host, guest);
            context.SaveChanges();

            // 2. Tiện ích
            var wifi = new Amenity { Name = "Wifi tốc độ cao" };
            var ac = new Amenity { Name = "Điều hòa 2 chiều" };
            var pool = new Amenity { Name = "Hồ bơi vô cực" };
            var kitchen = new Amenity { Name = "Bếp nấu ăn riêng" };
            context.Amenities.AddRange(wifi, ac, pool, kitchen);
            context.SaveChanges();

            // 3. Phòng nghỉ
            var room1 = new Room
            {
                HostId = host.UserId,
                Title = "Homestay View Hồ Tây Hiện Đại",
                Description = "Căn hộ studio rộng 45m2, ban công nhìn thẳng ra Hồ Tây, đầy đủ nội thất.",
                Province = "Hà Nội",
                District = "Tây Hồ",
                Address = "120 Trích Sài, Phường Bưởi",
                PricePerNight = 650000,
                MaxGuests = 4,
                Status = "Đang hoạt động",
                AvgRating = 5.0
            };

            var room2 = new Room
            {
                HostId = host.UserId,
                Title = "Villa Săn Mây Sapa",
                Description = "Biệt thự nghỉ dưỡng hướng thung lũng Mường Hoa, có bể bơi nước ấm.",
                Province = "Lào Cai",
                District = "Sa Pa",
                Address = "05 Đường Mường Hoa",
                PricePerNight = 1500000,
                MaxGuests = 6,
                Status = "Đang hoạt động",
                AvgRating = 4.8
            };

            context.Rooms.AddRange(room1, room2);
            context.SaveChanges();

            // 4. Hình ảnh phòng & tiện ích phòng
            context.RoomImages.AddRange(
                new RoomImage { RoomId = room1.RoomId, Url = "/images/rooms/tayho-1.jpg", IsCover = true, SortOrder = 1 },
                new RoomImage { RoomId = room2.RoomId, Url = "/images/rooms/sapa-1.jpg", IsCover = true, SortOrder = 1 }
            );

            context.RoomAmenities.AddRange(
                new RoomAmenity { RoomId = room1.RoomId, AmenityId = wifi.AmenityId },
                new RoomAmenity { RoomId = room1.RoomId, AmenityId = ac.AmenityId },
                new RoomAmenity { RoomId = room1.RoomId, AmenityId = kitchen.AmenityId },
                new RoomAmenity { RoomId = room2.RoomId, AmenityId = wifi.AmenityId },
                new RoomAmenity { RoomId = room2.RoomId, AmenityId = pool.AmenityId }
            );

            // 5. Đơn đặt phòng, thanh toán, đánh giá mẫu
            var booking = new Booking
            {
                RoomId = room1.RoomId,
                GuestId = guest.UserId,
                CheckIn = DateTime.UtcNow.AddDays(-5),
                CheckOut = DateTime.UtcNow.AddDays(-3),
                Adults = 2,
                Children = 0,
                TotalPrice = 1300000,
                Status = "Đã hoàn thành",
                ExpiresAt = DateTime.UtcNow.AddDays(-6).AddMinutes(5),
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };
            context.Bookings.Add(booking);
            context.SaveChanges();

            context.Payments.Add(new Payment
            {
                BookingId = booking.BookingId,
                Amount = 1300000,
                Method = "Mô phỏng VNPay",
                Status = "Đã thanh toán",
                TransactionCode = "TXN20261014001",
                PaidAt = DateTime.UtcNow.AddDays(-6).AddMinutes(2)
            });

            context.Reviews.Add(new Review
            {
                BookingId = booking.BookingId,
                RoomId = room1.RoomId,
                GuestId = guest.UserId,
                Rating = 5,
                Comment = "Phòng cực kỳ sạch sẽ, view Hồ Tây rất đẹp, chủ nhà nhiệt tình!",
                Status = "Hiển thị"
            });

            context.SaveChanges();
        }
    }
}