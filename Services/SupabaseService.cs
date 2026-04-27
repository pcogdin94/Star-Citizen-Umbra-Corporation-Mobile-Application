using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views.Accessibility;
using Supabase;
using UmbraCorpApp.Models;

namespace UmbraCorpApp.Services
{
    public class SupabaseService
    {
        private readonly Client _client;

        private const string AccessTokenKey = "supabase_access_token";
        private const string RefreshTokenKey = "supabase_refresh_token";

        public SupabaseService()
        {
            var url = "https://ixlpvepxuvtfawjcdsft.supabase.co/";
            var anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Iml4bHB2ZXB4dXZ0ZmF3amNkc2Z0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzY5OTY5ODMsImV4cCI6MjA5MjU3Mjk4M30.2-FsUkbPzXqXEX7Zed1NfM_fRpIP-gd9fqlU_KqxVZ0";

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            _client = new Client(url, anonKey, options);
        }

        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();

        }

        public async Task<bool> SignUpAsync(string email, string password)
        {
            var session = await _client.Auth.SignUp(email, password);
            return session?.User != null;
        }

        public async Task<bool> TryRestoreSessionAsync()
        {
            try
            {
                string? accessToken = await SecureStorage.Default.GetAsync(AccessTokenKey);
                string? refreshToken = await SecureStorage.Default.GetAsync(RefreshTokenKey);

                if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(refreshToken))
                {
                    return false;
                }
                var session = await _client.Auth.SetSession(accessToken, refreshToken, forceAccessTokenRefresh: true);

                if (session?.User == null)
                {
                    await ClearSavedSessionAsync();
                    return false;
                }

                await SaveSessionAsync();

                return true;
            }
            catch
            {
                await ClearSavedSessionAsync();
                return false;
            }
        }

        public async Task SaveSessionAsync()
            {
              var session = _client.Auth.CurrentSession;

              if (session == null)
                    return;

              await SecureStorage.Default.SetAsync(AccessTokenKey, session.AccessToken);
              await SecureStorage.Default.SetAsync(RefreshTokenKey, session.RefreshToken);
            }

        public async Task SignOutAsync()
        {
            await _client.Auth.SignOut();
            await ClearSavedSessionAsync();
        }

        public Task ClearSavedSessionAsync()
        {
            SecureStorage.Default.Remove(AccessTokenKey);
            SecureStorage.Default.Remove(RefreshTokenKey);

            return Task.CompletedTask;
        }
        public async Task<bool> SignInAsync(string email, string password)
        {
            var session = await _client.Auth.SignIn(email, password);

            if (session?.User == null)
                return false;

            await SaveSessionAsync();

            return true;
        }

        public bool IsLoggedIn()
        {
            return _client.Auth.CurrentSession != null;
        }

        public string? GetUserEmail()
        {
            return _client.Auth.CurrentUser?.Email;
        }

        public Client GetClient()
        {
            return _client;
        }

        public string? GetUserId()
        {
            return _client.Auth.CurrentUser?.Id;
        }

        public async Task<bool> IsAdminAsync()
        {
            string? email = GetUserEmail();

            if (string.IsNullOrWhiteSpace(email))
                return false;

            var result = await _client
                .From<Profile>()
                .Where(x => x.Email == email)
                .Get();

            return result.Models.Count > 0;
        }

        public Supabase.Gotrue.Session? GetCurrentSession()
        {
            return _client.Auth.CurrentSession;
        }

        public async Task<string?> GetHomeVideoUrlAsync()
        {
            var result = await _client
                .From<SiteSetting>()
                .Where(x => x.Id == "home_video_url")
                .Single();

            return result?.Value;
        }

    }
}
