using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductAttachment> ProductAttachments { get; set; }
        public virtual ICollection<WishListItem> WishLists { get; set; }
        public virtual ICollection<CartItem> CartLists { get; set; }
    }
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            //builder.HasIndex(b => b.Name).IsUnique();
            builder.HasKey(b => b.ID);
            builder.Property(b => b.ID).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).HasMaxLength(1000).IsRequired();
            builder.Property(b => b.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(b => b.Quantity).IsRequired().HasDefaultValue(1);
            builder.Property(b => b.Price).IsRequired();
            builder
            .HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }
    }
}
