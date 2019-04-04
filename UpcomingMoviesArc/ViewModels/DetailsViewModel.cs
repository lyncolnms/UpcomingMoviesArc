using UpcomingMoviesArc.Models;

namespace UpcomingMoviesArc.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public MovieDetails MovieInfo { get; set; }

        public DetailsViewModel(MovieDetails movieInfo = null)
        {
            TitlePage = movieInfo?.Title;
            MovieInfo = movieInfo;
        }
    }
}
