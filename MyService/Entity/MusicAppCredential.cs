using System;
using System.Collections.Generic;
using System.Text;

namespace MyService.Entity
{
    public class MusicAppCredential
    {
        public string token { get; set; }
        public string secretToken { get; set; }
        public long userId { get; set; }
        public long createdTimeMLS { get; set; }
        public long expiredTimeMLS { get; set; }

        public int status { get; set; }
    }
}
