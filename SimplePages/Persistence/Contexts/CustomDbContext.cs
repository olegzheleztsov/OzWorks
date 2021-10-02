// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;

namespace SimplePages.Persistence.Contexts
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}