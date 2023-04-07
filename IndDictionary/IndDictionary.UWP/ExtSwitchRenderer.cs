using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(IndDictionary.ExtSwitch),typeof(IndDictionary.UWP.ExtSwitchRenderer))]
namespace IndDictionary.UWP
{
	internal class ExtSwitchRenderer :SwitchRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
		{
			base.OnElementChanged(e);

			if (null != Control)
			{
				if (null != Control.OnContent)
				{
					Control.OnContent = null;
					Control.OffContent = null;
				}
			}
		}
	}
}
