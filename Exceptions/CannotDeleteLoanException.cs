namespace Bank.Exceptions
{
    public class CannotDeleteLoanException:Exception
    {
        public CannotDeleteLoanException() : base("Cannot delete loan") { }
    }
}
