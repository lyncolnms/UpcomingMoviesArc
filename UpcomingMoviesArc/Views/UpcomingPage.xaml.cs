using UpcomingMoviesArc.Models;
using UpcomingMoviesArc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpcomingMoviesArc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingPage : ContentPage
    {
        UpcomingViewModel Vm;
        public UpcomingPage()
        {
            InitializeComponent();
            BindingContext = Vm = new UpcomingViewModel();
        }

        private void LoadMoreMovies(object sender, ItemVisibilityEventArgs e)
        {
            if (IsBusy || Vm.Movies.Count == 0) return;

            if (e.Item == Vm.Movies[Vm.Movies.Count - 1])
            {
                if (Vm.LoadMoreCommand.CanExecute(null))
                    Vm.LoadMoreCommand.Execute(null);
            }
        }

        async void GoToDetails(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            await Navigation.PushAsync(new DetailsPage(new DetailsViewModel(e.Item as MovieDetails)));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}