
using Core.Entities;
using Core.Interfaces;
using Infrastruce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products = await _repository.GetProductsAsync();
          return  Ok(products);
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<Product>> GetProduct(int id){
            return await _repository.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
         public async Task<ActionResult<ProductBrand>> GetProductBrands(){
            return Ok(await _repository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
         public async Task<ActionResult<ProductType>> GetProductTypes(){
            return Ok(await _repository.GetProductTypesAsync());
        }
    }
}