using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IMapper mapper, IService<Product> service)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id); // servise id yi gönderip product alıyor
            var productDto = _mapper.Map<ProductDto>(product); // gelen modeli dto ya çeviriyor
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto)); // sonucu gönderiyor
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        // PUT api/products
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto)); 
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        // DELETE api/products/{id}
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
