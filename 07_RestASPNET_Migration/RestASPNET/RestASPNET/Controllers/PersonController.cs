using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestASPNET.Business;
using RestASPNET.Model;

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
            Person person = _personBusiness.FindById(id);
            return (person == null) ? NotFound() : Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            Person createdPerson = _personBusiness.Create(person);
            return (createdPerson == null) ? BadRequest() : Ok(createdPerson);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            Person UpdatedPerson = _personBusiness.Update(person);
            return (UpdatedPerson == null) ? BadRequest() : Ok(UpdatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Ok(_personBusiness.Delete(id));
            
        }
    }
}
