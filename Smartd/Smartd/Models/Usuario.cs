using Smartd.Bibliotecas.Validations;
using Smartd.Models.Base;
using Newtonsoft.Json;
using SQLite;
using System.Runtime.Serialization;
using System;

namespace Smartd.Models
{
    public class Usuario : ObservableBaseObject, IKeyObject
    {
        private int _id;
        private ValidatableObject<string> _login;
        private ValidatableObject<string> _senha;
        private bool _manterConectado;
        private string _token;
        private string _idPaciente;
        private string _nome;
        private string _email;
        private DateTime _validadeToken;


        [PrimaryKey, AutoIncrement]
        public int id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [Ignore]
        [JsonIgnore]
        [IgnoreDataMember]
        public ValidatableObject<string> Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        [Ignore]
        [JsonIgnore]
        [IgnoreDataMember]
        public ValidatableObject<string> Senha
        {
            get => _senha;
            set => SetProperty(ref _senha, value);
        }

        [Ignore]
        [JsonIgnore]
        [IgnoreDataMember]
        public string Key => id.ToString();

        public string login
        {
            get => Login.Value;
            set { Login.Value = value; OnPropertyChanged(); }
        }

        public string cpf => Login.Value.Replace(".", "").Replace("-", "");

        public string senha
        {
            get => Senha.Value;
            set { Senha.Value = value; OnPropertyChanged(); }
        }

        public bool manterConectado
        {
            get => _manterConectado;
            set => SetProperty(ref _manterConectado, value);
        }

        public string token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        public string idPaciente
        {
            get => _idPaciente;
            set => SetProperty(ref _idPaciente, value);
        }

        public string nome
        {
            get => _nome;
            set => SetProperty(ref _nome, value);
        }

        public string email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        [Ignore, JsonIgnore, IgnoreDataMember]
        public string tokenHeader
        {
            get => $"Bearer {token}";
        }

        [JsonIgnore, IgnoreDataMember]
        public DateTime validadeToken
        {
            get => _validadeToken;
            set => SetProperty(ref _validadeToken, value);
        }

        public Usuario()
        {
            Login = new ValidatableObject<string>();
            Senha = new ValidatableObject<string>();
            manterConectado = true;

            AddValidations();
        }

        public bool Validate()
        {
            bool isValid1 = Login.Validate(),
                 isValid2 = Senha.Validate();

            return isValid1 && isValid2;
        }

        private void AddValidations()
        {
            Login.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Por favor, informe o CPF." });
            Login.Validations.Add(new MaxLengthRule<string> { ValidationMessage = "O CPF deve conter 14 digitos.", MaxLength = 14, Type = MaxLengthType.Equals });
            Login.Validations.Add(new CpfRule<string> { ValidationMessage = "CPF inválido." });
            //Login.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Por favor, informe o Usuário." });
            //Login.Validations.Add(new MaxLengthRule<string>() { ValidationMessage = "O Usuário deve ter no mínimo 3 caracteres.", MaxLength = 3, Type = MaxLengthType.BiggerEqual });
            Senha.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Por favor, informe a Senha." });
            Senha.Validations.Add(new MaxLengthRule<string>() { ValidationMessage = "A Senha deve ter no mínimo 3 caracteres.", MaxLength = 3, Type = MaxLengthType.BiggerEqual });
        }
    }
}

