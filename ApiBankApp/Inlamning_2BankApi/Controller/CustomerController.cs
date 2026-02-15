using AutoMapper;
using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiBankApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public CustomerController(IMapper mapper, IAccountService accountService, ICustomerService customerService, ITransactionService transactionService, IUserService userService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _customerService = customerService;
            _transactionService = transactionService;
            _userService = userService;
        }

        [Authorize(Roles = "Customer")] 
        [HttpGet("myaccounts")] 
        public async Task<IActionResult> GetMyAccounts()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user.CustomerId is null)
                return NotFound("You have no customer.");

            var accounts = await _accountService.GetAccountsForCustomerAsync((int)user.CustomerId);

            if (!accounts.Any())
                return NotFound("You have no accounts.");

            var accountDtos = _mapper.Map<List<AccountDTO>>(accounts);
            return Ok(accountDtos);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("mytransactions/{accountId}")]
        public async Task<IActionResult> GetMyTransactions(int accountId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _userService.GetUserByIdAsync(userId);

            if (user.CustomerId is null)
                return BadRequest("Customer not found");

            var transactions = await _transactionService.GetTransactionsForAccountAsync(accountId,(int)user.CustomerId);

            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for this account.");

            var transactionDtos = _mapper.Map<List<TransactionDTO>>(transactions);
            return Ok(transactionDtos);
        }

       
        [Authorize(Roles = "Customer")]
        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO createAccountDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userService.GetUserByIdAsync (userId);

            if (user.CustomerId == null)
                return NotFound("Customer not found.");

            var customer = await _customerService.GetCustomerByIdAsync((int)user.CustomerId);
            
            var account = _mapper.Map<Account>(createAccountDTO);

            var createdAccount = await _userService.CreateCustomerAccountAsync(account, customer);
            if (createdAccount is null)
                return BadRequest("Failed to create account.");

            // Mappa till DTO
            var createdAccountDTO = _mapper.Map<AccountDTO>(createdAccount);

            return Created("", createdAccountDTO); 
        }

       
        [Authorize(Roles = "Customer")]
        [HttpPost("createtransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO createTransactionDTO)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userService.GetUserByIdAsync(userId);

            var customer = await _customerService.GetCustomerByIdAsync(user.CustomerId ?? 0);
            if (customer == null)
                return NotFound("Customer not found.");

            var createdTransaction = await _userService.CreateCustomerTransactionAsync(createTransactionDTO, customer);
            if (createdTransaction is null)
                return BadRequest("Failed to create transaction.");

            // Mappa till DTO
            var createdTransactionDTO = _mapper.Map<TransactionDTO>(createdTransaction);

            return Created("", createdTransactionDTO);
        }
    }
}
