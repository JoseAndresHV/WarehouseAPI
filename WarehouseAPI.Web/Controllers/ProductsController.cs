using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.ApplicationCore.Exceptions;
using WarehouseAPI.ApplicationCore.Interfaces.Services;
using WarehouseAPI.Web.Dtos;
using WarehouseAPI.Web.Models;

namespace WarehouseAPI.Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Products.
        /// </summary>
        /// <returns>A list of Products</returns>
        /// <response code="200">Returns a list of Products</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<List<ProductDto>>>> Get()
        {
            var response = new ApiResponse<List<ProductDto>>();

            try
            {
                var data = await _productService.GetAllProducts();
                response.Success = true;
                response.Message = "List of products obtained successfully.";
                response.Result = _mapper.Map<List<ProductDto>>(data);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Get a Product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single Product</returns>
        /// <response code="200">Returns the Product</response>
        /// <response code="404">If the Product was not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Get(int id)
        {
            var response = new ApiResponse<ProductDto>();

            try
            {
                var data = await _productService.GetProductById(id);
                response.Success = true;
                response.Message = "Product obtained successfully.";
                response.Result = _mapper.Map<ProductDto>(data);

                return Ok(response);
            }
            catch (ProductNotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Creates a Product.
        /// </summary>
        /// <returns>A newly created Product</returns>
        /// <response code="201">Returns the created Product</response>
        /// <response code="400">If the Product is null or there is an invalid field</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Post([FromBody] CreateProductDto product)
        {
            var response = new ApiResponse<ProductDto>();

            try
            {
                var data = await _productService.CreateProduct(_mapper.Map<Product>(product));
                response.Success = true;
                response.Message = "The product was created successfully.";
                response.Result = _mapper.Map<ProductDto>(data);

                return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
            }
            catch (InvalidStockException ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (InvalidPriceException ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Updates a Product.
        /// </summary>
        /// <returns>An updated Product</returns>
        /// <response code="200">Returns the updated Product</response>
        /// <response code="400">If the Product is null or there is an invalid field</response>
        /// <response code="404">If the Product was not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Put([FromBody] ProductDto product)
        {
            var response = new ApiResponse<ProductDto>();

            try
            {
                var data = await _productService.UpdateProduct(_mapper.Map<Product>(product));
                response.Success = true;
                response.Message = "The product was updated successfully.";
                response.Result = _mapper.Map<ProductDto>(data);

                return Ok(response);
            }
            catch (MissingIdException<Product> ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (InvalidStockException ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (InvalidPriceException ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (ProductNotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Deletes a Product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">If the Product was deleted</response>
        /// <response code="404">If the Product was not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Delete(int id)
        {
            var response = new ApiResponse<ProductDto>();

            try
            {
                await _productService.DeleteProduct(id);
                response.Success = true;
                response.Message = "The product was deleted successfully.";

                return Ok(response);
            }
            catch (ProductNotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
