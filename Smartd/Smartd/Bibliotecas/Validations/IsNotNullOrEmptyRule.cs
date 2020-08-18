using System;

namespace Smartd.Bibliotecas.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool IsCombo { get; set; }

        public IsNotNullOrEmptyRule()
        {
            IsCombo = false;
        }

        public bool Check(T value)
        {
            if (value != null)
            {
                if (value is string)
                {
                    string val = value as string;

                    return !val.IsNullOrEmpty();
                }
                else if (value is int && IsCombo)
                {
                    int? val = value as int?;

                    return val.Value >= 0;
                }
                else if (value is byte[])
                {
                    byte[] val = value as byte[];

                    return val != null && val.Length > 0;
                }
                else if (value is DateTime?)
                {
                    DateTime? val = value as DateTime?;

                    return val != null && val.HasValue;
                }
            }

            return false;
        }
    }
}
