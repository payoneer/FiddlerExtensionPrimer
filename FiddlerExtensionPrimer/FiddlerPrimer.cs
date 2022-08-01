using Fiddler;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;


[assembly: Fiddler.RequiredVersion("4.0.0.0")]
namespace FiddlerExtensionPrimer
{
    public class FiddlerPrimer : IAutoTamper
    {
        private MenuItem _serviceMenu;

        private MenuItem CreateServiceMenu()
        {
            var menu = new MenuItem("&Services");
            PopulateServiceMenu(menu);
            return menu;
        }
        private void RefreshMenu()
        {
            _serviceMenu.MenuItems.Clear();
            PopulateServiceMenu(_serviceMenu);
        }

        private void PopulateServiceMenu(MenuItem menu)
        {
            var settings = ServicesSettingsHandler.GetSettings();

            foreach (var route in settings.Services)
            {
                var item = new MenuItem(route.Name);
                if (route.IsActive)
                {
                    item.Checked = true;
                }

                item.Click += (sender, args) =>
                {
                    item.Checked = !item.Checked;
                    route.IsActive = item.Checked;
                    ServicesSettingsHandler.SaveSettings(settings);
                };
                menu.MenuItems.Add(item);
            }

            menu.MenuItems.Add("Edit...",
                (sender, args) =>
                {
                    var form = new ServicesSettingsForm
                    {
                        Parent = FiddlerApplication.UI.ParentForm,
                        StartPosition = FormStartPosition.CenterParent
                    };
                    form.ShowDialog(FiddlerApplication.UI.ParentForm);
                    RefreshMenu();
                });
        }
        public void OnLoad()
        {
            if (FiddlerApplication.IsViewerMode)
            {
                return;
            }
            _serviceMenu = CreateServiceMenu();
            FiddlerApplication.UI.mnuMain.MenuItems.Add(_serviceMenu);
        }

        public void OnBeforeUnload()
        {

        }

        public void AutoTamperRequestBefore(Session oSession)
        {
            var settings = ServicesSettingsHandler.GetSettings();

            if (!settings.Services.Any(s => s.IsActive))
            {
                return;
            }

            var activeServices = settings.Services.Where(s => s.IsActive).Select(s => s.Name);
            var names = string.Join(",", activeServices);

            //Get the IP of the PC
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var IP = host.AddressList
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                .Select(ip => ip.ToString())
                .OrderBy(o => o)
                .FirstOrDefault();

            oSession.RequestHeaders.Add("x-service-debug-redirect", $"{names};{IP}");
        }

        public void AutoTamperRequestAfter(Session oSession)
        {

        }

        public void AutoTamperResponseBefore(Session oSession)
        {

        }

        public void AutoTamperResponseAfter(Session oSession)
        {

        }

        public void OnBeforeReturningError(Session oSession)
        {

        }
    }
}
