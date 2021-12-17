using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDeveloperService
    {
        IResult Add(Developer developer);
        IResult Delete(Developer developer);
        IResult Update(Developer developer);
        IDataResult<Developer> GetById(int id);
        IDataResult<List<Developer>> GetAll();
    }
}
