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
    public class CategoryManager : BaseManager<Category>
    {
        public CategoryManager(AppDbContext appContext):base(appContext){}

        public IQueryable<Category> GetAll()
        {
            return base.GetAll();
        }

        public async Task<Category> Get(string _ID)
        {
            var category =await base.GetByID( _ID );
            return category;
        }

        public bool Add(CategoryViewModel _categoryView)
        {
            try 
            {
                base.Add(_categoryView.MapToCategoryViewModel());
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
                base.Update(_categoryView.MapToCategoryViewModel());
                return true;
            }
            catch (Exception ex)
            {
                //should do logging here
                throw ex.InnerException;
            }

        }

        public bool Delete(CategoryViewModel _categoryView)
        {
            try
            {
                base.Delete(_categoryView.MapToCategoryViewModel());
                return true;
            }
            catch (Exception ex)
            {
                //should do logging here
                throw ex.InnerException;
            }

        }



    }
}
