using Microsoft.AppCenter.Analytics;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace HelloWorldApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            Analytics.TrackEvent("Launch About View");

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}