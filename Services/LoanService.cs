using AutoMapper;
using Bank.DataBase;
using Bank.DataBase.Models;
using Bank.DTOs;
using Bank.Enums;
using Bank.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public interface ILoanService
    {
        public Task AddLoan(LoanDTO loan);
        public Task ChangeLaonStatus(int loanId, LoanStatus newStatus);
        public Task<List<LoanDTO>> GetUserLoans(int userId);
        public Task DeleteLoan(int loanId);
        public Task UpdateLoan(LoanDTO newLoan);
    }
    public class LoanService : ILoanService
    {
        private readonly Mapper mapper;
        private readonly BankDbContext context;
        public LoanService(BankDbContext context)
        {
            this.context = context;
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Loan, LoanDTO>()
            .ReverseMap()
            .ForMember(o => o.Id, act => act.Ignore())));
        }

        public async Task AddLoan(LoanDTO loan)
        {
            var data = mapper.Map<Loan>(loan);
            data.Status = LoanStatus.WaitingApproval;
            await context.AddAsync(data);
            await context.SaveChangesAsync();
        }

        public async Task ChangeLaonStatus(int loanId, LoanStatus newStatus)
        {
            var loan = await context.Loans.FirstOrDefaultAsync(o => o.Id == loanId);
            if (loan == null)
                throw new LoanDoesNotExistException();
            loan.Status = newStatus;
            await context.SaveChangesAsync();
        }

        public async Task DeleteLoan(int loanId)
        {
            var loan = await context.Loans.FirstOrDefaultAsync(o => o.Id == loanId);
            if (loan == null)
                throw new LoanDoesNotExistException();
            if (loan.Status != LoanStatus.WaitingApproval)
                throw new CannotDeleteLoanException();
            context.Loans.Remove(loan);
            await context.SaveChangesAsync();
        }

        public async Task<List<LoanDTO>> GetUserLoans(int userId)
        {
            var loans = await context.Loans.Where(o => o.UserId == userId).Select(o=>mapper.Map<LoanDTO>(o)).ToListAsync();
            return loans;
        }

        public async Task UpdateLoan(LoanDTO newLoan)
        {
            var loan = await context.Loans.FirstOrDefaultAsync(o => o.Id == newLoan.Id);
            if (loan == null)
                throw new LoanDoesNotExistException();

            loan.StartDate = newLoan.StartDate;
            loan.EndDate = newLoan.EndDate;
            loan.Amount = newLoan.Amount;
            loan.Currency = newLoan.Currency;

            await context.SaveChangesAsync();
        }
    }
}
