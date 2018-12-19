using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APILinkedin
{
    public class Api
    {
        static Lazy<Api> LazyApi = new Lazy<Api>(() => new Api());

        public static Api Current => LazyApi.Value;

        readonly HttpClient Client;
      
        Api()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.ConnectionClose = false;
        }

        public void Init(string clientId, string clientSecret, string redirectUrl)
        {
            Const.ClientId     = clientId;
            Const.ClientSecret = clientSecret;
            Const.RedirectUri  = redirectUrl;
        }

        public string AuthUrl() => string.Format("https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={0}&redirect_uri={1}&state=DCEeFWf45A53sdfKef424scope=r_basicprofile",
                                                  Const.ClientId,
                                                  Const.RedirectUri);

        public async Task<bool> GetTokenAsync()
        {
            try
            {
                if (Const.RedirectUri.IsNullOrEmpty() && 
                    Const.ClientSecret.IsNullOrEmpty() &&
                    Const.ClientId.IsNullOrEmpty())
                {
                    throw new ArgumentNullException("Please run the Init method first!");
                }



                using (var request = new HttpRequestMessage(HttpMethod.Post, Const.tokenUrl))
                {
                    var content = new Dictionary<string, string>
                    {
                        {"grant_type"   , "authorization_code" },
                        {"code"         , Const.AuthCode},
                        {"redirect_uri" , Const.RedirectUri },
                        {"client_id"    , Const.ClientId },
                        {"client_secret", Const.ClientSecret }
                    };

                    request.Content = new FormUrlEncodedContent(content);
                    
                    using (var response = await Client.SendAsync(request).ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var tokenString = JObject.Parse(await response.Content.ReadAsStringAsync());

                            Const.AccessToken = tokenString["access_token"].ToString();
                            Const.ExpiresIn   = tokenString["expires_in"].ToString();
                            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Const.AccessToken}");

                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetPersonAsync(string args = null)
        {

            var uri = string.Empty;
            if (string.IsNullOrEmpty(args))
                uri = "https://api.linkedin.com/v1/people/~?format=json";
            else
                uri = string.Format("https://api.linkedin.com/v1/people/~:({0})?format=json", args);
            
            try
            {
                using (var response = await Client.GetAsync(uri).ConfigureAwait(false))                
                   return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
