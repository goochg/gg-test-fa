using System.ComponentModel.DataAnnotations;

namespace gg_test_fa.models;

public class Record
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Age is a requited field.")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Surname is a required field")]
    public string Surname { get; set; }
}