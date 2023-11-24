using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace gg_test_fa.triggers;

public class UploadRecord
{
    public UploadRecord()
    {
        
    }

    [FunctionName("upload")]
    
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
    {
        return new ObjectResult("This was hit ok");
    }
}