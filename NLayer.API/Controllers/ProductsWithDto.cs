using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsWithDto : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;

        public ProductsWithDto(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }

        // GET api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productServiceWithDto.GetProductsWithCategory());
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productServiceWithDto.GetAllAsync());
        }

        // GET api/products/{id}
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            return CreateActionResult(await _productServiceWithDto.AddAsync(productCreateDto));
        }

        // PUT api/products
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            return CreateActionResult(await _productServiceWithDto.UpdateAsync(productUpdateDto));
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveAsync(id));
        }
    }
}
