using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using CefSharp;
using System.IO;
using System.Net;

namespace LB5
{
    public partial class MainWindow : Window
    {
        public string Access_token { get; set; }
        public string UserID { get; set; }
        string appId = "51788560";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uriStr = @"https://oauth.vk.com/authorize?client_id=" + appId +
                    @"&scope=offline,friends&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.6&response_type=token";
            wb.AddressChanged += BrowserOnNavigated;
            wb.Load(uriStr);
        }
        private void BrowserOnNavigated(object sender, DependencyPropertyChangedEventArgs e)
        {
            var uri = new Uri((string)e.NewValue);
            if (uri.AbsoluteUri.Contains(@"oauth.vk.com/blank.html#"))
            {
                string url = uri.Fragment;
                url = url.Trim('#');
                Access_token = HttpUtility.ParseQueryString(url).Get("access_token");
                UserInfo(Access_token);
            }
        }
        public static string GET(string Url, string Method, string Token)
        {
            WebRequest req = WebRequest.Create(string.Format(Url, Method, Token));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }
        private static async void UserInfo(string Access_token)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getProfileInfo";
            string f = GET(reqStrTemplate, method, Access_token);
            var user = JsonSerializer.Deserialize<Model.Rootobject>(f).response;
            string[] list =
            {
                "id: " + user.id.ToString(),
                "status: " + user.status,
                "lastname: " + user.last_name,
                "firstname: " + user.first_name,
                "birth date: " + user.bdate,
                "city: " + user.city.title,
                "phone number: " + user.phone,
                "sex: " + user.sex.ToString(),
                "country: " + user.country.title
            };
            MessageBox.Show(string.Join("\n", list));
        }
    }
}