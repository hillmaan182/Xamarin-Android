﻿using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MobileApp
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Products> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DatabaseContext()
        {
            this.Database.EnsureCreated();
        }
        // overrides the OnConfigure Method 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Dev11.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}