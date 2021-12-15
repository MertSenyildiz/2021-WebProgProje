using Business.Abstract;
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
    public class RatingManager:IRatingService
    {
        IRatingDal _ratingDal;
        public RatingManager(IRatingDal ratingDal)
        {
            _ratingDal = ratingDal;
        }

        public IResult Add(Rating rating)
        {
            _ratingDal.Add(rating);
            return new SuccessResult();
        }

        public IResult Delete(Rating rating)
        {
            _ratingDal.Delete(rating);
            return new SuccessResult();
        }

        public IDataResult<List<Rating>> GetAll()
        {
            return new SuccessDataResult<List<Rating>>(_ratingDal.GetAll());
        }

        public IResult Update(Rating rating)
        {
            _ratingDal.Update(rating);
            return new SuccessResult();
        }
    }
}
