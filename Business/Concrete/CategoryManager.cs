using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            var result = BusinessRules.Run(CheckIfCategoryNameExists(category.CategoryName));
            if (result != null)
                return result;
            _categoryDal.Add(category);
            return new Result(true);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new Result(true);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.ID == id));
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new Result(true);
        }

        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            if (_categoryDal.Get(c => c.CategoryName == categoryName) != null)
                return new ErrorResult();
            return new SuccessResult();
        }
    }
}
