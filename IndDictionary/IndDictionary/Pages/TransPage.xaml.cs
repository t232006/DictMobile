using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransPage : FlyoutPage
	{
		public TransPage (bool slog)
		{
			InitializeComponent ();
			if (slog) Slog.Text = "idi na hyi";
		}
	}
}