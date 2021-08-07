using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestASPNET.Business;
using RestASPNET.Data.VO;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            return (person == null) ? NotFound() : Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonVO person)
        {
            var createdPerson = _personBusiness.Create(person);
            return (createdPerson == null) ? BadRequest() : Ok(createdPerson);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PersonVO person)
        {
            var UpdatedPerson = _personBusiness.Update(person);
            return (UpdatedPerson == null) ? BadRequest() : Ok(UpdatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return _personBusiness.Delete(id) ? Ok() : NotFound();
            
        }
    }
}
