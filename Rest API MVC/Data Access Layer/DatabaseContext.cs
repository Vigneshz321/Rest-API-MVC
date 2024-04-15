using Microsoft.EntityFrameworkCore;
using Rest_API_MVC.Models;


namespace Rest_API_MVC.Data_Access_Layer;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
}


