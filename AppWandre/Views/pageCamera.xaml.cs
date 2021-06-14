using Android.Graphics;
using System;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageCamera : ContentPage
    {
        public pageCamera()
        {
            InitializeComponent();
        }
        private async void cameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            Image foto = new Image();
            foto.Source = e.Image;

            var localPasta = new LocalRootFolder();
            var pasta = localPasta.CreateFolder("Carros", CreationCollisionOption.OpenIfExists);
            var arquivo = pasta.CreateFile("teste" + ".png", CreationCollisionOption.ReplaceExisting);

            foto.Source = ImageSource.FromFile(arquivo.Path);
        }
        private void cameraView_OnAvailable(object sender, bool e)
        {
                   
        }
        private void btnCapturarFoto_Clicked(object sender, EventArgs e)
        {
            cameraView.Shutter();
        }
    }
}