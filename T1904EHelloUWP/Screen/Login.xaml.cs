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
using MyService.Entity;
using MyService.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using T1904EHelloUWP.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1904EHelloUWP.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        MemberService memberService = new MemberService();
        public Login()
        {
            this.InitializeComponent();
        }

        private async void Do_Login(object sender, RoutedEventArgs e)
        {
            var email = Username.Text;
            var password = Password.Password;
            var musicAppCredential = await memberService.Login(email, password);
            // lưu vào file.
            Debug.WriteLine($"Login success {musicAppCredential.token}");
        }

        private void Do_Reset(object sender, RoutedEventArgs e)
        {
            Username.Text = string.Empty;
            Password.Password = string.Empty;
        }
    }
}
