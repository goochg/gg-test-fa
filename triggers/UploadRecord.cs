using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using gg_test_fa.models;
using gg_test_fa.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace gg_test_fa.triggers;

public class UploadRecord
{
    private IFileService _fileService;
    public UploadRecord(IFileService fileService)
    {
        _fileService = fileService;
    }

    [FunctionName("upload")]

    /*public async Task<IActionResult> Run2([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
    {
        Record record = new Record();
        try
        {
            string content = await new StreamReader(request.Body).ReadToEndAsync();
            record = JsonConvert.DeserializeObject<Record>(content);
            _fileService.SaveFile(record, $"{record.Name}.json");

        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"This failed: {e.Message}");
        }

        return new ObjectResult($"This was hit ok. {record.Name} is {record.Age}.");
    }*/

    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
    {
        return new OkObjectResult("Function App hit");
    }

    /*
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
   {

       List<string> times = new();
        List<(int, string)> errors = new List<(int, string)>();
        try
        {
            string content = await new StreamReader(request.Body).ReadToEndAsync();
            object[] objectArray = JsonConvert.DeserializeObject<object[]>(content);
            if (objectArray is not null && objectArray.Length > 0)
            {
                int count = 0;
                foreach (var obj in objectArray)
                {
                    try
                    {
                        Record record = JsonConvert.DeserializeObject<Record>(obj.ToString());
                        var js = JsonSerializer.Serialize(record);
                        _fileService.SaveFile(record, $"{record.Name}.json");
                        var noo = DateTime.Now.ToFileTimeUtc();
                        //"yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK
                        times.Add($"{record.Name}--{DateTime.FromFileTimeUtc(noo).ToString("yyyy-MMM-dd:HHmmss.fffff")}");
                        count++;
                    }
                    catch (Exception e)
                    {
                        errors.Add(new(count, e.Message));
                    }
                }
            }
            else
            {
                return new BadRequestObjectResult("Function App>> Null or empty content");
            }

        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"Function App>> This failed: {e.Message}");
        }

        return (errors.Count) switch
        {
            0 => new OkObjectResult($"Function App>> Added: {string.Join("/n", times)}"),
            _ => new BadRequestObjectResult($"Function App>> Records Failed: {errors.Count} errors: {errors.ToArray()}")
        };
    }
*/
}

/*
[
{object},
{object},
{object}
]


[
{OK},
{BAD},
{OK}
]

--133471078237795032
--133471078237800216

--133471085621731956---2023-Dec-15:100922.17319/n
--133471085621839231---2023-Dec-15:100922.18392


206 ----  Result - 2/3 Records updated, record at index failed a invalid Schema

File save


Blob Account/Virtual Folder/FileName.json

xxxxx/LA/xxxxx.json

*/