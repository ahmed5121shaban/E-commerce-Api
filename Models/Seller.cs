using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Seller
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.User).WithOne(s => s.Seller).HasForeignKey<Seller>(s => s.UserID);
        }
    }
}
