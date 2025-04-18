using Microsoft.EntityFrameworkCore;
using BookLibrary.Models;
using System.Collections.Generic;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=library_db;Username=postgres;Password=2746");
    }
}
