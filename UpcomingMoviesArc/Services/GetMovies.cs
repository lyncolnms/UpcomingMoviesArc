using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UpcomingMoviesArc.Models;

namespace UpcomingMoviesArc.Services
{
    class GetMovies
    {
        readonly string UrlBase = "https://api.themoviedb.org/3/";
        readonly string ApiKey = "?api_key=1f54bd990f1cdfb230adb312546d765d";
        readonly string UrlUpcoming = "movie/upcoming";
        readonly string UrlGenres = "genre/movie/list";
        readonly string UrlSearchMovies = "search/movie";

        private static HttpClient _client;
        private static HttpClient Client => _client ?? (_client = new HttpClient());

        public async Task<Movie> GetUpcomingMovies(int page)
        {
            HttpResponseMessage response = await Client.GetAsync(UrlBase + UrlUpcoming + ApiKey + "&page=" + page);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Movie>(content);
            }
            return null;
        }

        public async Task<GenreList> GetGenresAsync()
        {
            HttpResponseMessage response = await Client.GetAsync(UrlBase + UrlGenres + ApiKey);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GenreList>(content);
            }
            return null;
        }

        public async Task<Movie> GetMoviesSearched(string query)
        {
            HttpResponseMessage response = await Client.GetAsync(UrlBase + UrlSearchMovies + ApiKey + "&query=" + query);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Movie>(content);
            }

            return null;
        }
    }
}
