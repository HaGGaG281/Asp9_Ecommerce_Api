using Asp9_Ecommerce_Api.HelperFunctions;
using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.DTOs.Orders;
using Asp9_Ecommerce_Core.Interfaces;
using Asp9_Ecommerce_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Asp9_Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        // GET: api/items
        [HttpGet("get_items")]
        public async Task<ActionResult<IEnumerable<Item_dto>>> GetItems()
        {
            var items = await _itemsRepository.GetItemsWithUnitsAsync();
            if (items == null)
            {
                return NotFound("items not exists");
            }
            return Ok(items);
        }


        [HttpPost("add/bulk_to_cart")]
        public async Task<IActionResult> AddBulkItemsToCart([FromBody] AddToCartDto dto)
        {
            // Extract the token from the Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            try
            {
                // Extract user ID from the token
                var userId = ExtractClaims.ExtractUserIdFromToken(token);

                if (!userId.HasValue)
                {
                    return Unauthorized("Invalid or missing user ID in token.");
                }

                // Pass the userId to AddItemToCartAsync
                var result = await _itemsRepository.AddBulkQuantityofItemToCartAsync(dto, userId.Value);

                if (result == "Item added to cart successfully.")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid token: " + ex.Message);
            }
        }



        [HttpPost("add/one_to_cart")]
        public async Task<IActionResult> AddOneItemToCart([FromBody] AddToCartDto dto)
        {
            // استخراج الـ token من الـ Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            try
            {
                // استخراج الـ Claims من الـ token
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("Invalid token");
                }

                // استخراج الـ Cus_Id من الـ claim
                var userId = int.Parse(userIdClaim.Value);

                // الآن قم بتمرير الـ userId إلى دالة AddItemToCartAsync
                var result = await _itemsRepository.AddOneQuantityofIteToCartAsync(dto, userId);

                if (result == "Item added to cart successfully.")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid token: " + ex.Message);
            }
        }

    }


}
