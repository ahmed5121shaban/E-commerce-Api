using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CartManager : BaseManager<CartItem>
    {
        public CartManager(AppDbContext _dbContext) : base(_dbContext)
        {
        }


    }
}
