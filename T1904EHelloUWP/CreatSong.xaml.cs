using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public sealed partial class CreatSong : Page
    {
        public CreatSong()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            var song = new 
            {
                name = name.Text,
                singer = singer.Text,
                author = author.Text,
                thumbnail = thumbnail.Text,
                link = link.Text

            };
            var stringSong = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + "VOA7T2CJ4mHWJuzyzabQqjaCDVA31C4uejA3w7gsKOLNfxiE9IpFKMpNgXAqYJIz");
            var result = client.PostAsync(ApiConfig.CreatSongApi, stringSong).Result.Content.ReadAsStringAsync();
            var response = JObject.Parse(result.Result);
            Debug.WriteLine(response);

        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            name.Text = string.Empty;
            singer.Text = string.Empty;
            author.Text = string.Empty;
            thumbnail.Text = string.Empty;
            link.Text = string.Empty;

        }
    }
}
