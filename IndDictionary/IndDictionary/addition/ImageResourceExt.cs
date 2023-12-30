using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndDictionary.addition
{
	[ContentProperty("Source")]
	public class ImageResourceExt : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Source == null)
			{
				return null;
			}
			var imageSource = ImageSource.FromResource(Source);

			return imageSource;
		}
	}
}
