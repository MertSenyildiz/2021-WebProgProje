using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRatingDal : EfEntityRepositoryBase<Rating, GameRatingContext>, IRatingDal
    {
        public List<RatingDetailsDto> GetAllRatingDetailsByGameId(int id)
        {
            using (var context = new GameRatingContext())
            {
                var result = from rating in context.Ratings
                             join user in context.Users
                                  on rating.UserID equals user.ID
                             where user.Status == true
                             join game in context.Games
                                  on rating.GameID equals game.ID
                             where game.ID == id
                             select new RatingDetailsDto
                             {
                                 UserName=user.UserName,
                                 GameName=game.GameName,
                                 Comment=rating.Comment,
                                 Rate=rating.Rate,
                                 Date=rating.Date
                             };
                return result.ToList();

            }
        }
    }
}
