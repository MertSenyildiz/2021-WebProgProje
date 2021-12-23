using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
    public class GameManager : IGameService
    {
        IGameDal _gameDal;
        public GameManager(IGameDal gameDal)
        {
            _gameDal = gameDal;
        }

        [ValidationAspect(typeof(GameValidator))]
        public IResult Add(Game game)
        {
            var result = BusinessRules.Run(CheckIfGameNameExists(game.GameName));
            if (result!=null)
                return result;
            _gameDal.Add(game);
            return new Result(true);
        }

        public IResult Delete(Game game)
        {
            _gameDal.Delete(game);
            return new Result(true);
        }

        public IDataResult<List<Game>> GetAll()
        {
            return new SuccessDataResult<List<Game>>(_gameDal.GetAll());
        }

        public IDataResult<List<GameDetailsDto>> GetAllGameDetails()
        {
            return new SuccessDataResult<List<GameDetailsDto>>(_gameDal.GetAllGameDetails());
        }

        public IDataResult<Game> GetById(int id)
        {
            return new SuccessDataResult<Game>(_gameDal.Get(g=>g.ID==id));
        }

        public IDataResult<GameDetailsDto> GetGameDetailsById(int id)
        {
            return new SuccessDataResult<GameDetailsDto>(_gameDal.GetGameDetailsById(id));
        }

        public IResult Update(Game game)
        {
            _gameDal.Update(game);
            return new Result(true);
        }

        private IResult CheckIfGameNameExists(string gameName)
        {
            if (_gameDal.Get(g => g.GameName == gameName) != null)
                return new ErrorResult();
            return new SuccessResult();
        }
    }
}
