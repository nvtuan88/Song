using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using T1904EHelloUWP.Entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1904EHelloUWP.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        public Profile()
        {
            this.InitializeComponent();
            GetInformation();
        }

        private async void GetInformation()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic" + "u7uWREcvPAWI3qrZ9QYMj75645s9z1xoH020IRZWPpKjHQc7Lv6ztJHCXPITKk14");
            var httpResponseMessage = await client.GetAsync(new Uri(ApiConfig.MemberInformationApi));
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseContent);
            FirstName.Text = (string)jsonObject["firstName"];
            LastName.Text = (string)jsonObject["lastName"];
            Email.Text = (string)jsonObject["email"];
            Avatar.Source = new BitmapImage(new Uri((string)jsonObject["avatar"], UriKind.Absolute));
            Phone.Text = (string)jsonObject["phone"];
            Address.Text = (string)jsonObject["address"];
            JToken intro = jsonObject["introduction"];
            if (intro != null && intro.HasValues)
            {
                Introduction.Text = (string)intro;
            }
            Birthday.Text = (string)jsonObject["birthday"];
            Gender.Text = int.Parse((string)jsonObject["gender"]) == 1 ? "Male" : "Female";

            Debug.WriteLine("Test");
        }

    }
}
