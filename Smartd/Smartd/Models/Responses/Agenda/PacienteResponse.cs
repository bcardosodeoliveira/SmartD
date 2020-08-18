using Smartd.Bibliotecas;
using Smartd.Models.Base;
using System;
using System.Collections.Generic;

namespace Smartd.Models.Responses.Agenda
{
    public class PacienteResponse
    {
        public class Agenda : ResponseCore
        {
            public List<Item> Items { get; set; }
        }

        public class Item : ObservableBaseObject
        {
            private string _hora;

            public Clinica clinica { get; set; }
            public Paciente paciente { get; set; }
            public bool excluido { get; set; }
            public string _id { get; set; }
            public DateTime data { get; set; }
            public string hora
            {
                get => _hora;
                set => SetProperty(ref _hora, value);
            }
            public bool ocupado { get; set; }
            public DateTime created_at { get; set; }
            public DateTime? updated_at { get; set; }
            public long __v { get; set; }
            public string id { get; set; }


            public string data_formatada
            {
                get
                {
                    if (id.IsNullOrEmpty())
                        return null;

                    string mes = data.ToString("MMMM");
                    return $"{data:dd} de {char.ToUpper(mes[0]) + mes.Substring(1)} - {hora}";
                }
            }
        }

        public class Clinica
        {
            public string id { get; set; }
            public string nome { get; set; }
        }

        public class Paciente
        {
            public string id { get; set; }
            public string nome_completo { get; set; }
        }
    }
}
