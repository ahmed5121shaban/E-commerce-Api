using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions dbContext ):base(dbContext) {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CartItemConfigration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductAttachmentConfiguration());

            base.OnModelCreating(builder);

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductAttachment> ProductAttachments { get; set; }
        public virtual DbSet<WishListItem> WishListItems { get; set; }
    }
}
