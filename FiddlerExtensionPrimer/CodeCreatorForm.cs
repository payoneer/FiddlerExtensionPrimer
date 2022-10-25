using Fiddler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FiddlerExtensionPrimer
{
    public partial class CodeCreatorForm : Form
    {
        public CodeCreatorForm(Session session)
        {
            InitializeComponent();

            if (session != null)
            {
                try
                {
                    CreateRequest(session);
                }
                catch (Exception e)
                {
                    rtbCode.Text = e.Message;
                }
            }
        }

        private void btnCopyToClipBoard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbCode.Text);
        }

        private static string CapitalizeFirstLetter(string text) => text.First().ToString().ToUpper() + text.Substring(1);

        public void CreateRequest(Session session)
        {
            var urlParts = session.fullUrl.Split('?');
            var host = urlParts[0];
            var queryString = urlParts.Length > 1 ? urlParts[1] : null;

            host += string.IsNullOrWhiteSpace(queryString) ? string.Empty : ("?" + queryString);

            //Create request
            var list = new List<string>
            {
                "var request = new HttpRequestMessage",
                "{",
                $"  RequestUri = new Uri(\"{host}\"),",
                $"  Method = HttpMethod.{CapitalizeFirstLetter( session.RequestMethod.ToLower())}",
                "};",
                Environment.NewLine
            };

            var customHeaderNames = new List<string> {   "Content-Length", "Content-Type" };
            var headers = new Dictionary<string, string>();

            foreach (var header in session.RequestHeaders)
            {
                if (!customHeaderNames.Contains(header.Name))
                {
                    headers.Add(header.Name, header.Value);
                }
            }

            //headers
            foreach (var header in headers)
            {
                list.Add($" request.Headers.Add(\"{header.Key}\", \"{header.Value}\");");
            }
            var reqBody = session.GetRequestBodyAsString();
            if (!string.IsNullOrWhiteSpace(reqBody))
            {
                list.Add($" request.Content = new StringContent(\"{reqBody}\");");

                var contentType = session.RequestHeaders["Content-Type"] ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    list.Add($" request.Content.Headers.ContentType = new MediaTypeHeaderValue(\"{contentType}\");");
                }
            }

            //send request
            list.Add(Environment.NewLine);


            list.Add("var httpClient = new HttpClient();");
            list.Add("var response = await httpClient.SendAsync(request);");
            list.Add("if (response.IsSuccessStatusCode)");
            list.Add("{");
            list.Add("    var result = await response.Content.ReadAsStringAsync();");
            list.Add("}");

            rtbCode.Text = string.Join(Environment.NewLine, list.Select(s => $"   {s}"));
        }
    }
}
