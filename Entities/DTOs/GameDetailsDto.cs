using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GameDetailsDto:IDto
    {
        public int ID { get; set; }
        public string GameName { get; set; }
        public string PublisherName { get; set; }
        public string DeveloperName { get; set; }
        public string CategoryName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rate { get; set; }
    }
}
