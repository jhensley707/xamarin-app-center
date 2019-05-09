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
                // Cross-platform path builder
                // This path to the apk file is unnecessary if the Android project is referenced by UITest project
                // @"..\..\..\HelloWorldApp\HelloWorldApp.Android\bin\debug\com.companyname.HelloWorldApp.apk"
                var apkPath = new string[] { "..", "..", "..", "HelloWorldApp", "HelloWorldApp.Android", "bin", "Debug", "com.companyname.HelloWorldApp.apk" };
                return ConfigureApp.Android
                //.ApkFile(System.IO.Path.Combine(apkPath))
                .StartApp();
            }

            // This simulator device id and path to the iOS app file is unnecessary if the iOS project is referenced by UITest project
            //const string simId = "B6FCC117-58C5-4E98-AB6C-6AF581FEE300";// iPhone 6s 12.2 Simulator
            //const string iPhoneSimulatorAppPath = @"../../../HelloWorldApp/HelloWorldApp.iOS/bin/iPhoneSimulator/debug/device-builds/iphone8.1-12.2/HelloWorldApp.iOS.app";
            return ConfigureApp.iOS
                //.AppBundle(iPhoneSimulatorAppPath)
                //.DeviceIdentifier(simId)
                .StartApp();
        }
    }
}