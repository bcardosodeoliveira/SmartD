namespace Smartd.Bibliotecas.Validations
{
    public class MaxLengthRule<T> : IValidationRule<T>
    {
        public bool IsRequerid { get; set; }
        public bool IsInt { get; set; }
        public MaxLengthType Type { get; set; }
        public int MaxLength { get; set; }
        public string ValidationMessage { get; set; }
        public MaxLengthRule()
        {
            IsRequerid = true;
            IsInt = false;
        }

        public bool Check(T value)
        {
            if (value is string && !IsInt)
            {
                string str = value as string;
                if (str.IsNullOrEmpty())
                    str = string.Empty;

                switch (Type)
                {
                    case MaxLengthType.Equals:
                        return str.Length.IsZero(MaxLength) == MaxLength;
                    case MaxLengthType.BiggerEqual:
                        return str.Length.IsZero(MaxLength) >= MaxLength;
                    case MaxLengthType.LessEqual:
                        return str.Length.IsZero(MaxLength) <= MaxLength;
                    case MaxLengthType.Bigger:
                        return str.Length > MaxLength;
                    case MaxLengthType.Less:
                        return str.Length < MaxLength;
                    case MaxLengthType.Different:
                        return str.Length != MaxLength;
                }
            }
            else if (value is int || IsInt)
            {
                int? val = value as int?;

                if (val == null && value != null)
                {
                    val = value.ToString().ToInt(out bool isSuccess);
                    if (!isSuccess)
                        val = null;
                }

                if (val != null)
                {
                    switch (Type)
                    {
                        case MaxLengthType.Equals:
                            return val == MaxLength;
                        case MaxLengthType.BiggerEqual:
                            return val >= MaxLength;
                        case MaxLengthType.LessEqual:
                            return val <= MaxLength;
                        case MaxLengthType.Bigger:
                            return val > MaxLength;
                        case MaxLengthType.Less:
                            return val < MaxLength;
                        case MaxLengthType.Different:
                            return val != MaxLength;
                    }

                }
                else if (!IsRequerid)
                    return true;
            }
            else if (value == null && !IsRequerid)
            {
                return true;
            }
            return false;
        }
    }

    public enum MaxLengthType
    {
        Equals = 0,
        BiggerEqual = 1,
        LessEqual = 2,
        Bigger = 3,
        Less = 4,
        Different = 5,
    }
}

