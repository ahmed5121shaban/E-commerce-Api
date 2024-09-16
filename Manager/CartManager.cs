using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Manager
{
    public class CartManager : BaseManager<CartItem>
    {
        public CartManager(AppDbContext _dbContext) : base(_dbContext)
        {
        }

        public void AddToCart(string userID, ProductViewModel productView)
        {
            var item = base.GetAll().FirstOrDefault(c => c.ProductID == productView.ID);
            if (item != null)
            {
                item.SupPrice += productView.Price;
                item.Quantity += productView.Quantity;
                base.Update(item);
            }
            else
            {
                item = new CartItem
                {
                    ProductID = (int)productView.ID,
                    UserID = userID,
                    Quantity = productView.Quantity,
                    SupPrice = productView.Price
                };
            }

            base.Add(item);
        }

    }
}
