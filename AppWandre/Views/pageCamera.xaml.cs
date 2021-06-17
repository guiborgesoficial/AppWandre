using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Drawing;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageCamera : ContentPage
    {
        public int contadorFotos = 1;
        public pageCamera()
        {
            InitializeComponent();
        }
        private void cameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            CapturandoFotos(e);
        }
        private void CapturandoFotos(MediaCapturedEventArgs byteCamera)
        {
            if (contadorFotos < 10)
            {
                var localPasta = new LocalRootFolder();
                var pasta = localPasta.CreateFolder("Carros", CreationCollisionOption.OpenIfExists);
                var arquivo = pasta.CreateFile("0" + contadorFotos + ".png", CreationCollisionOption.ReplaceExisting);

                byte[] imgByte = byteCamera.ImageData;
                File.WriteAllBytes(arquivo.Path, imgByte);
                contadorFotos++;
                btnCapturarFoto.Text = contadorFotos.ToString();
            }
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