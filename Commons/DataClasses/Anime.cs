using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    /// <summary>
    /// The class encapsulating an anime's data
    /// </summary>
    public class Anime
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int ID { get; set; }
        public int EpisodesNumber { get; set; }
        public string Description { get; set; }
        public float AverageRating { get; set; }
    }
}
