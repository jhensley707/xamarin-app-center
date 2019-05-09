using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace HelloWorldApp.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;
        AppResult[] results;
        const string BrowseTitle = "Browse";
        const string AboutTitle = "About";
        const string AddTitle = "Add";
        const string CancelTitle = "Cancel";
        const string SaveTitle = "Save";
        const string AppNameLabel = "AppName 1.0";
        const string NewItemTitle = "New Item";
        const string FirstItemLabel = "First item";
        const string SecondItemLabel = "Second item";
        const string ThirdItemLabel = "Third item";
        const string FourthItemLabel = "Fourth item";
        const string ThisIsAnItemDescriptionLabel = "This is an item description.";
        const string ItemNameText = "Item name";
        const string NewItemNameText = "My new item name";
        const string NewItemDescriptionText = "My new item description.";
        const string NewItemDescriptionEditorId = "NewItemDescription";
        const string CancelItemNameText = "My canceled item name";
        const string CancelItemDescriptionText = "My canceled item description.";

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        public void VerifyItemList()
        {

            app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(BrowseTitle));

            results = app.Query(c => c.Marked(FirstItemLabel));
            Assert.IsTrue(results.Any(), $"{FirstItemLabel} label not found");
            app.Flash(c => c.Marked(FirstItemLabel));

            results = app.Query(c => c.Marked(SecondItemLabel));
            Assert.IsTrue(results.Any(), $"{SecondItemLabel} label not found");
            app.Flash(c => c.Marked(SecondItemLabel));

            results = app.Query(c => c.Marked(ThirdItemLabel));
            Assert.IsTrue(results.Any(), $"{ThirdItemLabel} label not found");
            app.Flash(c => c.Marked(ThirdItemLabel));

            //results = app.Query(c => c.Marked(FourthItemLabel));
            //Assert.IsTrue(results.Any(), $"{FourthItemLabel} label not found");
            //app.Flash(c => c.Marked(FourthItemLabel));
        }

        [Test]
        public void FirstItemDetailsAreDisplayed()
        {
            VerifyItemList();
            //app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));

            //app.WaitForElement(c => c.Marked(FirstItemLabel), timeoutMessage: $"{FirstItemLabel} label not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(FirstItemLabel));
            app.Tap(c => c.Marked(FirstItemLabel));

            app.WaitForElement(c => c.Marked(FirstItemLabel), timeoutMessage: $"{FirstItemLabel} label not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(FirstItemLabel));

            results = app.Query(c => c.Marked(ThisIsAnItemDescriptionLabel));
            Assert.IsTrue(results.Any(), $"{ThisIsAnItemDescriptionLabel} label not found");
            app.Flash(c => c.Marked(ThisIsAnItemDescriptionLabel));

            app.Back();

            app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(BrowseTitle));
        }

        [Test]
        public void NewItemAdded()
        {
            app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(BrowseTitle));

            results = app.Query(c => c.Marked(AddTitle));
            Assert.IsTrue(results.Any(), $"{AddTitle} title not found");
            //app.WaitForElement(c => c.Marked(AddTitle), timeoutMessage: $"{AddTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(AddTitle));
            app.Tap(c => c.Marked(AddTitle));

            app.WaitForElement(c => c.Text(NewItemTitle), timeoutMessage: $"{NewItemTitle} title not found");
            app.Flash(c => c.Marked(NewItemTitle));

            results = app.Query(c => c.Text(ItemNameText));
            Assert.IsTrue(results.Any(), $"{ItemNameText} text not found");
            app.Flash(c => c.Text(ItemNameText));
            app.ClearText(c => c.Text(ItemNameText));
            app.EnterText(NewItemNameText);
            app.DismissKeyboard();

            // Although there is only one ThisIsAnItemDescriptionLabel on the screen, there are
            // 7 total because it is counting the hidden ones on the browse page.
            var list = app.Query(c => c.Text(ThisIsAnItemDescriptionLabel));
            results = app.Query(c => c.Text(ThisIsAnItemDescriptionLabel));//.Index(6));
            Assert.IsTrue(results.Any(), $"{ThisIsAnItemDescriptionLabel} text not found");
            //app.WaitForElement(c => c.Marked(ThisIsAnItemDescriptionLabel), $"{ThisIsAnItemDescriptionLabel} text not found");
            app.Flash(c => c.Text(ThisIsAnItemDescriptionLabel));//.Index(6));
            app.ClearText(c => c.Marked(NewItemDescriptionEditorId));//.Index(6));
            app.EnterText(NewItemDescriptionText);
            app.DismissKeyboard();

            results = app.Query(c => c.Marked(SaveTitle));
            Assert.IsTrue(results.Any(), $"{SaveTitle} title not found");
            app.Flash(c => c.Marked(SaveTitle));
            app.Tap(c => c.Marked(SaveTitle));

            //app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            //app.Flash(c => c.Marked(BrowseTitle));

            VerifyItemList();

            results = app.Query(c => c.Marked(NewItemNameText));
            Assert.IsTrue(results.Any(), $"{NewItemNameText} text not found");
            app.Flash(c => c.Marked(NewItemNameText));

            results = app.Query(c => c.Marked(NewItemDescriptionText));
            Assert.IsTrue(results.Any(), $"{NewItemDescriptionText} text not found");
            app.Flash(c => c.Marked(NewItemDescriptionText));
        }

        [Test]
        public void CancelItemNotAdded()
        {
            app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(BrowseTitle));

            results = app.Query(c => c.Marked(AddTitle));
            Assert.IsTrue(results.Any(), $"{AddTitle} title not found");
            //app.WaitForElement(c => c.Marked(AddTitle), timeoutMessage: $"{AddTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(AddTitle));
            app.Tap(c => c.Marked(AddTitle));

            //results = app.Query(c => c.Marked(NewItemTitle));
            //Assert.IsTrue(results.Any(), $"{NewItemTitle} title not found");
            app.WaitForElement(c => c.Marked(NewItemTitle), timeoutMessage: $"{NewItemTitle} title not found");
            app.Flash(c => c.Marked(NewItemTitle));

            results = app.Query(c => c.Text(ItemNameText));
            Assert.IsTrue(results.Any(), $"{ItemNameText} text not found");
            app.Flash(c => c.Text(ItemNameText));
            app.ClearText(c => c.Text(ItemNameText));
            app.EnterText(CancelItemNameText);
            app.DismissKeyboard();

            // Although there is only one ThisIsAnItemDescriptionLabel on the screen, there are
            // 7 total because it is counting the hidden ones on the browse page.
            var list = app.Query(c => c.Text(ThisIsAnItemDescriptionLabel));
            //results = app.Query(c => c.Text(ThisIsAnItemDescriptionLabel).Index(6));
            results = app.Query(c => c.Text(ThisIsAnItemDescriptionLabel));
            Assert.IsTrue(results.Any(), $"{ThisIsAnItemDescriptionLabel} text not found");
            //app.WaitForElement(c => c.Marked(ThisIsAnItemDescriptionLabel), $"{ThisIsAnItemDescriptionLabel} text not found");
            app.Flash(c => c.Text(ThisIsAnItemDescriptionLabel)); //Index(6));
            app.ClearText(c => c.Text(ThisIsAnItemDescriptionLabel));//.Index(6));
            app.EnterText(CancelItemDescriptionText);
            app.DismissKeyboard();

            results = app.Query(c => c.Marked(CancelTitle));
            Assert.IsTrue(results.Any(), $"{CancelTitle} title not found");
            app.Flash(c => c.Marked(CancelTitle));
            app.Tap(c => c.Marked(CancelTitle));

            VerifyItemList();

            results = app.Query(c => c.Marked(CancelItemNameText));
            Assert.IsFalse(results.Any(), $"{CancelItemNameText} text found");

            results = app.Query(c => c.Marked(CancelItemDescriptionText));
            Assert.IsFalse(results.Any(), $"{CancelItemDescriptionText} text found");
        }

        [Test]
        public void AboutViewDisplayed()
        {
            app.WaitForElement(c => c.Marked(BrowsePageTitle), timeoutMessage: $"{BrowsePageTitle} title not found", timeout: new TimeSpan(0, 0, 5));

            if (this.platform == Platform.Android)
            {
                results = app.Query(c => c.Class("android.widget.ImageButton"));
                Assert.IsTrue(results.Any(), "Menu ImageButton not found");
                app.Flash(c => c.Class("android.widget.ImageButton"));
                app.Tap(c => c.Class("android.widget.ImageButton"));
            }
            else if (this.platform == Platform.iOS)
            {
                results = app.Query(c => c.Button("Menu"));
                Assert.IsTrue(results.Any(), "Menu ImageButton not found");
                app.Flash(c => c.Button("Menu"));
                app.Tap(c => c.Button("Menu"));
            }

            // Since no resource id is associated with the About menu item, it becomes random
            // trial and error to find the correct container with the correct labels.
            // This becomes highly inconvenient when the layout changes.
            results = app.Query(c => c.Marked("About"));
            Assert.IsTrue(results.Any(), "About menu item not found");
            app.Flash(c => c.Marked("About"));
            app.Tap(c => c.Marked("About"));

            app.WaitForElement(c => c.Marked(AppNameLabel), timeoutMessage: $"{AppNameLabel} label not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(AppNameLabel));

            if (this.platform == Platform.Android)
            {
                results = app.Query(c => c.Class("android.widget.ImageButton"));
                Assert.IsTrue(results.Any(), "Menu ImageButton not found");
                app.Flash(c => c.Class("android.widget.ImageButton"));
                app.Tap(c => c.Class("android.widget.ImageButton"));
            }
            else if (this.platform == Platform.iOS)
            {
                results = app.Query(c => c.Button("Menu"));
                Assert.IsTrue(results.Any(), "Menu ImageButton not found");
                app.Flash(c => c.Button("Menu"));
                app.Tap(c => c.Button("Menu"));
            }

            results = app.Query(c => c.Marked("Browse"));
            Assert.IsTrue(results.Any(), "Browse menu item not found");
            app.Flash(c => c.Marked("Browse"));
            app.Tap(c => c.Marked("Browse"));

            app.WaitForElement(c => c.Marked(BrowseTitle), timeoutMessage: $"{BrowseTitle} title not found", timeout: new TimeSpan(0, 0, 5));
            app.Flash(c => c.Marked(BrowseTitle));
        }
    }
}
