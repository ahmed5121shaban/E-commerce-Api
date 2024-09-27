using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModel
{
    public static class Extainsions
    {
        public static Product MapUpdate(this ProductViewModel productView)
        {
            List<ProductAttachment> imagesPathes = null;


            imagesPathes = new List<ProductAttachment>();
            foreach (IFormFile item in productView.Attachments)
            {
                imagesPathes.Add(new ProductAttachment { Image = item.FileName });
            }

            return new Product
            {
                CategoryID = productView.CategoryID,
                ID = (int)productView.ID,
                Price = productView.Price,
                Name = productView.Name,
                Quantity = productView.Quantity,
                Description = productView.Description,
                ProductAttachments = imagesPathes,
            };
        }
        public static Product MapAdd(this ProductViewModel productView)
        {
            List<ProductAttachment> imagesPathes = null;


            imagesPathes = new List<ProductAttachment>();
            if (productView.Attachments != null)
            {
                foreach (IFormFile item in productView.Attachments)
                {
                    imagesPathes.Add(new ProductAttachment { Image = item.FileName });
                }
            }



            return new Product
            {
                CategoryID = productView.CategoryID,
                Price = productView.Price,
                Name = productView.Name,
                Quantity = productView.Quantity,
                Description = productView.Description,
                ProductAttachments = imagesPathes,
            };
        }
        public static ProductViewModel MapToView(this Product productView)
        {
            if(productView == null)
                return null;
            List<string> paths = new List<string>();
            if (!(productView.ProductAttachments == null))
            {
                foreach (var item in productView.ProductAttachments)
                {
                    paths.Add(item.Image);
                }
            }
            

            return new ProductViewModel
            {
                CategoryID = productView.CategoryID,
                ID = (int)productView.ID,
                Price = productView.Price,
                Name = productView.Name,
                Quantity = productView.Quantity,
                Description = productView.Description,
                ProductsImageList = paths
            };
        }
        public static User MapFromRegisterToUser(this UserRegisterViewModel user)
        {
            return new User
            {
                Email = user.Email,
                UserName = user.UserName,
            };
        }
        public static User MapFromLoginToUser(this UserLoginViewModel user)
        {
            return new User
            {
                UserName = user.Login.Contains("@") ? null : user.Login,
                Email = user.Login.Contains("@") ? user.Login : null,
            };
        }
        public static Category MapToCategoryViewModel(this CategoryViewModel categoryView)
        {
            return new Category { ID = (int)categoryView.ID, Image = categoryView.Image.FileName
            , Name = categoryView.Name, Products = categoryView.Products};
        }

       public static WishListItem MapToWishListItemViewModel(this WishListItemViewModel wishListView)
        {
            return new WishListItem
            {
               ProductID = wishListView.ProductID,
               UserID = wishListView.UserID,
            };
        }
    }
}
