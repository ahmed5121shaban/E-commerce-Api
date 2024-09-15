using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WishListItem
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }
    public class WishListItemConfigration : IEntityTypeConfiguration<WishListItem>
    {
        public void Configure(EntityTypeBuilder<WishListItem> builder)
        {
            builder.ToTable("WishListItem");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();

            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.WishLists)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
            builder
               .HasOne(e => e.User)
               .WithMany(e => e.WishList)
               .HasForeignKey(e => e.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
