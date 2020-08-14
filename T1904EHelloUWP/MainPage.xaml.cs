using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using T1904EHelloUWP.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Windows.Storage;
using Windows.Media.Capture;
using System.Net;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace T1904EHelloUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFile photo;
        private string _imageUrl;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            var member = new Member()
            {
                firstName = firstName.Text,
                lastName = lastName.Text,
                password = password.Text,
                address = address.Text,
                phone = phone.Text,
                email = email.Text,
                avatar = _imageUrl,
                gender = Int32.Parse(gender.Text),
                birthday = birthday.Text
            };
            var isValid = true; // validate dữ liệu
            if (isValid) {
                var contentJson = JsonConvert.SerializeObject(member);
                var client = new HttpClient();
                var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
                var respone = client.PostAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/members", content).Result;
                Debug.WriteLine(respone.Content.ReadAsStringAsync().Result);
            }
            

        }

        private void Button_Click_upload(object sender, RoutedEventArgs e)
        {
            GetPhoto();
        }

        private async void GetPhoto()
        {
            HttpClient client = new HttpClient();
            // lấy link upload từ api, link này thay đổi và chỉ dùng 1 lần.
            var uploadUrl = await client.GetStringAsync(new Uri(ApiConfig.GetUploadImg));
             //Mở camera cho phép người dùng chụp ảnh.
            CameraCaptureUI cameraCaptureUI = new CameraCaptureUI();
            //Chờ người dùng chụp thành công (ng dùng có thể tắt camera)
            photo = await cameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                //Dừng lại trong trường hợp ko chụp ảnh
                return;
            }

            //Gửi ảnh vừa chụp lên đường link lấy phía trên với tên tham số là  "myFile" và kiểu content là image jpeg
            HttpUploadFile(uploadUrl, photo, "myFile", "image/jpeg");
        }

        public async void HttpUploadFile(string url, StorageFile photo, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                //string imageUrl = reader2.ReadToEnd();
                //Debug.WriteLine(imageUrl);
                //ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                //avatar.Text = imageUrl;
                string imageUrl = reader2.ReadToEnd();
                Debug.WriteLine(imageUrl);
                avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                this._imageUrl = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}
