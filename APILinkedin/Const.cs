using System;

namespace APILinkedin
{
    public class Const
    {
        internal const string BaseAuth     = "https://www.linkedin.com/";
        internal const string Auth         = BaseAuth + "oauth/v2/authorization";
        internal const string tokenUrl     = BaseAuth + "oauth/v2/accessToken";
        internal const string BaseUrl      = "https://api.linkedin.com/";
        //internal const string UserUrl      = BaseUrl + "v1/people/~:({0})?format=json";

        public static string ClientId      = string.Empty; 
        public static string ClientSecret  = string.Empty; 
        public static string RedirectUri   = string.Empty; 
        public static string AuthCode      = string.Empty;
        public static string AccessToken   = string.Empty;
        public static string ExpiresIn     = string.Empty;
    }  
}