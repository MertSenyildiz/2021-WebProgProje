using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRatingService
    {
        IResult Add(Rating rating);
        IResult Delete(Rating rating);
        IResult Update(Rating rating);
        IDataResult<List<Rating>> GetAll();
    }
}
