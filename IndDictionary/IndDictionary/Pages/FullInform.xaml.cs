using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FullInform : ContentPage
	{
		dict TempDict;
		bool blank;
		IEnumerable<topic> TempTop;
		public FullInform(bool _blank)
		{
			InitializeComponent();
			blank = _blank;
			BaseLayout.Children.Add(TransSpace,
				Constraint.Constant(0),
				Constraint.RelativeToView(WordSpace, (parent, view) =>
					{ return WordSpace.Y + WordSpace.Height + 10; }),
				Constraint.RelativeToParent((parent) =>
					{ return parent.Width; }),
				Constraint.RelativeToParent((parent) =>
					{ return parent.Height * 0.35; })
				);
			BaseLayout.Children.Add(LabelSpace,
				Constraint.Constant(0),
				Constraint.RelativeToView(TransSpace, (parent, view) =>
				{ return TransSpace.Y + TransSpace.Height + 10; }));
		}

		protected void onRecordChanged(object Sender, EventArgs e)
		{
			if (blank) ConfirmB.IsEnabled = true;
			else ConfirmB.IsVisible = true;
		}

		protected void onConfPress(object Sender, EventArgs e)
		{
			App.Database.saveRecD(TempDict);
			Navigation.PopAsync();
		}

		protected void onDeclPress(object Sender, EventArgs e)
		{
			if (blank) Navigation.PopAsync();
			else ConfirmB.IsVisible = false;
		}

		protected void onDelPress(object Sender, EventArgs e)
		{
			App.Database.deleteRecD((this.BindingContext as dict).Number);
		}

		protected override void OnAppearing()
		{
			TempTop = App.Database.showTableTopic();
			TopicSpace.ItemsSource = TempTop.Select(p => p.Name).ToList();
			
			if (!blank)
			{
				TempDict = this.BindingContext as dict;
				var temp = from p in TempTop
						   where p.id == TempDict.Topic
						   select p.Name;
				TopicSpace.SelectedItem = temp.ToList()[0];
				TempDict.Relevation++;
				App.Database.saveRecD(TempDict);	
			}
			else
			{
				TopicSpace.SelectedItem = TempTop.ToList()[0].ToString();
				EditBox.IsToggled = true;
				ConfirmB.IsEnabled = false;
				DeleteB.IsVisible = false;
			}
			ConfirmB.IsVisible = blank;			
			base.OnAppearing();
		}
	}
}