// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.EntityFrameworkCore;

namespace clscs.EF.Contexts
{
    public class ResilientContext : AdventureWorksContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString,
                options =>
                {
                    options.EnableRetryOnFailure();
                    options.UseHierarchyId();
                });
            DoLogToConsole(optionsBuilder);
        }
    }
}