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
    public class CalculatorController : ControllerBase
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
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

        [HttpGet("subtract/{firstNumber}/{secondNumber}")]
        public IActionResult Subtract(string firstNumber, string secondNumber)
        {
            double? numberA = IsNumeric(firstNumber);
            double? numberB = IsNumeric(secondNumber);

            if (numberA != null && numberB != null)
            {
                var result = numberA - numberB;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            double? numberA = IsNumeric(firstNumber);
            double? numberB = IsNumeric(secondNumber);

            if (numberA != null && numberB != null)
            {
                var result = numberA * numberB;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("divison/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            double? numberA = IsNumeric(firstNumber);
            double? numberB = IsNumeric(secondNumber);

            if(numberB.Value == 0)
            {
                return BadRequest("Divisão por 0 não permitida");
            }

            if (numberA != null && numberB != null)
            {
                var result = numberA / numberB;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("avarage/{firstNumber}/{secondNumber}")]
        public IActionResult Average(string firstNumber, string secondNumber)
        {
            double? numberA = IsNumeric(firstNumber);
            double? numberB = IsNumeric(secondNumber);

            if (numberA != null && numberB != null)
            {
                var result = (numberA + numberB)/2;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("squareroot/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {
            double? numberA = IsNumeric(firstNumber);

            if (numberA != null)
            {
                var result = Math.Sqrt(numberA.Value);
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
