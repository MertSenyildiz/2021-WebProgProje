using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        [ValidationAspect(typeof(RatingValidator))]
        [SecuredOperation("User")]
        public IResult Add(Rating rating)
        {
            _ratingDal.Add(rating);
            return new SuccessResult();
        }
        [SecuredOperation("User")]
        public IDataResult<bool> CheckRateExist(Rating rate)
        {
            var rating = _ratingDal.Get(r => r.GameID == rate.GameID && r.UserID == rate.UserID);
            var result = rating == null ? false : true;
            return new SuccessDataResult<bool>(result);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(Rating rating)
        {
            _ratingDal.Delete(rating);
            return new SuccessResult();
        }

        public IDataResult<List<Rating>> GetAll()
        {
            return new SuccessDataResult<List<Rating>>(_ratingDal.GetAll());
        }

        public IDataResult<List<RatingDetailsDto>> GetAllRatingDetailsByGameId(int id)
        {
            return new SuccessDataResult<List<RatingDetailsDto>>(_ratingDal.GetAllRatingDetailsByGameId(id));
        }
        [SecuredOperation("User")]
        public IResult Update(Rating rating)
        {
            _ratingDal.Update(rating);
            return new SuccessResult();
        }
    }
}
