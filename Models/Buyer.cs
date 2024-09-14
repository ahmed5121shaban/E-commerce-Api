using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Buyer
    {

            public string ID { get; set; }
            public string UserName { get; set; }
            public string UserID { get; set; }
            public virtual User User { get; set; }

        
        
    }
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.User).WithOne(s => s.Buyer).HasForeignKey<Buyer>(b => b.UserID);
        }
    }
}
