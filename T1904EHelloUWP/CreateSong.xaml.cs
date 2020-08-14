using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using T1904EHelloUWP.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1904EHelloUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
        public CreateSong()
        {
            this.InitializeComponent();
        }

        private void Do_Save(object sender, RoutedEventArgs e)
        {
            var songObject = new
            {
                name = Name.Text,
                author = Author.Text,
                singer = Singer.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(songObject), Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + "5Z5yIukL93C8uBEYY9oJyDGJdxLSC71UjZjmG25iLb3pFkqyL4cCrGmyJH4192y2");    
            var result = 
                httpClient.PostAsync(ApiConfig.CreateSongApi, stringContent).Result.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(result.Result);
            Debug.WriteLine(responseObject["name"]);
            Debug.WriteLine(responseObject["id"]);
        }

        private void Do_Reset(object sender, RoutedEventArgs e)
        {
            Name.Text = string.Empty;
            Singer.Text = string.Empty;
            Author.Text = string.Empty;
            Thumbnail.Text = string.Empty;
            Link.Text = string.Empty;
        }
    }
}
