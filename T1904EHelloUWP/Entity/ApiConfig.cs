using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1904EHelloUWP.Entity
{
    class ApiConfig
    {
        public const string DomainApi = "https://2-dot-backup-server-002.appspot.com";
        public const string LoginApi = DomainApi + "/_api/v2/members/authentication";
        public const string RegisterApi = DomainApi + "/_api/v2/members";
        public const string CreatSongApi = DomainApi + "/_api/v2/songs";
        public const string GetUploadImg = DomainApi + "/get-upload-token";
        public const string MemberInfor = DomainApi + "/get-upload-token";
    }
}
