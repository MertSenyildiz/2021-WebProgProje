using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class DeveloperManager:IDeveloperService
    {
        IDeveloperDal _developerDal;
        public DeveloperManager(IDeveloperDal developerDal)
        {
            _developerDal = developerDal;
        }
        [ValidationAspect(typeof(DeveloperValidator))]
        public IResult Add(Developer developer)
        {
            _developerDal.Add(developer);
            return new SuccessResult();
        }

        public IResult Delete(Developer developer)
        {
            _developerDal.Delete(developer);
            return new SuccessResult();
        }

        public IDataResult<List<Developer>> GetAll()
        {
            return new SuccessDataResult<List<Developer>>(_developerDal.GetAll());
        }

        public IDataResult<Developer> GetById(int id)
        {
            return new SuccessDataResult<Developer>(_developerDal.Get(d=> d.ID==id));
        }

        public IResult Update(Developer developer)
        {
            _developerDal.Update(developer);
            return new SuccessResult();
        }
    }
}
