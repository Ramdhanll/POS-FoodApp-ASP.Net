namespace FoodShop.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // create Conversions enum OrderStatus 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order>()
                .Property(x => x.OrderStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumOrderStatus)Enum.Parse(typeof(EnumOrderStatus), v));
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
