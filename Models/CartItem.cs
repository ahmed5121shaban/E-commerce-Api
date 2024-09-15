using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public double SupPrice { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }

    public class CartItemConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(b => b.Quantity).HasDefaultValue(1);
            builder.Property(b => b.SupPrice).IsRequired();
            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.CartLists)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
            builder
               .HasOne(e => e.User)
               .WithMany(e => e.CartList)
               .HasForeignKey(e => e.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        }
    }
}
