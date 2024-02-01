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
    public partial class Flyout_Page : FlyoutPage
    {
        bool transl=false;
        public bool Transl { set => transl = value; }
        public Flyout_Page()
        {
            InitializeComponent();
            Detail = new NavigationPage(new WordPage(transl));
        }
        protected void onSelected(object sender, ItemTappedEventArgs e)
        {
            transl = (e.ItemIndex == 0) ? false : true;
        }

    }
}