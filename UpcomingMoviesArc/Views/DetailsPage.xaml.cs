using UpcomingMoviesArc.ViewModels;
using Xamarin.Forms;

namespace UpcomingMoviesArc.Views
{
    public partial class DetailsPage : ContentPage
    {
        DetailsViewModel Vm;
        public DetailsPage(DetailsViewModel movieInfo)
        {
            InitializeComponent();
            BindingContext = Vm = movieInfo;
        }
    }
}
