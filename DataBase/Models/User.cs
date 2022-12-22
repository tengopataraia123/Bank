namespace Bank.DataBase.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public decimal Income { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
