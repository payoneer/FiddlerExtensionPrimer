using System.Collections.Generic;

namespace FiddlerExtensionPrimer
{
    public class ServiceModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class ServicesSettings
    {
        public List<ServiceModel> Services { get; set; } = new List<ServiceModel>();
    }
}
