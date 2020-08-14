using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1904EHelloUWP.Entity
{
    public class ApiConfig
    {
        public const string ApiDomain = "https://2-dot-backup-server-002.appspot.com"; // domain chính.
        public const string LoginApi = ApiDomain + "/_api/v2/members/authentication"; // login hệ thống.
        public const string RegisterApi = ApiDomain + "/_api/v2/members"; // đăng ký tài khoản
        public const string SongDetailApi = ApiDomain + "/_api/v2/songs/detail"; // xem bài hát chi tiết. 
        public const string MySongApi = ApiDomain + "/_api/v2/songs/get-mine"; // lấy danh sách bài hát của tôi.
        public const string ListSongApi = ApiDomain + "/_api/v2/songs/get-free-songs"; // lấy danh sách bài hát mới.
        public const string MemberInformationApi = ApiDomain + "/_api/v2/members/information"; // lấy thông tin người dùng.
        public const string CreateSongApi = ApiDomain + "/_api/v2/songs"; // tạo mới bài hát mới.
        public const string GetUploadImageUrl = ApiDomain + "/get-upload-token"; // lấy link gửi ảnh từ google storage.
        public const string MediaType = "application/json";
        public const string UploadImageName = "myFile";
        public const string UploadContentType = "image/jpeg";
    }
}
