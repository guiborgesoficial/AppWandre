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
    public partial class pageCamera : ContentPage
    {
        public pageCamera()
        {
            InitializeComponent();
        }

        private void cameraView_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {

        }

        private void cameraView_OnAvailable(object sender, bool e)
        {

        }
    }
}