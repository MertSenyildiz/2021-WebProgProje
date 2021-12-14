using Business.Abstract;
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
    public class GameManager : IGameService
    {
        IGameDal _gameDal;
        public GameManager(IGameDal gameDal)
        {
            _gameDal = gameDal;
        }
        public IResult Add(Game game)
        {
            var result = BusinessRules.Run(CheckIfBookNameExists(game.GameName));
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

        public IDataResult<Game> GetById(int id)
        {
            return new SuccessDataResult<Game>(_gameDal.Get(g=>g.ID==id));
        }

        public IResult Update(Game game)
        {
            _gameDal.Update(game);
            return new Result(true);
        }

        private IResult CheckIfBookNameExists(string gameName)
        {
            if (_gameDal.Get(g => g.GameName == gameName) != null)
                return new ErrorResult();
            return new SuccessResult();
        }
    }
}
