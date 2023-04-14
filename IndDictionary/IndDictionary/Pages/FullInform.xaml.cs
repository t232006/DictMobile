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
		IEnumerable<topic> TempTop;
		public FullInform()
		{
			InitializeComponent();
			BaseLayout.Children.Add(TransSpace,
				Constraint.Constant(0),
				Constraint.RelativeToView(WordSpace, (parent, view) =>
					{ return WordSpace.Y + WordSpace.Height + 10; }),
				Constraint.RelativeToParent((parent) =>
					{ return parent.Width; }),
				Constraint.RelativeToParent((parent) =>
					{ return parent.Height * 0.4; })
				);
			BaseLayout.Children.Add(LabelSpace,
				Constraint.Constant(0),
				Constraint.RelativeToView(TransSpace, (parent, view) =>
				{ return TransSpace.Y + TransSpace.Height + 10; }));
		}
		protected override void OnAppearing()
		{
			TempTop = App.Database.showTableTopic();
			TopicSpace.ItemsSource = TempTop.Select(p => p.Name).ToList();
			var temp = from p in TempTop
						 where p.id == (this.BindingContext as dict).Topic
						 select p.Name ;
			TopicSpace.SelectedItem = temp.ToList()[0] ;
			//Temp.Text = temp[0];
			
			base.OnAppearing();
		}
	}
}