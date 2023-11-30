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

   
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest request)
    {

        List<(int, string)> errors = new List<(int, string)>();
        try
        {
            string content = await new StreamReader(request.Body).ReadToEndAsync();
            object[] objectArray = JsonConvert.DeserializeObject<object[]>(content);
            if (objectArray is not null)
            {
                int count = 0;
                foreach (var obj in objectArray)
                {
                    count++;
                    try
                    {
                        Record record = JsonConvert.DeserializeObject<Record>(obj.ToString());
                        var js = JsonSerializer.Serialize(record);
                        _fileService.SaveFile(record, $"{record.Name}.json");
                    }
                    catch (Exception e)
                    {
                        errors.Add(new(count, e.Message));
                    }
                }
            }

        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"This failed: {e.Message}");
        }

        return (errors.Count) switch
        {
            0 => new OkObjectResult("Added: "),
            _ => new BadRequestObjectResult($"Records Failed: {errors.Count} errors: {errors.ToArray()}")
        };
    }
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



206 ----  Result - 2/3 Records updated, record at index failed a invalid Schema

File save


Blob Account/Virtual Folder/FileName.json

xxxxx/LA/xxxxx.json

*/