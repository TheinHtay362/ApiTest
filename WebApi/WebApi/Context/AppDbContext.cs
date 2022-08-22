using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Context
{
    public class AppDbContext: DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //     optionsBuilder.UseSqlServer(StaticConfigService.configuration.GetSection("ConnectionStrings:DbStr").Value);
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<UserModel> Users { get; set; }
    }
}
