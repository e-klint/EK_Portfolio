using Microsoft.EntityFrameworkCore;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data
{
    public class BookshopContext: DbContext
    {
     
        public BookshopContext(DbContextOptions<BookshopContext> options): base(options)
        {
            Console.WriteLine("DbContext created with DI");
        }

        //Mappning mellan entitetsklasser och tabeller i databasen.
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Skapar en admin-användaren i databasen. (För att logga in som användaren, använd "admin123").
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "admin",
                PasswordHash = "80864474B673F9A0D425B091EB785565DFCE99E5DB4A0494E01E1A6D4D832224-8F152C62870ADBCD7402F9120935CBA0", // (Hashat lösenord: admin123)
                Role = "Admin"
            });
            
            //Skapar en customer-användaren i databasen. (För att logga in som användaren, använd "customer123").
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 2,
                UserName = "customer",
                PasswordHash = "E40055AFE5B6654F50995035F6B2E0BBD231874C70801050E6F65CF5F01C860F-9E45A2395CE7F2FB53149323B5CD4241", // (Hashat lösenord: customer123)
                Role = "Customer"
            });

            //Skapar en varukorg kopplad till kunden ovan
            modelBuilder.Entity<Cart>().HasData(new Cart
            {
                CartId = 1,
                UserId = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}

