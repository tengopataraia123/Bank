using Bank.DTOs;
using Bank.Exceptions;
using Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService service;
        public LoanController(ILoanService service)
        {
            this.service = service;
        }

        [HttpGet(nameof(GetUserLoans)+"/{userId}")]
        public async Task<IActionResult> GetUserLoans(int userId)
        {
            try
            {
                var loans = await service.GetUserLoans(userId);
                return Ok(loans);
            }catch(LoanDoesNotExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(AddLoan))]
        public async Task<IActionResult> AddLoan(LoanDTO loan)
        {
            await service.AddLoan(loan);
            return Ok();
        }

        [HttpDelete(nameof(DeleteLoan)+"/{loanId}")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            try
            {
                await service.DeleteLoan(loanId);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoan(LoanDTO loan)
        {
            try
            {
                await service.UpdateLoan(loan);
                return Ok();
            }catch(LoanDoesNotExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
