using System.Net;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using MediatR;

using RoyalLibrary.Features.Features.Books.Query;

namespace RoyalLibrary.Api.Controllers.Books
{
    [Produces("application/json")]
    [Route("api/books")]
    [ApiController]
    public class BooksController(IMediator mediator) : ControllerBase
    {

        // GET api/<BookController>
        ///<summary>
        /// Get Book
        ///</summary>
        ///<remarks>
        /// This endpoint will allow you to get all book information. You may pass URL parameters to these endpoints to navigate to get all  books.
        ///</remarks>
        /// <param name="request">Search request</param>
        /// <response code="200">Return all tenants.</response>
        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAllBookResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAllBookResponse>> GetAllTenantByIdAsync([FromQuery] GetBookByFilterRequest request)
        {
            var response = await mediator.Send(new GetAllBookByQuery
            {
              
                FilterRequest = new GetBookByFilterRequest
                {
                    Page = request.Page <= 0 ? 1 : request.Page,
                    PageSize = request.PageSize,
                    SearchBy = request.SearchBy,
                    SearchValue = request.SearchValue
                }
            });
            return Ok(response);
        }
    }
}
