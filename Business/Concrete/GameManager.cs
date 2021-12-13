using Business.Abstract;
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
        public void Add(Game game)
        {
            _gameDal.Add(game);
        }

        public void Delete(Game game)
        {
            _gameDal.Delete(game);
        }

        public List<Game> GetAll()
        {
            return _gameDal.GetAll();
        }

        public Game GetById(int id)
        {
            return _gameDal.Get(g=>g.ID==id);
        }

        public void Update(Game game)
        {
            _gameDal.Update(game);
        }
    }
}
