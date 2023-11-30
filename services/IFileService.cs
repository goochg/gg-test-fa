using Microsoft.AspNetCore.Mvc;

namespace gg_test_fa.services;

public interface IFileService
{
    public IActionResult SaveFile<T>(T obj, string filename);

}