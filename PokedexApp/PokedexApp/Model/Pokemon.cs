using System.ComponentModel;
using Microsoft.Maui.Storage;

namespace PokedexApp.Model
{
    public class Pokemon : INotifyPropertyChanged
    {
        private bool _isCaught;

        public string? dexNum { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
        public string? classification { get; set; }
        public string? type { get; set; }
        public string? dexEntryRB { get; set; }
        public string? height { get; set; }
        public string? weight { get; set; }
        public string? evolution { get; set; }
        public string? baseStats { get; set; }
        public string? catchRate { get; set; }
        public string? obtainedLocationRed { get; set; }
        public string? obtainedLocationBlue { get; set; }
        public string? obtainedLocationYellow { get; set; }
        public string? tip1 { get; set; }
        public string? tip2 { get; set; }
        public string? tip3 { get; set; }
        public string? type1Color { get; set; }
        public string? type2Color { get; set; }

        public bool isCaught
        {
            get => _isCaught;
            set
            {
                if (_isCaught != value)
                {
                    _isCaught = value;
                    OnPropertyChanged(nameof(isCaught));
                    SaveIsCaught();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadIsCaught()
        {
            if (dexNum != null)
            {
                isCaught = Preferences.Get($"isCaught_{dexNum}", false);
            }
        }

        private void SaveIsCaught()
        {
            if (dexNum != null)
            {
                Preferences.Set($"isCaught_{dexNum}", _isCaught);
            }
        }
    }
}



