using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            RunDownloadSync();

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
           
            await RunDownloadParallelAsync();
            
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;
            
            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }


        private List<string> PrepData()
        {
            List<string> output = new List<string>{};

            resultsWindow.Text = "";

            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.stackoverflow.com");
            output.Add("https://www.apple.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.yahoo.com");

            return output;
        }

        private void RunDownloadSync()
        {

            List<string> websites = PrepData();

            foreach (string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                ReportWebsiteInfo(results);
            }
        }

        private async Task RunDownloadAsync()
        {
            List<string> websites = PrepData();

            foreach (string site in websites)
            {
                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                ReportWebsiteInfo(results);
            }
        }

        private async Task RunDownloadParallelAsync()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                //tasks.Add(Task.Run(() => DownloadWebsite(site)));
                tasks.Add(DownloadWebsiteAsync(site));
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }

        private WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.websiteUrl = websiteURL;
            output.websiteData = client.DownloadString(websiteURL);

            return output;
        }

        private async Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.websiteUrl = websiteURL;
            output.websiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            resultsWindow.Text += $"{data.websiteUrl} downloaded: {data.websiteData.Length} characters long.{Environment.NewLine}";
        }
    }
}
