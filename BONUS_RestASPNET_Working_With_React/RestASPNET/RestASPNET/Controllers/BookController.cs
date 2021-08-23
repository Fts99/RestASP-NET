using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestASPNET.Business;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<PersonController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(_bookBusiness.FindWithPagedSearch(name,sortDirection,pageSize,page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            return (book == null) ? NotFound() : Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            var createdPerson = _bookBusiness.Create(book);
            return (createdPerson == null) ? BadRequest() : Ok(createdPerson);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            var UpdatedPerson = _bookBusiness.Update(book);
            return (UpdatedPerson == null) ? BadRequest() : Ok(UpdatedPerson);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            return _bookBusiness.Delete(id) ? Ok() : NotFound();
            
        }
    }
}
