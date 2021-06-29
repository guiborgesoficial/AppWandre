using Android.Graphics;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCamera : ContentPage
    {
        public int contadorFotos = 1;
        public string stringPath;
        private bool boolImagemAprovada;
        private byte[] universalByteArrayFoto;
        public PageCamera()
        {
            InitializeComponent();
        }
        private void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            btnCapturarFoto.IsEnabled = false;
            universalByteArrayFoto = null;
            RetornandoFoto(e);
        }
        private void RetornandoFoto(MediaCapturedEventArgs fotoRetorno)
        {
            imgRetornoCaptura.Source = fotoRetorno.Image;
            imgRetornoCaptura.Rotation = fotoRetorno.Rotation;
            imgRetornoCaptura.IsVisible = true;
            btnCancelado.IsVisible = true;
            btnVerificado.IsVisible = true;
            btnCapturarFoto.IsVisible = false;
            universalByteArrayFoto = fotoRetorno.ImageData;
        }
        private void CapturandoFotos()
        {
            if (contadorFotos < 10)
            {
                LocalRootFolder localPasta = new LocalRootFolder();
                var pastaPrincipal = localPasta.GetFolder("Carros");
                var pastaCarro = pastaPrincipal.GetFolder(stringPath);
                var pastaFotosCruas = pastaCarro.CreateFolder("fotos_cruas", CreationCollisionOption.OpenIfExists);
                var arquivo = pastaFotosCruas.CreateFile("0" + contadorFotos + ".jpeg", CreationCollisionOption.ReplaceExisting);

                Bitmap bitmapCamera = BitmapFactory.DecodeByteArray(universalByteArrayFoto, 0, universalByteArrayFoto.Length);
                var bitmapRotacionado = RotacionarBitmap(90, bitmapCamera);
                var bitmap1080 = Android.Graphics.Bitmap.CreateScaledBitmap(bitmapRotacionado, 1080, 1080, true);
                
                MemoryStream stream = new MemoryStream();
                bitmap1080.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);
                
                byte[] byteArrayRotacionado = stream.ToArray();
                File.WriteAllBytes(arquivo.Path, byteArrayRotacionado);

                contadorFotos++;
                btnCapturarFoto.Text = contadorFotos.ToString();

                activIndicator.IsRunning = false;
                activIndicator.IsVisible = false;

                if (contadorFotos == 10)
                {
                    DisplayActionSheet("Sucesso", "OK", "", "Fotos amarzenadas com sucesso. Voltando para a página inicial...");
                    Navigation.PopToRootAsync();
                }
            }
        }
        public Android.Graphics.Bitmap RotacionarBitmap(int angulo, Android.Graphics.Bitmap bitmap)
        {
            Android.Graphics.Matrix matrix = new Android.Graphics.Matrix();
            matrix.PostRotate(angulo);
            return Android.Graphics.Bitmap.CreateBitmap(bitmap, 165, 0,
                bitmap.Height, bitmap.Height, matrix, true);
        }

        private void ImageButtonCancelado_Clicked(object sender, EventArgs e)
        {
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsVisible = false;
            btnCapturarFoto.IsEnabled = true;
            btnCapturarFoto.IsVisible = true;
            boolImagemAprovada = false;
        }

        private void ImageButtonVerificado_Clicked(object sender, EventArgs e)
        {
            activIndicator.IsVisible = true;
            activIndicator.IsRunning = true;
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsEnabled = false;
            btnVerificado.IsVisible = false;
            btnCapturarFoto.IsEnabled = true;
            btnCapturarFoto.IsVisible = true;
            boolImagemAprovada = true;
            if (boolImagemAprovada)
            {
                CapturandoFotos();
                btnVerificado.IsEnabled = true;
            }
        }
    }
}