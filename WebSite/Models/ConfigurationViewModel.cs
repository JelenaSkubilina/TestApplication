using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebSite.Models
{
    public class ConfigurationViewModel
    {
        public int Id { get; set; }

        public int ConfigurationTypeId { get; set; }

        public string ConfigurationType { get; set; }

        public IEnumerable<SelectListItem> ConfigurationTypes { get; set; }

        public string Value { get; set; }
    }
}
