using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGameService
    {
        IResult Add(Game game);
        IResult Delete(Game game);
        IResult Update(Game game);
        IDataResult<Game> GetById(int id);
        IDataResult<List<Game>> GetAll();

        IDataResult<List<GameDetailsDto>> GetAllGameDetails();
        IDataResult<List<GameDetailsDto>> GetGameDetailsByName(string name);
        IDataResult<GameDetailsDto> GetGameDetailsById(int id);
    }
}
