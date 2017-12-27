using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinChatWithCosmosOnly.Ioc;

using Xamarin.Forms;
using XamarinChatWithCosmosOnly.Services;
using XamarinChatWithCosmosOnly.Cosmos;
using Microsoft.Azure.Documents.Client;
using XamarinChatWithCosmosOnly.Models;
using XamarinChatWithCosmosOnly.AppSettings;

namespace XamarinChatWithCosmosOnly
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();

            MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Loading... ",
                    HorizontalOptions  = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }
            };

            var startup = new Startup();

            startup.Init(this);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
