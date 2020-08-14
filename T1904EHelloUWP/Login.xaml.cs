using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using T1904EHelloUWP.Entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Authentication.Web.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1904EHelloUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            var account = new Account()
            {
                email = email.Text,
                password = password.Text
            };
            /*
                var account = new {
                    email = email.Text,
                    password = password.Text
                };
             */
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var result = client.PostAsync(ApiConfig.LoginApi, content).Result.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result.Result);// generic type - tham số hóa kiểu dữ liệu
            Debug.WriteLine(tokenResponse.token);


            /* 
             * var tokenResponse = JObject.Parse(result.Result);
            Debug.WriteLine(tokenResponse["token"]); 
            */



        }
        private void Button_Click_Reset(object sender, RoutedEventArgs e) {
            email.Text = string.Empty;
            password.Text = string.Empty;
        }
    }
    class TokenResponse {
        public string token { get; set; }
        public string secretToken { get; set; }
        public string userID { get; set; }
        public string createdTimeMLS { get; set; }
        public string expridTimeMLS { get; set; }

        public int Status { get; set; }
    }
}
