using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace HelloWorldApp.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .ApkFile(@"..\..\..\HelloWorldApp\HelloWorldApp.Android\bin\debug\com.companyname.HelloWorldApp.apk")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}