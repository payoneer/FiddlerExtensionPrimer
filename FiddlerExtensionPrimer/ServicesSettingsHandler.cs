using Fiddler;
using Newtonsoft.Json;

namespace FiddlerExtensionPrimer
{
    public static class ServicesSettingsHandler
    {
        private const string PrefName = "primer-services-settings";
        public static ServicesSettings GetSettings()
        {
            var str = FiddlerApplication.Prefs.GetStringPref(PrefName, null);
            return str == null ? new ServicesSettings() : JsonConvert.DeserializeObject<ServicesSettings>(str);
        }

        public static void SaveSettings(ServicesSettings setting)
        {
            var str = JsonConvert.SerializeObject(setting);
            FiddlerApplication.Prefs.SetStringPref(PrefName, str);
        }
    }
}
