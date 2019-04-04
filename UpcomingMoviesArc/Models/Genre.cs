using System.Collections.Generic;

namespace UpcomingMoviesArc.Models
{
    public class GenreList
    {
        public List<Genres> Genres { get; set; }
    }

    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
