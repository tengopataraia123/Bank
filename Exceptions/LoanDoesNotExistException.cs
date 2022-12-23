namespace Bank.Exceptions
{
    public class LoanDoesNotExistException :Exception
    {
        public LoanDoesNotExistException() : base("Loan Doesn't exist") { }
    }
}
