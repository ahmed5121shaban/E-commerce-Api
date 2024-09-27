using Mapster;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Mappings
{
    public class MapsterConfiguration
    {
        public static void RegisterMappings()
        {
            //map from CartItem to CartItemViewModel
            TypeAdapterConfig<CartItem, CartItemViewModel>.NewConfig()
               .Map(dto => dto.Product, mod => mod.Product.Adapt<ProductViewModel>());

            //map from CartItemViewModel to CartItem
            TypeAdapterConfig<CartItemViewModel,CartItem>.NewConfig()
               .Map(mod => mod.Product, dto => dto.Product.Adapt<Product>());

            //map from Product to ProductViewModel
            TypeAdapterConfig<Product,ProductViewModel>.NewConfig()
                .Map(dist=>dist.CategoryImage,src=>src.Category.Name)
                .Map(dist => dist.CategoryImage, src => src.Category.Image)
                .Map(dest => dest.ProductsImageList, src => src.ProductAttachments.Select(pa => pa.Image).ToList())
                .Ignore(dest => dest.Attachments);

            //map from ProductViewModel to Product
            TypeAdapterConfig<ProductViewModel, Product>.NewConfig()
               .Map(dist => dist.Category,src=>new Category { ID=src.CategoryID,
                   Image=src.CategoryImage,
                   Name=src.CategoryName})
               .Map(dest => dest.ProductAttachments,src => src.ProductsImageList
                   .Select(imagePath => new ProductAttachment
                    {
                         Image = imagePath
                    }).ToList());

            //map from Category to CategoryViewModel
            TypeAdapterConfig<Category, CategoryViewModel>.NewConfig()
                .Map(dist=>dist.Products,src=>src.Products.Adapt<ProductViewModel>());

            //map from CategoryViewModel to Category 
            TypeAdapterConfig< CategoryViewModel, Category>.NewConfig()
                .Map(dist=>dist.Products,src=>src.Products.Adapt<Product>());
        }
    }
}
