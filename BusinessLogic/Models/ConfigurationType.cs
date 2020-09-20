using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class ConfigurationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Configuration> Configurations { get; set; }
        public ConfigurationType()
        {
            Configurations = new List<Configuration>();
        }

    }
}
