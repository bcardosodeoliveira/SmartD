namespace Smartd.Models.Request.Agenda
{
    public class ReagendarSessaoRequest
    {
        public class ReagendarSessao
        {
            public Clinica clinica { get; set; }
            public Paciente paciente { get; set; }
            public string data { get; set; }
            public string hora { get; set; }
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