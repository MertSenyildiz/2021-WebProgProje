using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RatingDetailsDto:IDto
    {
        public int Rate { get; set; }
        public string UserName { get; set; }
        public string GameName { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

    }
}
