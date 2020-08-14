using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyService.Entity;
using Newtonsoft.Json;
using T1904EHelloUWP.Entity;

namespace MyService.Service
{
    public class MemberService
    {
        public async Task<Member> Register(Member member)
        {
            var memberJson = JsonConvert.SerializeObject(member);
            var requestData = new StringContent(memberJson, Encoding.UTF8, ApiConfig.MediaType);
            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(new Uri(ApiConfig.RegisterApi), requestData);
            if (httpResponseMessage.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Member>(responseContent);
        }

        public async Task<MusicAppCredential> Login(string email, string password)
        {
            var loginInformation = new
            {
                email, password
            };
            var loginInformationJson = JsonConvert.SerializeObject(loginInformation);
            var requestData = new StringContent(loginInformationJson, Encoding.UTF8, ApiConfig.MediaType);
            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(new Uri(ApiConfig.LoginApi), requestData);
            if (httpResponseMessage.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MusicAppCredential>(responseContent);
        }   
    }
}
