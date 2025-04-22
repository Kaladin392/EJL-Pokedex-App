using PokedexApp.Model;

namespace PokedexApp;

[QueryProperty(nameof(Pokemon), "Pokemon")]
public partial class PokemonDetailsPage : ContentPage
{
    private Pokemon _pokemon;

    public Pokemon Pokemon
    {
        get => _pokemon;
        set
        {
            _pokemon = value;
            OnPropertyChanged();
            BindingContext = _pokemon;
        }
    }

    public PokemonDetailsPage()
    {
        InitializeComponent();
    }

    private void ToggleButtonClicked_DetailsPage(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is Pokemon pokemon)
        {
            pokemon.isCaught = !pokemon.isCaught;
        }
    }
}



