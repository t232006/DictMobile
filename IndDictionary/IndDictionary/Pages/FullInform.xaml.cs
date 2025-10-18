using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace IndDictionary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FullInform : ContentPage
	{
		dict TempDict;
		bool blank;
		//bool editable = false;
		IEnumerable<topic> TempTop;
		ToolbarItem ConfirmItem;
		ToolbarItem CancelItem;
		ToolbarItem DeleteItem;
		
		public FullInform(bool _blank)
		{
			InitializeComponent();
			blank = _blank;

			ConfirmItem = new ToolbarItem()
			{
				Text = "Confirm",
				Order = ToolbarItemOrder.Primary,
				Priority = 0,
			};
			CancelItem = new ToolbarItem()
			{
				Text = "Cancel",
				Order = ToolbarItemOrder.Primary,
				Priority = 1
			};
			DeleteItem = new ToolbarItem()
			{
				Text = "Delete",
				Order = ToolbarItemOrder.Secondary,
				Priority = 2
			};
			ConfirmItem.Clicked += onConfPress;
			CancelItem.Clicked += onDeclPress;
			DeleteItem.Clicked += onDelPress;
			//EditBut.Clicked += onEditBut;
			if (_blank)	ToolbarItems.Add(ConfirmItem);
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
		protected void onEditBut(object sender, EventArgs e)
		{
			EditBox.IsToggled = !EditBox.IsToggled;
			if (EditBox.IsToggled)
			{
				//(sender as ToolbarItem).;
				ToolbarItems.Add(DeleteItem); ToolbarItems.Add(ConfirmItem); ToolbarItems.Add(CancelItem);
			} else
			{
				//(sender as Button).BackgroundColor = Color.Gainsboro;
				ToolbarItems.RemoveAt(0); ToolbarItems.RemoveAt(0); ToolbarItems.RemoveAt(0);
			}
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			bool HorizontalOr()
			{
				return (width > height);
			}
			base.OnSizeAllocated(width, height);

			if (HorizontalOr()) LabelSpace.Orientation = StackOrientation.Horizontal; else
								LabelSpace.Orientation = StackOrientation.Vertical;
		}

		protected void onRecordChanged(object Sender, EventArgs e)
		{
			if (blank)
			{
				ToolbarItems.Add(CancelItem);
				ToolbarItems.Add(ConfirmItem);
				ToolbarItems.Add(DeleteItem);
			}	
		}

		protected void onConfPress(object Sender, EventArgs e)
		{
			App.Database.saveRecD(TempDict);
			Navigation.PopAsync();
		}

		protected void onDeclPress(object Sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		protected void onDelPress(object Sender, EventArgs e)
		{
			App.Database.deleteRecD((this.BindingContext as dict).Number);
			Navigation.PopAsync();
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
				//EditBox.IsToggled = true;
				//EditBut.Active = false;
				
			}
			//ConfirmB.IsVisible = blank;			
			base.OnAppearing();
		}
		protected override void OnDisappearing()
		{
			
			base.OnDisappearing();
		}
	}
}