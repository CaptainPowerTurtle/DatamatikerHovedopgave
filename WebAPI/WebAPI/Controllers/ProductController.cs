using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "ApiReader")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/values
        [Authorize(Policy = "Bestyrer")]
        [HttpGet]
        public async Task GetAllProducts()
        {

            Response.ContentType = "application/json";
            await using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(Response.BodyWriter);
            await ProductService.getAllProducts(utf8JsonWriter);
        }

        [Authorize(Policy = "Bestyrer")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("User object is null");
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await ProductService.CreateProduct(product);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}