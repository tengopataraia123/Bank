namespace Bank.Exceptions
{
    public class UserDoesNotExist : Exception
    {
        public UserDoesNotExist() : base("User doesn't exist") { }
    }
}
