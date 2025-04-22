namespace PokedexApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PokemonDetailsPage), typeof(PokemonDetailsPage));
        }
    }

}
