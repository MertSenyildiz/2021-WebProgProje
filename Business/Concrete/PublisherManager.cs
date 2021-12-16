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
    public class PublisherManager:IPublisherService
    {
        IPublisherDal _publisherDal;
        public PublisherManager(IPublisherDal publisherDal)
        {
            _publisherDal = publisherDal;
        }
        [ValidationAspect(typeof(PublisherValidator))]
        public IResult Add(Publisher publisher)
        {
            _publisherDal.Add(publisher);
            return new SuccessResult();
        }

        public IResult Delete(Publisher publisher)
        {
            _publisherDal.Delete(publisher);
            return new SuccessResult();
        }

        public IDataResult<List<Publisher>> GetAll()
        {
            return new SuccessDataResult<List<Publisher>>(_publisherDal.GetAll());
        }

        public IDataResult<Publisher> GetById(int id)
        {
            return new SuccessDataResult<Publisher>(_publisherDal.Get(p=>p.ID==id));
        }

        public IResult Update(Publisher publisher)
        {
            _publisherDal.Update(publisher);
            return new SuccessResult();
        }
    }
}
