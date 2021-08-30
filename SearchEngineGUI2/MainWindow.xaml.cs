using SearchEngineGUI2.DataTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchEngineGUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient client;
        public MainWindow()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            InitializeComponent();
        }

        private static async Task SearchQuery(string query, ListView results)
        {
            string url = "https://localhost:5001/api/search?query=" + query;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "FOOGLE SEARCH ENGINE");

            var streamTask = client.GetStreamAsync(url);
            var response = await JsonSerializer.DeserializeAsync<SearchResponse>(await streamTask);
            results.Items.Clear();
            foreach (KeywordsDocument document in response.documents)
            {
                Hyperlink link = new Hyperlink
                {
                    NavigateUri = new Uri(document.documentLink),
                    
                };
                Run run = new Run();
                run.Text = document.documentName;
                link.Inlines.Add(run);
                MessageBox.Show(document.documentLink);
                link.RequestNavigate += (sender, e) =>
                {
                    var url = e.Uri.ToString();
                    Process.Start(new ProcessStartInfo(url)
                    {
                        UseShellExecute = true
                    });
                };
                ListViewItem listItem = new ListViewItem
                {
                    Content = link,
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var item = results.Items.Add(listItem);
            }

        }
        private async void Search(object sender, RoutedEventArgs e)
        {
            string query = searchBox.Text;
            if (query.Length == 0)
            {
                MessageBox.Show("Search can not be empty");
                return;
            }
            await SearchQuery(query, results);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //results.View = View.Details;
        }
    }
}
