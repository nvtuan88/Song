using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyService.Entity;
using MyService.Service;
using T1904EHelloUWP.Entity;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var memberService = new MemberService();
            MusicAppCredential musicAppCredential = memberService.Login("helloworld00002@gmail.com", "123456").GetAwaiter().GetResult();
            Console.WriteLine($"Done {musicAppCredential.token}");
            Console.ReadLine();
        }
    }
}
