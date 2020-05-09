using BasicAuthentication.Authentication;
using BasicAuthentication.Security;
using Microsoft.Owin;
using Owin;
using System;
using Unity;
using WebsiteTemplate.Utilities;

namespace QBicSamples.SiteSpecific
{
    public class AppSettings : ApplicationSettingsCore
    {
        public override bool EnableAuditing => false; // Will audit everything

        public override bool DebugStartup => false; //  will create a window to allow debugging when starting if true

        public override bool UpdateDatabase => true; // Set to true first time to create tables. Also set to true after making changes

        public override string ApplicationPassPhrase => "RL&xFuVM&k]u[Zx/:*RFWBS5c};{Jp%~"; // This is used for encrypting/decrypting password inputs. Should be different for each project

        public override Type GetApplicationStartupType => typeof(Startup);

        public override string SystemEmailAddress => "system@example.com";

        public override string GetApplicationName()
        {
            return "QBic Samples";
        }

        // The following code registers a custom Identity or Authentication & Authorization service that will allow a different kind of user
        // to login and get an access token and refresh token, as well as to use the refresh token to obtain a new access token

        // We've put these static variables here so it is clear from the samples how these values are used and relate to different parts of the platform
        internal static string CUSTOM_CLIENT_ID = "CustomApiClient";
        internal static string CUSTOM_TOKEN_PATH = "/custom/token";

        public override void PerformAdditionalStartupConfiguration(IAppBuilder app, IUnityContainer container)
        {
            var mobileAuthOptions = new UserAuthenticationOptions()
            {
                AccessControlAllowOrigin = "*",
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1), // how long the access token should be valid for, usually a small amount, since it cannot be revoked.
                RefreshTokenExpireTimeSpan = TimeSpan.FromHours(10),  // how long the refresh token should be valid for, can be much longer, since we can revoke this.
                AllowInsecureHttp = false, // or True
                TokenEndpointPath = new PathString(CUSTOM_TOKEN_PATH),  // The url at which users can login and refresh tokens.
                UserContext = container.Resolve<CustomUserContext>(), // the user context to use for retrieving users and checking their passwords
                // could also just use this...
                //UserContext = container.Resolve<UserContextBase<CustomUser>>(),
                ClientId = CUSTOM_CLIENT_ID
            };

            app.UseBasicUserTokenAuthentication(mobileAuthOptions);
        }
    }
}