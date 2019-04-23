using System.Collections.ObjectModel;

namespace UpcomingMoviesArc.Models
{
    class Movie
    {
        public ObservableCollection<MovieDetails> Results { get; set; }
        public int Page { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
    }
}
