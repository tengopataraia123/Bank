namespace Bank.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("User with same username alread exists") { }
    }
}
