using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gg_test_fa.services;

public class FileService: IFileService
{
    private string FilePath = "saveLocation";
    
    public FileService()
    {
        
    }

    public IActionResult SaveFile<T>(T obj, string filename)
    {
        try
        {
            string json = JsonSerializer.Serialize<T>(obj);
            File.WriteAllText($"{filename}", json);
        } catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }

        return new OkResult();
    }
}