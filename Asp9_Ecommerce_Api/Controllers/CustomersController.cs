using Asp9_Ecommerce_Api.HelperFunctions;
using Asp9_Ecommerce_Core.Interfaces;
using Asp9_Ecommerce_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp9_Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        [HttpGet("get/cart/all")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var customerId = ExtractClaims.ExtractUserIdFromToken(token);

            if (!customerId.HasValue)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var receipt = await customersRepository.GetAllCartItemsAsync(customerId.Value);

            if (receipt == null)
            {
                return NotFound("Invoice not found or does not belong to the customer.");
            }

            return Ok(receipt);
        }

        [HttpGet("invoices/receipt")]
        public async Task<IActionResult> GetInvoiceReceipt(int invoiceId)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var customerId = ExtractClaims.ExtractUserIdFromToken(token);

            if (!customerId.HasValue)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var receipt = await customersRepository.CalculateInvoiceTotalAsync(invoiceId, customerId.Value);

            if (receipt == null)
            {
                return NotFound("Invoice not found or does not belong to the customer.");
            }

            return Ok(receipt);
        }


        [HttpPost("invoices/create")]
        public async Task<IActionResult> CreateInvoiceForCustomer()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }
            var userId = ExtractClaims.ExtractUserIdFromToken(token);

            if (!userId.HasValue)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var result = await customersRepository.CreateInvoiceAsync(userId.Value);

            if (result.StartsWith("Invoice created successfully"))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



    }

}
