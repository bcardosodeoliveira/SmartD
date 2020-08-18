using Smartd.Models.Base;

namespace Smartd.Models
{
    public class Horario : ObservableBaseObject
    {
        private string _horario;
        private bool _check;

        public string horario
        {
            get => _horario;
            set => SetProperty(ref _horario, value);
        }

        public bool check
        {
            get => _check;
            set => SetProperty(ref _check, value);
        }

        public Horario(string horario, bool check)
        {
            this.horario = horario;
            this.check = check;
        }

        public override bool Equals(object obj)
        {
            return obj is Horario horario &&
                   _horario == horario._horario &&
                   _check == horario._check &&
                   this.horario == horario.horario &&
                   check == horario.check;
        }
    }
}

