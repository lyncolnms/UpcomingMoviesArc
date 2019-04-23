using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace UpcomingMoviesArc.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Original_Title { get; set; }
        public string Title { get; set; }
        private string _backdrop_Path { get; set; }
        public string Backdrop_Path
        {
            get => _backdrop_Path;
            set => _backdrop_Path = "https://image.tmdb.org/t/p/original/" + value;
        }
        private string _poster_Path;
        public string Poster_Path
        {
            get => _poster_Path;
            set => _poster_Path = "https://image.tmdb.org/t/p/w500/" + value;
        }
        public Collection<int> Genre_Ids { get; set; }
        [JsonIgnore]
        public string AllGenreNames { get; set; }
        public string Overview { get; set; }
        public string Release_Date { get; set; }
    }
}
