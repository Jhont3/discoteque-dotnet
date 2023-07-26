using Microsoft.EntityFrameworkCore;
using Discoteque.Data.Models;

namespace Discoteque.Data;

public class DiscotequeContext : DbContext
{
    public DiscotequeContext( DbContextOptions< DiscotequeContext > options ) :  base(options)
    {

    }
    public DbSet<Artist> Artists { get; set; }  // reference to table
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Tour> Tours { get; set; }
}
