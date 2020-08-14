using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using MyService.Service;
using T1904EHelloUWP.Entity;
using T1904EHelloUWP.Dialog;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1904EHelloUWP.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        readonly MemberService _memberService = new MemberService();
        readonly ImageService _imageService = new ImageService();
        private string _imageUrl;

        public Register()
        {
            this.InitializeComponent();
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var confirmDialog = new CofirmDialog();
            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary) {
                var member = new Member
                {
                    firstName = firstName.Text,
                    lastName = lastName.Text,
                    password = password.Text,
                    address = address.Text,
                    phone = phone.Text,
                    avatar = _imageUrl,
                    gender = Int32.Parse(gender.Text),
                    email = email.Text,
                    birthday = birthday.Text
                };
                var isValid = true; // validate dữ liệu
                if (isValid)
                {
                    Member createdMember = await _memberService.Register(member);
                }
            }
            else if (result == ContentDialogResult.Secondary)
            {
            }

        }

        private async void TakePhoto(object sender, RoutedEventArgs e)
        {
            var cameraCaptureUi = new CameraCaptureUI();
            // chờ người dùng chụp thành công (ng dùng có thể tắt camera)
            var photo = await cameraCaptureUi.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo == null)
            {
                // dừng lại trong trường hợp không chụp ảnh
                return;
            }
            var imageStream = await photo.OpenStreamForReadAsync();
            var imageUrl = await _imageService.UploadPhoto(imageStream);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return;
            }
            // hiển thị ảnh.
            Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
            _imageUrl = imageUrl;
        }

        private void EmailTip_Click(object sender, RoutedEventArgs e)
        {
            AutoSaveTip.IsOpen = true;
        }
    }
}
