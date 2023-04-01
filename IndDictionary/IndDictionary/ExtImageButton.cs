using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace IndDictionary
{
    class ExtImageButton : ImageButton
    {
		public static readonly BindableProperty CheckedProperty = 
			BindableProperty.Create("Checked",
				typeof(bool), 
				typeof(ExtImageButton),
				false,
				BindingMode.TwoWay);
		public bool Checked
		{
			set {SetValue(CheckedProperty, value);}
			get => (bool)GetValue(CheckedProperty);
		}
		
		/*public ExtImageButton()
		{
			Clicked += (sender, e) =>
			{
				Checked = !Checked;
				if (Checked)
					Source = ImageSource.FromResource("IndDictionary.Resources.check-mark.bmp");
				else
					Source = null;
			};	
		}*/
		
    }
}
