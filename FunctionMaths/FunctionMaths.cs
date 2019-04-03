using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionMaths
{
    public static class FunctionMaths
    {
        [FunctionName("Maths")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Maths Function started");

            String num1 = req.Query["num1"];
            String num2 = req.Query["num2"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            num1 = num1 ?? data?.num1;
            num2 = num2 ?? data?.num2;

            var nnum1 = CommonMaths.ToNullableInt(num1);
            var nnum2 = CommonMaths.ToNullableInt(num2);

            log.LogInformation(string.Format(@"num1= {0} & num2 = {1} goes for addition", num1, num2));

            var sum = nnum1 + nnum2;

            return sum != null
                ? (ActionResult)new OkObjectResult($"Sum of numbers {num1} & {num2} is equals {sum}")
                : new BadRequestObjectResult("Unbale to do Calculation, Please pass num1 & num2 on the query string or in the request body");
        }
    }
}
