namespace Smartd.Bibliotecas.Validations
{
    public class EqualsRule<T> : IValidationRule<T>
    {
        public ValidatableObject<string> Field { get; set; }
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var str = value as string;

            return str.Equals(Field.Value);
        }
    }

}
