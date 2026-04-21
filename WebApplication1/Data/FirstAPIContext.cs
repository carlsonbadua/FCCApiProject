using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class FirstAPIContext : DbContext
{
    public FirstAPIContext(DbContextOptions<FirstAPIContext> options)
        : base(options)
    {}

    public DbSet<Book> Books { get; set; }
}
