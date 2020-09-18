using Microsoft.AspNetCore.Http;

namespace WebSite.Models
{
    public class DataViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comments { get; set; }

        public string DataUrl { get; set; }

        public IFormFile File { get; set; }
    }
}
