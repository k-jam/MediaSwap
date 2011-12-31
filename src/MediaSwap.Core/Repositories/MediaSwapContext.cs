using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Repositories
{
    public class MediaSwapContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
        public DbSet<Format> Format { get; set; }
        public DbSet<Queue> Queue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            Database.SetInitializer<MediaSwapContext>(null);
        }
    }
}
