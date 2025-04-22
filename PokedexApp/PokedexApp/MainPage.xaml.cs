using Microsoft.Maui.Controls;
using PokedexApp.ViewModel;
using PokedexApp.Model;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void ToggleButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is Pokemon pokemon)
            {
                pokemon.isCaught = !pokemon.isCaught;
                var viewModel = GetViewModel();
                if (viewModel != null)
                {
                    await viewModel.FilterPokemonAsync(searchBar.Text);
                }
            }
        }

        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = GetViewModel();
            if (viewModel != null)
            {
                await viewModel.FilterPokemonAsync(e.NewTextValue);
            }
        }

        private void OnFilterPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker)
            {
                var viewModel = GetViewModel();
                if (viewModel != null)
                {
                    viewModel.SelectedFilter = picker.SelectedItem.ToString();
                }
            }
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm Reset", "Are you sure you would like to reset your caught Pokémon data?", "Yes", "No");
            if (answer)
            {
                var viewModel = GetViewModel();
                if (viewModel != null)
                {
                    viewModel.ResetAllPokemon();
                }
            }
        }

        private MainPageViewModel GetViewModel()
        {
            return BindingContext as MainPageViewModel;
        }
    }
}
