using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HelloWorldApp.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HelloWorldApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // This should come before AppCenter.Start() is called
            // Avoid duplicate event registration:
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    var title = e.Title;
                    var message = e.Message;
                    string summary = null;
                    /*
                     * Message and title cannot be read from a background notification object.
                     * Message being a mandatory field, you can use that to check foreground vs background.
                     */
                    if (message != null)
                    {
                        // Add the notification message and title to the message
                        summary = $"Push notification received:" +
                                            $"\n\tNotification title: {title}" +
                                            $"\n\tMessage: {message}";

                        // If there is custom data associated with the notification,
                        // print the entries
                        if (e.CustomData != null)
                        {
                            summary += "\n\tCustom data:\n";
                            foreach (var key in e.CustomData.Keys)
                            {
                                summary += $"\t\t{key} : {e.CustomData[key]}\n";
                            }
                        }
                    }
                    else
                    {
                        title = "Background Notification";
                        message = "Custom data would be handled here";
                        summary = $"Push notification received:" +
                                            $"\n\tNotification title: {title}" +
                                            $"\n\tMessage: {message}";
                        // If there is custom data associated with the notification,
                        // print the entries
                        if (e.CustomData != null)
                        {
                            summary += "\n\tCustom data:\n";
                            foreach (var key in e.CustomData.Keys)
                            {
                                summary += $"\t\t{key} : {e.CustomData[key]}\n";
                            }
                        }
                    }

                    // Send the notification summary to debug output
                    System.Diagnostics.Debug.WriteLine(summary);
                    Current.MainPage.DisplayAlert(title, message, "OK");
                };
            }

            // AppCenter.start after
            // Handle when your app starts
            AppCenter.Start("ios=8f63f315-da02-4f9c-bc3f-b51909206526;" +
                "uwp=f8997871-78d0-45cc-82cf-80fabf949249;" +
                "android=271ff12b-1c2f-4888-8280-421e9b12e682",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Push));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
