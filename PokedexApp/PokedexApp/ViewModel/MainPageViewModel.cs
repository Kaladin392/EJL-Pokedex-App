using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using PokedexApp.Model;
using Microsoft.Maui.Controls;

namespace PokedexApp.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> Pokemon { get; set; }
        private List<Pokemon> allPokemon;
        public ICommand GoToPokemonDetailsCommand { get; }
        private string selectedFilter;
        public int CaughtPokemonCount => allPokemon?.Count(p => p.isCaught) ?? 0;
        public int TotalPokemonCount => allPokemon?.Count ?? 0;

        public MainPageViewModel()
        {
            Pokemon = new ObservableCollection<Pokemon>();
            GoToPokemonDetailsCommand = new Command<Pokemon>(async (pokemon) => await GoToPokemonDetailsAsync(pokemon));
            LoadPokemonAsync();
        }

        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (selectedFilter != value)
                {
                    selectedFilter = value;
                    FilterPokemonAsync(searchText: null);
                }
            }
        }

        private async Task LoadPokemonAsync()
        {
            var assembly = typeof(MainPageViewModel).Assembly;
            using var stream = assembly.GetManifestResourceStream("PokedexApp.Resources.Raw.pokemon.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            allPokemon = JsonSerializer.Deserialize<List<Pokemon>>(json);
            foreach (var pokemon in allPokemon)
            {
                pokemon.LoadIsCaught();
                Pokemon.Add(pokemon);
                OnPropertyChanged(nameof(CaughtPokemonCount));
                OnPropertyChanged(nameof(TotalPokemonCount));
            }
            SortPokemons();
        }

        public async Task FilterPokemonAsync(string searchText)
        {
            IEnumerable<Pokemon> filteredPokemons = allPokemon;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredPokemons = filteredPokemons.Where(p => p.name.ToLower().Contains(searchText.ToLower()));
            }

            if (SelectedFilter == "Isn't Caught")
            {
                filteredPokemons = filteredPokemons.Where(p => !p.isCaught);
            }
            else if (SelectedFilter == "Is Caught")
            {
                filteredPokemons = filteredPokemons.Where(p => p.isCaught);
            }
            else if (SelectedFilter == "Normal Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("NORMAL"));
            }
            else if (SelectedFilter == "Fire Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("FIRE"));
            }
            else if (SelectedFilter == "Water Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("WATER"));
            }
            else if (SelectedFilter == "Grass Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("GRASS"));
            }
            else if (SelectedFilter == "Electric Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("ELECTRIC"));
            }
            else if (SelectedFilter == "Ice Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("ICE"));
            }
            else if (SelectedFilter == "Fighting Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("FIGHTING"));
            }
            else if (SelectedFilter == "Poison Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("POISON"));
            }
            else if (SelectedFilter == "Ground Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("GROUND"));
            }
            else if (SelectedFilter == "Flying Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("FLYING"));
            }
            else if (SelectedFilter == "Psychic Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("PSYCHIC"));
            }
            else if (SelectedFilter == "Bug Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("BUG"));
            }
            else if (SelectedFilter == "Rock Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("ROCK"));
            }
            else if (SelectedFilter == "Ghost Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("GHOST"));
            }
            else if (SelectedFilter == "Dragon Types")
            {
                filteredPokemons = filteredPokemons.Where(p => p.type.Contains("DRAGON"));
            }

            Pokemon.Clear();
            foreach (var pokemon in filteredPokemons)
            {
                Pokemon.Add(pokemon);
            }

            SortPokemons();
            OnPropertyChanged(nameof(CaughtPokemonCount));
        }

        public void ResetAllPokemon()
        {
            foreach (var pokemon in allPokemon)
            {
                pokemon.isCaught = false;
            }
            FilterPokemonAsync(searchText: null);
            OnPropertyChanged(nameof(CaughtPokemonCount));
        }

        private void SortPokemons()
        {
            var sortedPokemon = Pokemon.OrderBy(p => p.dexNum).ToList();
            Pokemon.Clear();
            foreach (var pokemon in sortedPokemon)
            {
                Pokemon.Add(pokemon);
            }
        }

        private async Task GoToPokemonDetailsAsync(Pokemon pokemon)
        {
            if (pokemon is null)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(PokemonDetailsPage), true, new Dictionary<string, object>
            {
                { "Pokemon", pokemon }
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
