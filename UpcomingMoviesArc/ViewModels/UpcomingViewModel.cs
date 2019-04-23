using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UpcomingMoviesArc.Models;
using UpcomingMoviesArc.Services;
using Xamarin.Forms;

namespace UpcomingMoviesArc.ViewModels
{
    class UpcomingViewModel : BaseViewModel
    {
        private Movie _movieResults;
        public Movie MovieResults
        {
            get => _movieResults;
            set => SetProperty(ref _movieResults, value);
        }

        private ObservableCollection<MovieDetails> _movies;
        public ObservableCollection<MovieDetails> Movies
        {
            get => _movies;
            set => SetProperty(ref _movies, value);
        }
        public int Page { get; private set; }

        private GenreList _genresName { get; set; }

        private string _allGenres;
        public string AllGenres
        {
            get => _allGenres;
            set => SetProperty(ref _allGenres, value);
        }

        private string _searchMovies = string.Empty;
        public string SearchMovies
        {
            get => _searchMovies;
            set
            {
                if (_searchMovies != value)
                {
                    _searchMovies = value ?? string.Empty;
                    SetProperty(ref _searchMovies, value);

                    if (SearchMoviesCommand.CanExecute(null))
                        SearchMoviesCommand.Execute(null);
                }
            }
        }

        public Command LoadMoreCommand { get; }
        private Command _searchMoviesCommand;
        public Command SearchMoviesCommand => _searchMoviesCommand ?? new Command(GetSearchedMovies, CanExecuteSearchCommand);

        readonly GetMovies GetMovies = new GetMovies();

        public UpcomingViewModel()
        {
            GetUpcomingMovies();
            LoadMoreCommand = new Command(LoadMoreMovies, () => !IsBusy);
        }

        private void LoadMoreMovies()
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                MovieResults = await GetMovies.GetUpcomingMovies(MovieResults.Page + 1);
                for (int i = 0; i < MovieResults.Results.Count; i++)
                {
                    MovieDetails item = MovieResults.Results[i];
                    Movies.Add(item);
                    await GetGenresNames();
                }
            }).GetAwaiter().GetResult();

            IsBusy = false;
        }

        void GetUpcomingMovies()
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                MovieResults = await GetMovies.GetUpcomingMovies(1);
                Movies = MovieResults.Results;
                await GetGenresNames();
            }).GetAwaiter().GetResult();

            IsBusy = false;
        }

        private async Task GetGenresNames()
        {
            _genresName = await GetMovies.GetGenresAsync();
            foreach (MovieDetails movie in Movies)
            {
                movie.AllGenreNames += "| ";
                foreach (var movieGenreId in movie.Genre_Ids)
                {
                    movie.AllGenreNames += $" {_genresName.Genres.FirstOrDefault(x => x.Id == movieGenreId).Name} |";
                }
            }
        }

        private void GetSearchedMovies(object obj)
        {
            Task.Run(async () =>
            {
                if (!string.IsNullOrWhiteSpace(_searchMovies))
                {
                    MovieResults = await GetMovies.GetMoviesSearched(_searchMovies);
                    Movies = MovieResults.Results;
                    await GetGenresNames();
                }
                else
                {
                    GetUpcomingMovies();
                }

            }).GetAwaiter().GetResult();
        }

        private bool CanExecuteSearchCommand(object arg) => true;
    }
}
