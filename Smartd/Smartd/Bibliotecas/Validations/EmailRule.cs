using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Smartd.Bibliotecas.Validations
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public List<string> BlackDomains { get; set; }
        public string ValidationMessage { get; set; }
        private string _ValidationMessage { get; set; }

        public EmailRule()
        {
            BlackDomains = new List<string>();
        }

        public bool Check(T value)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool sucesso = false;

            if (ValidationMessage.Equals("Só é permitido e-mail corporativo."))
                ValidationMessage = _ValidationMessage;

            if (value != null)
            {
                string val = value as string;
                if (!string.IsNullOrEmpty(val))
                {
                    var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    sucesso = regex.IsMatch(val);
                    if (sucesso && BlackDomains?.Count > 0)
                    {
                        foreach (string blackDomain in BlackDomains)
                        {
                            if (val.Contains(blackDomain))
                            {
                                sucesso = false;
                                if (!ValidationMessage.Equals("Só é permitido e-mail corporativo."))
                                    _ValidationMessage = ValidationMessage;

                                ValidationMessage = "Só é permitido e-mail corporativo.";
                            }
                        }
                    }
                }
            }

            return sucesso;
        }
    }
}
