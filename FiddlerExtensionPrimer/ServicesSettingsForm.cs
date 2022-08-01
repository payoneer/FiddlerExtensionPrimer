using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FiddlerExtensionPrimer
{
    public partial class ServicesSettingsForm : Form
    {
        public ServicesSettingsForm()
        {
            InitializeComponent();
            ServicesSettings servicesSettings = ServicesSettingsHandler.GetSettings();

            richTextBox1.Text = servicesSettings.Services != null ?
                string.Join("\n", servicesSettings.Services.Select(q => q.Name)) :
                string.Empty;
        }

        private void ServicesSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //split the text by lines
            var lines = richTextBox1.Text.Split(new[] { "\n"}, StringSplitOptions.RemoveEmptyEntries);

            // get the current settings
            ServicesSettings servicesSettings = ServicesSettingsHandler.GetSettings();
            // get a list of the currently active services, so we will not lose it 
            List<string> activeServices = servicesSettings.Services
                .Where(s => s.IsActive).Select(s => s.Name).ToList();

            List<ServiceModel> newList = new List<ServiceModel>();
            //create a list ServiceModel for each line in the text editor
            //if there is already an active service with this name it should still be active
            foreach (var line in lines)
            {
                newList.Add(new ServiceModel
                {
                    Name = line,
                    IsActive = activeServices.Contains(line)
                });
            }
            //save it to the settings 
            ServicesSettingsHandler.SaveSettings(new ServicesSettings
            {
                Services = newList
            });
        }
    }
}
