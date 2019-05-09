using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HelloWorldApp.Models;
using HelloWorldApp.Views;
using Microsoft.AppCenter.Analytics;

namespace HelloWorldApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                Analytics.TrackEvent("Adding new item");
                await DataStore.AddItemAsync(newItem);
                var count = DataStore.GetItemCountAsync();
                Analytics.TrackEvent($"Item added", new System.Collections.Generic.Dictionary<string, string> {{ "New count", count.Result.ToString()}});
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Analytics.TrackEvent("Launch Items View");

                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Analytics.TrackEvent("Error loading items");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}