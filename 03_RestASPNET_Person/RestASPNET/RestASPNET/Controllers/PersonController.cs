using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestASPNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            double? numberA = IsNumeric(firstNumber);
            double? numberB = IsNumeric(secondNumber);
            
            if(numberA != null && numberB != null)
            {
                var result = numberA + numberB;
                return Ok(result.ToString());
            }            
            return BadRequest("Invalid input");
        }

        private double? IsNumeric(string strNumber)
        {   
            double convertedNumber;
            if (double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out convertedNumber))
            {
                return convertedNumber;
            }
            else
                return null;
        }
    }
}
