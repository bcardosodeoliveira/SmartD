namespace Smartd.Bibliotecas.Validations
{
    public class CpfRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value != null)
            {
                if (value is string)
                {
                    string val = value as string;
                    if (!val.IsNullOrEmpty())
                        return val.IsCpf();
                }
            }

            return false;
        }
    }
}
