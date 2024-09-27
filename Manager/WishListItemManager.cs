using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Manager
{
    public class WishListItemManager : BaseManager<WishListItem>
    {
        public WishListItemManager(AppDbContext dbContextOptions):base(dbContextOptions)
        {
        }

        public IQueryable<WishListItemViewModel> GetAll() => base.GetAll().Adapt<IQueryable<WishListItemViewModel>>();


        public WishListItemViewModel Get(int _ID) => base.GetByID(_ID).Adapt<WishListItemViewModel>();


        public bool Add(WishListItemViewModel wishList)
        {
            try
            {
                base.Add(wishList.Adapt<WishListItem>());
                return true;
            }
            catch (Exception ex)
            {
                //logging
                Console.WriteLine(ex.InnerException);
                return false;
            }

        }


        public async Task<bool> Delete(int id)
        {
            var wishList = await base.GetByID(id);
            if (wishList == null) { return false; }
            try
            {
                base.Delete(wishList);
                return true;
            }
            catch (Exception ex)
            {
                //should do logging here
                throw ex;
            }

        }

        public IQueryable<WishListItemViewModel> GetWishListItemOfUser(string _ID) =>  base.GetAll().Where(w=>w.UserID==_ID)
            .Adapt<IQueryable<WishListItemViewModel>>();
    }
}
