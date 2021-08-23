using Microsoft.EntityFrameworkCore;

namespace RestASPNET.Model.Context
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext() { }

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> User { get; set; }
    }
}
