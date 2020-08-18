using Smartd.Bibliotecas.Validations;
using Smartd.Models.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Smartd.Models.Request.Paciente
{
    public class PacienteRequest
    {
        public class Paciente : ObservableBaseObject
        {
            private ValidatableObject<string> _nome_completo;
            private ValidatableObject<string> _sexo;
            private ValidatableObject<DateTime?> _data_nascimento;
            private ValidatableObject<string> _cpf;
            private ValidatableObject<string> _nome_mae;
            private ValidatableObject<string> _etnia;
            private ValidatableObject<string> _naturalidade;
            private ValidatableObject<string> _email;
            private ValidatableObject<string> _senha;
            private ValidatableObject<string> _confirmarSenha;
            private bool _temResponsavel;
            private int _indexUF;

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> NomeCompleto
            {
                get => _nome_completo;
                set => SetProperty(ref _nome_completo, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> Sexo
            {
                get => _sexo;
                set => SetProperty(ref _sexo, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<DateTime?> DataNascimento
            {
                get => _data_nascimento;
                set => SetProperty(ref _data_nascimento, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> CPF
            {
                get => _cpf;
                set => SetProperty(ref _cpf, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> NomeMae
            {
                get => _nome_mae;
                set => SetProperty(ref _nome_mae, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> Etnia
            {
                get => _etnia;
                set => SetProperty(ref _etnia, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> Naturalidade
            {
                get => _naturalidade;
                set => SetProperty(ref _naturalidade, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public bool TemResponsavel
            {
                get => _temResponsavel;
                set
                {
                    SetProperty(ref _temResponsavel, value);

                    if (responsavel?.Count > 0)
                    {
                        if (value)
                        {
                            responsavel[0].Nome.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                            responsavel[0].Telefone.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                        }
                        else
                        {
                            responsavel[0].Nome.Validations.Clear();
                            responsavel[0].Telefone.Validations.Clear();
                        }

                        responsavel[0].Nome.Errors = new List<string>();
                        responsavel[0].Telefone.Errors = new List<string>();
                    }
                }
            }
            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> Email
            {
                get => _email;
                set => SetProperty(ref _email, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> Senha
            {
                get => _senha;
                set => SetProperty(ref _senha, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public ValidatableObject<string> ConfirmarSenha
            {
                get => _confirmarSenha;
                set => SetProperty(ref _confirmarSenha, value);
            }

            [JsonIgnore, IgnoreDataMember]
            public int IndexUF
            {
                get => _indexUF;
                set => SetProperty(ref _indexUF, value);
            }

            public string nome_completo => NomeCompleto?.Value;
            public string sexo => Sexo?.Value;
            public DateTime? data_nascimento => DataNascimento?.Value;
            public string cpf => CPF?.Value?.Replace(".", "").Replace("-", "");
            public string nome_mae => NomeMae?.Value;
            public string etnia => Etnia?.Value;
            public string naturalidade => Naturalidade?.Value;
            public string email => Email?.Value;
            public string senha => Senha?.Value;

            public Rg rg { get; set; }
            public Endereco endereco { get; set; }
            public Telefone telefone { get; set; }
            public string cns { get; set; }
            public List<Responsavel> responsavel { get; set; }
            public Convenio convenio { get; set; }

            public Paciente()
            {
                NomeCompleto = new ValidatableObject<string>();
                Sexo = new ValidatableObject<string>();
                DataNascimento = new ValidatableObject<DateTime?>();
                CPF = new ValidatableObject<string>();
                NomeMae = new ValidatableObject<string>();
                Etnia = new ValidatableObject<string>();
                Naturalidade = new ValidatableObject<string>();
                Email = new ValidatableObject<string>();
                Senha = new ValidatableObject<string>();
                ConfirmarSenha = new ValidatableObject<string>();
                IndexUF = -1;

                rg = new Rg();
                endereco = new Endereco();
                telefone = new Telefone();
                convenio = new Convenio();
                responsavel = new List<Responsavel>() { new Responsavel() };

                AddValidations();
            }

            public bool Validate(int index)
            {
                bool sucesso = false;
                if (index.Equals(0))
                {
                    bool isValid1 = NomeCompleto.Validate(),
                         isValid2 = Sexo.Validate(),
                         isValid3 = DataNascimento.Validate(),
                         isValid4 = CPF.Validate(),
                         isValid5 = rg.Numero.Validate(),
                         isValid6 = NomeMae.Validate(),
                         isValid7 = Etnia.Validate();

                    sucesso = isValid1 && isValid2 && isValid3 && isValid4 && isValid5 && isValid6 && isValid7;
                }
                else if (index.Equals(1))
                {
                    bool isValid1 = endereco.Logradouro.Validate(),
                         isValid2 = endereco.Numero.Validate(),
                         isValid3 = endereco.Bairro.Validate(),
                         isValid4 = endereco.CEP.Validate(),
                         isValid5 = Naturalidade.Validate(),
                         isValid6 = endereco.UF.Validate(),
                         isValid7 = endereco.Cidade.Validate();

                    sucesso = isValid1 && isValid2 && isValid3 && isValid4 && isValid5 && isValid6 && isValid7;
                }
                else if (index.Equals(2))
                {
                    bool isValid1 = telefone.Celular.Validate(),
                         isValid2 = telefone.Fixo.Validate(),
                         isValid3 = Email.Validate(),
                         isValid4 = responsavel[0].Nome.Validate(),
                         isValid5 = responsavel[0].Telefone.Validate();

                    sucesso = isValid1 && isValid2 && isValid3 && isValid4 && isValid5;
                }
                else if (index.Equals(3))
                {
                    bool isValid1 = convenio.Empresa.Validate(),
                         isValid2 = convenio.Nome.Validate(),
                         isValid3 = convenio.Plano.Validate(),
                         isValid4 = convenio.Nro_carteira.Validate();

                    sucesso = isValid1 && isValid2 && isValid3 && isValid4;
                }
                else if (index.Equals(4))
                {
                    bool isValid1 = Senha.Validate(),
                         isValid2 = ConfirmarSenha.Validate();

                    sucesso = isValid1 && isValid2;
                }

                return sucesso;
            }

            private void AddValidations()
            {
                //0
                NomeCompleto.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                Sexo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                DataNascimento.Validations.Add(new IsNotNullOrEmptyRule<DateTime?> { ValidationMessage = "Campo obrigatório." });
                rg.Numero.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                NomeMae.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                Etnia.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });

                CPF.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                CPF.Validations.Add(new MaxLengthRule<string> { ValidationMessage = "O CPF deve conter 14 digitos.", MaxLength = 14, Type = MaxLengthType.Equals });
                CPF.Validations.Add(new CpfRule<string> { ValidationMessage = "CPF inválido." });

                //1
                endereco.Logradouro.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.Numero.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.Bairro.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.CEP.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.CEP.Validations.Add(new MaxLengthRule<string>() { ValidationMessage = "CEP inválido.", MaxLength = 9, Type = MaxLengthType.Equals });
                Naturalidade.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.UF.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                endereco.Cidade.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });

                //2
                telefone.Celular.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                telefone.Fixo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                Email.Validations.Add(new EmailRule<string> { ValidationMessage = "E-mail inválido." });

                //3
                convenio.Empresa.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                convenio.Nome.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                convenio.Plano.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                convenio.Nro_carteira.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });

                //4
                Senha.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                Senha.Validations.Add(new MaxLengthRule<string>() { ValidationMessage = "A Senha deve ter no mínimo 8 caracteres.", MaxLength = 8, Type = MaxLengthType.BiggerEqual });
                Senha.Validations.Add(new EqualsRule<string> { ValidationMessage = "A Senha e a Confirmação são divergentes.", Field = ConfirmarSenha });

                ConfirmarSenha.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório." });
                ConfirmarSenha.Validations.Add(new MaxLengthRule<string>() { ValidationMessage = "A Confirmação da Senha deve ter no mínimo 8 caracteres.", MaxLength = 8, Type = MaxLengthType.BiggerEqual });
                ConfirmarSenha.Validations.Add(new EqualsRule<string> { ValidationMessage = "A Senha e a Confirmação são divergentes.", Field = Senha });
            }

            public class Rg : ObservableBaseObject
            {

                private ValidatableObject<string> _numero;

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Numero
                {
                    get => _numero;
                    set => SetProperty(ref _numero, value);
                }

                public string numero => Numero?.Value;
                public DateTime data_emissao { get; set; }
                public string orgao_emissor { get; set; }
                public string cidade { get; set; }
                public string uf { get; set; }

                public Rg()
                {
                    Numero = new ValidatableObject<string>();
                }
            }

            public class Endereco : ObservableBaseObject
            {
                private ValidatableObject<string> _cep;
                private ValidatableObject<string> _logradouro;
                private ValidatableObject<string> _numero;
                private ValidatableObject<string> _bairro;
                private ValidatableObject<string> _uf;
                private ValidatableObject<string> _cidade;
                private string _complemento;

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Logradouro
                {
                    get => _logradouro;
                    set => SetProperty(ref _logradouro, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> CEP
                {
                    get => _cep;
                    set => SetProperty(ref _cep, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Numero
                {
                    get => _numero;
                    set => SetProperty(ref _numero, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Bairro
                {
                    get => _bairro;
                    set => SetProperty(ref _bairro, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> UF
                {
                    get => _uf;
                    set => SetProperty(ref _uf, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Cidade
                {
                    get => _cidade;
                    set => SetProperty(ref _cidade, value);
                }

                public string cep => CEP?.Value;
                public string logradouro => Logradouro?.Value;
                public string numero => Numero?.Value;
                public string bairro => Bairro?.Value;
                public string uf => UF?.Value;
                public string cidade => Cidade?.Value;

                public string complemento
                {
                    get => _complemento;
                    set => SetProperty(ref _complemento, value);
                }

                public Endereco()
                {
                    Logradouro = new ValidatableObject<string>();
                    CEP = new ValidatableObject<string>();
                    Numero = new ValidatableObject<string>();
                    Bairro = new ValidatableObject<string>();
                    UF = new ValidatableObject<string>();
                    Cidade = new ValidatableObject<string>();
                }
            }

            public class Telefone : ObservableBaseObject
            {
                private ValidatableObject<string> _celular;
                private ValidatableObject<string> _fixo;

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Celular
                {
                    get => _celular;
                    set => SetProperty(ref _celular, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Fixo
                {
                    get => _fixo;
                    set => SetProperty(ref _fixo, value);
                }

                public string celular => Celular?.Value;
                public string fixo => Fixo?.Value;

                public Telefone()
                {
                    Celular = new ValidatableObject<string>();
                    Fixo = new ValidatableObject<string>();
                }
            }

            public class Responsavel : ObservableBaseObject
            {
                private ValidatableObject<string> _nome;
                private ValidatableObject<string> _telefone;

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Nome
                {
                    get => _nome;
                    set => SetProperty(ref _nome, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Telefone
                {
                    get => _telefone;
                    set => SetProperty(ref _telefone, value);
                }

                public string nome => Nome?.Value;
                public string telefone => Telefone?.Value;

                public Responsavel()
                {
                    Nome = new ValidatableObject<string>();
                    Telefone = new ValidatableObject<string>();
                }
            }

            public class Convenio : ObservableBaseObject
            {
                private ValidatableObject<string> _nome;
                private ValidatableObject<string> _empresa;
                private ValidatableObject<string> _plano;
                private ValidatableObject<string> _nro_carteira;

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Nome
                {
                    get => _nome;
                    set => SetProperty(ref _nome, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Empresa
                {
                    get => _empresa;
                    set => SetProperty(ref _empresa, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Plano
                {
                    get => _plano;
                    set => SetProperty(ref _plano, value);
                }

                [JsonIgnore, IgnoreDataMember]
                public ValidatableObject<string> Nro_carteira
                {
                    get => _nro_carteira;
                    set => SetProperty(ref _nro_carteira, value);
                }

                public string nome => Nome?.Value;
                public string empresa => Empresa?.Value;
                public string plano => Plano?.Value;
                public string nro_carteira => Nro_carteira?.Value;

                public Convenio()
                {
                    Nome = new ValidatableObject<string>();
                    Empresa = new ValidatableObject<string>();
                    Plano = new ValidatableObject<string>();
                    Nro_carteira = new ValidatableObject<string>();
                }
            }
        }
    }
}
