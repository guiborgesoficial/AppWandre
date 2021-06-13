using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageOpcionais : ContentPage
    {
        public pageOpcionais()
        {
            InitializeComponent();
        }
        private void btnSalvarOpcionais_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pageCamera());
        }
    }
}