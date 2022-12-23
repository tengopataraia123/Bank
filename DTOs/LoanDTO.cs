using Bank.DataBase.Models;
using Bank.Enums;

namespace Bank.DTOs
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LoanStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
