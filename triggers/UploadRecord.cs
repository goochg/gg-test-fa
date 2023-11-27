using System;
using System.IO;
using System.Threading.Tasks;
using gg_test_fa.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace gg_test_fa.triggers;

public class UploadRecord
{
    public UploadRecord()
    {
        
    }

    [FunctionName("upload")]
    
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
    {
        Record record = new Record();
        try
        {
            string content = await new StreamReader(request.Body).ReadToEndAsync();
            record = JsonConvert.DeserializeObject<Record>(content);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"This failed: {e.Message}");
        }

        return new ObjectResult($"This was hit ok. {record.Name} is {record.Age}.");
    }

   
}