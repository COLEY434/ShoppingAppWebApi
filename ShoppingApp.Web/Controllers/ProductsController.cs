﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingApp.Core.DTOs.Request;
using ShoppingApp.Core.DTOs.Response;
using ShoppingApp.Core.Interfaces;
using ShoppingApp.Core.Interfaces.Services;
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
        public async Task<ActionResult> AddProductsAsync([FromBody] ProductRequestDto requestDto)
        {
            try
            {
                var result = await _productService.AddProductAsync(requestDto);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Product Created Successfully" });
                }

                return StatusCode(500, new { Success = false, Message = "Error adding product"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Error adding product" });
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
                return StatusCode(500, new { Success = false, Message = "Error getting product" });
            }

        }
    }
}
