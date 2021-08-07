using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestASPNET.Business;
using RestASPNET.Data.VO;

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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            return (book == null) ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            var createdPerson = _bookBusiness.Create(book);
            return (createdPerson == null) ? BadRequest() : Ok(createdPerson);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO book)
        {
            var UpdatedPerson = _bookBusiness.Update(book);
            return (UpdatedPerson == null) ? BadRequest() : Ok(UpdatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return _bookBusiness.Delete(id) ? Ok() : NotFound();
            
        }
    }
}
