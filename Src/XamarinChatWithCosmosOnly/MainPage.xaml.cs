using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChatWithCosmosOnly.ViewModels;

namespace XamarinChatWithCosmosOnly
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            this.BindingContext = new MainPageViewModel();

			InitializeComponent();
		}
	}
}
