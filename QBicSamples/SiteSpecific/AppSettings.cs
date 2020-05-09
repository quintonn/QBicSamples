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

        // We've put these static variables here so it is clear from the samples how these values are used and relate to different parts of the platform
        internal static string CUSTOM_CLIENT_ID = "CustomApiClient";
        internal static string CUSTOM_TOKEN_PATH = "/custom/token";

        public override void PerformAdditionalStartupConfiguration(IAppBuilder app, IUnityContainer container)
        {
            var mobileAuthOptions = new UserAuthenticationOptions()
            {
                AccessControlAllowOrigin = "*",
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                RefreshTokenExpireTimeSpan = TimeSpan.FromHours(10),
                AllowInsecureHttp = false, // or True
                TokenEndpointPath = new PathString(CUSTOM_TOKEN_PATH),
                UserContext = container.Resolve<CustomUserContext>(),
                // could also just use this...
                //UserContext = container.Resolve<UserContextBase<CustomUser>>(),
                ClientId = CUSTOM_CLIENT_ID
            };

            app.UseBasicUserTokenAuthentication(mobileAuthOptions);
        }
    }
}