using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

public class FileModel
{
    internal string fileName;

    //[Required(ErrorMessage = "Please enter file name")]
    //public string FileName { get; set; }
    //[Required(ErrorMessage = "Please select file")]
    //public IFormFile File { get; set; }
    public string ImageData { get; set; }
    public bool IsPublished { get; internal set; }
    public string UserName { get; internal set; }
    public int ViewCount { get; internal set; }
    public DateTime CreatedDateTime { get; internal set; }
    //public string Name { get; set; }
    //public string ImageData { get; set; }
}