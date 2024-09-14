using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductAttachment
    {
            public int ID { get; set; }
            public string Image { get; set; }
            public int ProductID { get; set; }
            public virtual Product Product { get; set; }
       
    }
    public class ProductAttachmentConfiguration: IEntityTypeConfiguration<ProductAttachment>
    {
        public void Configure(EntityTypeBuilder<ProductAttachment> builder)
        {
            builder.ToTable("ProductAttachment");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Image).IsRequired();

            builder
               .HasOne(e => e.Product)
               .WithMany(e => e.ProductAttachments)
               .HasForeignKey(e => e.ProductID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
