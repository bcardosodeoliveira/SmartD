using System.Collections.Generic;

namespace Smartd.Models.Responses.Agenda
{
    public class DisponibilidadeResponse
    {
        public class Disponibilidade : ResponseCore
        {
            public List<string> Items { get; set; }
        }
    }
}
