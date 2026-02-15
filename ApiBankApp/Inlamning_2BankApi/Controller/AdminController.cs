using AutoMapper;
using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBankApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ILoanService _loanService;
       
        public AdminController(
            IMapper mapper,
            IUserService userService,
            ICustomerService customerService,
            IAccountService accountService,
            ILoanService loanService
            )
        {
            _mapper = mapper;
            _userService = userService;
            _customerService = customerService;
            _loanService = loanService;
            _accountService = accountService;
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("AddCustomer")] 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO cDTO)
        {
            //Mappa DTO → Entitiy
            var customer = _mapper.Map<Customer>(cDTO);
            
            //Spara customer
            var createdCustomer = await _customerService.CreateCustomerAsync(customer);

            // Checka om skapandet lyckades
            if (createdCustomer == null)
            return BadRequest("Could not create customer.");

            // Mappa entity → DTO
            var customerDTO = _mapper.Map<CustomerDTO>(createdCustomer);

            //Returnera skapad customerId
            return Created("", customerDTO.CustomerId);
        }

        //Skapar användarkonto och bankkonto åt kund
        [HttpPost("AddUser")] 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserAccountDTO uDTO)
        {   
            //Hämta customer
            var customer = await _customerService.GetCustomerByIdAsync(uDTO.CustomerId);
            if (customer is null)
                return NotFound("Couldn't find customer");

            //Mappa DTO → Entities
            var user = _mapper.Map<User>(uDTO);
            var account = _mapper.Map<Account>(uDTO);

            //Sätt relationerna
            user.Role = "Customer";
            user.Customer = customer;

            //Skapa och spara användare och bankkonto
            var createdUser = await _userService.CreateUserAccountAsync(user, account);

            if (createdUser == null)
                return BadRequest("Could not create user account.");

            // Mappa entity → DTO
            var userDTO = _mapper.Map<UserDTO>(createdUser);

            return Created("", userDTO.CustomerId);
        }

        
        [HttpPost("loan")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanDTO loanDTO)
        {   
            //Kontrollera att kund finns
            var customer = await _customerService.GetCustomerByIdAsync(loanDTO.customerId);
            if (customer == null)
                return NotFound("Couldn't find customer");

            // Hämta kontot för att få kundinfo
            var account = await _accountService.GetAccountIfOwnedByCustomerAsync(loanDTO.AccountId, loanDTO.customerId);
            if (account == null)
                return BadRequest("Couldn't find account or customer doesn't own account");

            // Mappa DTO → Entity
            var loan = _mapper.Map<Loan>(loanDTO);
            
            // Spara lånet
            var createdLoan = await _loanService.CreateLoanAsync(loan);
            if (createdLoan is null)
                return BadRequest("Could not create loan.");

            // Mappa entity → DTO
            var loanResponseDTO = _mapper.Map<LoanResponseDTO>(createdLoan);

            return Created("", loanResponseDTO);
        }

        //Översikt över kundkonton (behövs för att reda på var lånet ska in).
        [HttpGet("customer/{customerId}/accounts")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAccountsForCustomer(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return NotFound("Customer not found.");


            var accounts = await _accountService.GetAccountsForCustomerAsync(customerId);
            if (accounts == null || accounts.Count == 0)
                return NotFound("No accounts found for the specified customer.");


            var accountDTOs = _mapper.Map<List<AccountDTO>>(accounts);
            return Ok(accountDTOs);
        }
    } 
}
