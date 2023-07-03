using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterMVC.Data;

public class RestaurantDbContext : DbContext //A DbContext instance represents a session with the database and can be used to query and save instances of your entities
{
    // Initializes a new instance of the DbContext class using the specified options
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options) { }
    
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
}