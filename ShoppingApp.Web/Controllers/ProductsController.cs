using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingApp.Core.DTOs.Request;
using ShoppingApp.Core.DTOs.Response;
using ShoppingApp.Core.Interfaces;
using ShoppingApp.Core.Interfaces.Services;
using ShoppingApp.Web.Constants;
using ShoppingApp.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("AddProducts")]
        [ValidateModel]
        public async Task<ActionResult> AddProductAsync([FromBody] ProductRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation($"Adding a new product for User...");
                await _productService.AddProductAsync(requestDto);

                return Ok(new Response 
                { 
                    Message = ProductConstant.SuccessMessage,
                    Success = true,
                    Code = ProductConstant.SuccessCode
                });               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new Response
                {
                    Success = false,
                    Message = ProductConstant.ErrorMessage,
                    Code = ProductConstant.ErrorCode
                });
            }
            
        }
        [HttpPut("{id}/Update")]
        [ValidateModel]
        public async Task<ActionResult> UpdateProductAsync([FromBody] ProductRequestDto requestDto, [FromRoute] int id) 
        {
            try
            {
                _logger.LogInformation("Update product {Product name} {product id} for {User}...");
                var result = await _productService.UpdateProductAsync(requestDto, id);

                if (result)
                {
                    return Ok(new Response
                    {
                        Message = ProductConstant.SuccessUpdateMessage,
                        Success = true,
                        Code = ProductConstant.SuccessCode
                    });
                }

                return Ok(new Response
                {
                    Message = ProductConstant.BadRequestMessage,
                    Success = true,
                    Code = ProductConstant.BadRequesCode
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new Response
                {
                    Success = false,
                    Message = ProductConstant.ErrorMessage,
                    Code = ProductConstant.ErrorCode
                });
            }

        }
        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProductsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                var result = await _productService.GetAllProductsAsync();
                          
                return Ok(result);                          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new Response
                {
                    Success = false,
                    Message = ProductConstant.ErrorMessage,
                    Code = ProductConstant.ErrorCode
                });
            }

        }

        [HttpDelete("{id}/Delete")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Deleting product {Product name} {product id} by {User}...");
                var result = await _productService.DeleteProductAsync(id);

                if (result)
                {
                    return Ok(new Response
                    {
                        Message = ProductConstant.SuccessDeleteMessage,
                        Success = true,
                        Code = ProductConstant.SuccessCode
                    });
                }

                return Ok(new Response
                {
                    Message = ProductConstant.BadRequestMessage,
                    Success = true,
                    Code = ProductConstant.BadRequesCode
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new Response
                {
                    Success = false,
                    Message = ProductConstant.ErrorMessage,
                    Code = ProductConstant.ErrorCode
                });
            }

        }
    }
}
