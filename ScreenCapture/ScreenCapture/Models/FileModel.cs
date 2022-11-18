using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class FileModel
{
    [Required(ErrorMessage = "Please enter file name")]
    public string FileName { get; set; }
    [Required(ErrorMessage = "Please select file")]
    public IFormFile File { get; set; }
}