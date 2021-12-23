using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGameDal : EfEntityRepositoryBase<Game, GameRatingContext>, IGameDal
    {
        public List<GameDetailsDto> GetAllGameDetails()
        {
            using (var context = new GameRatingContext())
            {
                var result = (from g in context.Games
                              join p in context.Publishers
                                 on g.PublisherID equals p.ID
                              join d in context.Developers
                                  on g.DeveloperID equals d.ID
                              join c in context.Categories
                                  on g.CategoryID equals c.ID
                              select new GameDetailsDto
                              {
                                  ID = g.ID,
                                  GameName = g.GameName,
                                  DeveloperName = d.DeveloperName,
                                  PublisherName = p.PublisherName,
                                  CategoryName = c.CategoryName,
                                  ReleaseDate = g.ReleaseDate,
                                  Rate = (from r in context.Ratings
                                          where r.GameID == g.ID
                                          select (double?)r.Rate
                                          ).Average() ?? 0.0//if null returns 0.0 instead of null
                              }).ToList();
                return result;

            }


        }

        public GameDetailsDto GetGameDetailsById(int id)
        {
            using (var context = new GameRatingContext())
            {
                var result = from g in context.Games
                             join p in context.Publishers
                                on g.PublisherID equals p.ID
                             join d in context.Developers
                                 on g.DeveloperID equals d.ID
                             join c in context.Categories
                                 on g.CategoryID equals c.ID
                             where g.ID == id
                             select new GameDetailsDto
                             {
                                 ID = g.ID,
                                 GameName = g.GameName,
                                 DeveloperName = d.DeveloperName,
                                 PublisherName = p.PublisherName,
                                 CategoryName = c.CategoryName,
                                 ReleaseDate = g.ReleaseDate,
                                 Rate = (from r in context.Ratings
                                         where r.GameID == g.ID
                                         select (double?)r.Rate
                                         ).Average()??0.0
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
