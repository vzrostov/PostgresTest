using Microsoft.EntityFrameworkCore;
using PostgresTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresTest.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
                : base(options)
        {
        }

        public DbSet<Payload> Payloads { get; set; } = null!;
    }
}
