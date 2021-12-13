using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Game:IEntity
    {
        public int ID { get; set; }
        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
