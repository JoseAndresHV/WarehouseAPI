using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.ApplicationCore.Exceptions;
using WarehouseAPI.ApplicationCore.Interfaces.Services;
using WarehouseAPI.Web.Dtos;
using WarehouseAPI.Web.Models;

namespace WarehouseAPI.Web.Controllers
{
    public class SalesController : BaseApiController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Sales.
        /// </summary>
        /// <returns>A list of Sales</returns>
        /// <response code="200">Returns a list of Sales</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<List<SaleDto>>>> Get()
        {
            var response = new ApiResponse<List<SaleDto>>();

            try
            {
                var data = await _saleService.GetAllSales();
                response.Success = true;
                response.Message = "List of sales obtained successfully.";
                response.Result = _mapper.Map<List<SaleDto>>(data);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Get a Sale.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single Sale</returns>
        /// <response code="200">Returns the Sale</response>
        /// <response code="404">If the Sale was not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SaleDto>>> Get(int id)
        {
            var response = new ApiResponse<SaleDto>();

            try
            {
                var data = await _saleService.GetSaleById(id);
                response.Success = true;
                response.Message = "Sale obtained successfully.";
                response.Result = _mapper.Map<SaleDto>(data);

                return Ok(response);
            }
            catch (SaleNotFoundException ex)
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
        /// Sells a Product.
        /// </summary>
        /// <returns>A created Sale</returns>
        /// <response code="201">Returns the created Sale</response>
        /// <response code="404">If the Product was not found</response>
        /// <response code="400">If there is an invalid field</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SaleDto>>> Post([FromBody] SellProductDto request)
        {
            var response = new ApiResponse<SaleDto>();

            try
            {
                var data = await _saleService.SellProduct(request.ProductId, request.Qty);
                response.Success = true;
                response.Message = "The product was sold successfully.";
                response.Result = _mapper.Map<SaleDto>(data);

                return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
            }
            catch (ProductNotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (InvalidQtyException ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            catch (NotEnoughStockException ex)
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
        /// Refund a Product sale.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The related Sale</returns>
        /// <response code="200">If the Sale was refund and deleted</response>
        /// <response code="404">If the Product was not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SaleDto>>> Delete(int id)
        {
            var response = new ApiResponse<SaleDto>();

            try
            {
                var data = await _saleService.RefundProduct(id);
                response.Success = true;
                response.Message = "The product sale was refund.";
                response.Result = _mapper.Map<SaleDto>(data);

                return Ok(response);
            }
            catch (SaleNotFoundException ex)
            {
                response.Message = ex.Message;
                return NotFound(response);
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
