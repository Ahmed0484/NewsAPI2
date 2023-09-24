﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewsAPI.Data
{
    public class NewsAuthDbContext : IdentityDbContext
    {
        public NewsAuthDbContext(DbContextOptions<NewsAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    ConcurrencyStamp = "1",
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = "2",
                    ConcurrencyStamp = "2",
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
