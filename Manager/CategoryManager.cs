using Infrastructure;
using Mapster;
using MapsterMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Manager
{
    public class CategoryManager : BaseManager<Category>
    {
        private readonly Mapper mapper;

        public CategoryManager(AppDbContext appContext,Mapper mapper):base(appContext)
        {
            this.mapper = mapper;
        }

        public IQueryable<CategoryViewModel> GetAll()
        {
            //var map = mapper.Adapt<>()
            return base.GetAll().Adapt<IQueryable<CategoryViewModel>>();
        }

        public async Task<CategoryViewModel> Get(int _ID)
        {
            var category =await base.GetByID( _ID );
            return category.Adapt<CategoryViewModel>();
        }

        public bool Add(CategoryViewModel _categoryView)
        {
            try 
            {
                base.Add(_categoryView.Adapt<Category>());
                return true;
            }
            catch (Exception ex) 
            {
                //logging
                Console.WriteLine(ex.InnerException);
                return false;
            }
            
        }

        public bool Update(CategoryViewModel _categoryView)
        {
            try
            {
                base.Update(_categoryView.Adapt<Category>());
                return true;
            }
            catch (Exception ex)
            {
                //should do logging here
                throw ex.InnerException;
            }

        }

        public async Task<bool> Delete(int id)
        {
            var category = await base.GetByID(id);
            if (category == null) { return false; }
            try
            {
                base.Delete(category);
                return true;
            }
            catch (Exception ex)
            {
                //should do logging here
                throw ex;
            }

        }



    }
}
