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
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Platform> Platform { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //                   .HasMany(u => u.Items)
            //                   .WithMany()
            //                   .Map(mc =>
            //                    {
            //                        mc.MapLeftKey("ItemId");
            //                        mc.MapRightKey("UserId");
            //                        mc.ToTable("UserItem");
            //                    });

            //modelBuilder.Entity<Item>().HasOptional(u => u.Users)
            //                   .WithMany()
            //                   .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
