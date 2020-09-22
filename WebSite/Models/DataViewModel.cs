using BusinessLogic.Models.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class DataViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comments { get; set; }

        public string DataUrl { get; set; }

        [AllowedExtension(ErrorMessage = "Invalid File")]       
        public IFormFile File { get; set; }
    }
}
