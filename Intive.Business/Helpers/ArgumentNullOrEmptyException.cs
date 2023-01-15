namespace Intive.Business.Helpers
{
    public class ArgumentNullOrEmptyException : Exception
    {
        public ArgumentNullOrEmptyException(string propertyName) : base($"Property {propertyName} cannot by null or empty.") { }
    }
}
