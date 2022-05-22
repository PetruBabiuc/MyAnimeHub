/**************************************************************************
 *                                                                        *
 *  File:        Anime.cs                                                 *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: The "model" class that encapsulates the attributes of    *
 *               an anime.                                                *
 *                                                                        *
 **************************************************************************/
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
