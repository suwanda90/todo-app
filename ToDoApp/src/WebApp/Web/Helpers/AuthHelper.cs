using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.ViewModels;

namespace Web.Helpers
{
    public static class AuthHelper
    {
        public static HttpClient ClientBearear(IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(appSettingsViewModel.ApiUrl)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken(client, httpContextAccessor, appSettingsViewModel));

            return client;
        }

        public static string GetToken(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var tokenExpiry = string.Empty;
            string token;

            if (!CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), httpContextAccessor) &&
                !CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), httpContextAccessor))
            {
                token = GenerateToken(client, httpContextAccessor, appSettingsViewModel);
            }
            else
            {
                token = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), httpContextAccessor).ToBase64Decode();
                tokenExpiry = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), httpContextAccessor).ToBase64Decode();
            }

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenExpiry))
            {
                var dateTimeOffset = DateTime.Parse(tokenExpiry);
                if ((dateTimeOffset - DateTime.Now).TotalSeconds < 60)
                {
                    token = GenerateToken(client, httpContextAccessor, appSettingsViewModel);
                }
            }

            return token;
        }

        private static string GenerateToken(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var url = "Auth/token/" + appSettingsViewModel.ApiClientId + "/" + appSettingsViewModel.ApiClientSecret;

            var response = client.GetAsync(url).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(content);
            var token = tokens["token"] as string;
            var tokenExpiry = tokens["validTo"] as string;

            CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), token.ToBase64Encode(), 30, httpContextAccessor);
            CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), tokenExpiry.ToBase64Encode(), 30, httpContextAccessor);

            return token;
        }
    }
}

