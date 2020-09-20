namespace WebSite.Models
{
    public class ConfigurationListViewModel
    {
        public int Id { get; set; }

        public int ConfigurationTypeId { get; set; }

        public string ConfigurationType { get; set; }

        public string Value { get; set; }
    }
}
