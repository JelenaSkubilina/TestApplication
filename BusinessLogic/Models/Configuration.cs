namespace BusinessLogic.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        public int ConfigurationTypeId { get; set; }
        
        public string Value { get; set; }

        public ConfigurationType ConfigurationType { get; set; }
    }
}
