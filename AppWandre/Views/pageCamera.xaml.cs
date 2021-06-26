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

                Android.Graphics.Bitmap bitmapCamera = BitmapFactory.DecodeByteArray(universalByteArrayFoto, 0, universalByteArrayFoto.Length);
                var bitmapRotacionado = RotacionarBitmap(90, bitmapCamera);

                MemoryStream stream = new MemoryStream();
                bitmapRotacionado.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);
                byte[] byteArrayRotacionado = stream.ToArray();
                File.WriteAllBytes(arquivo.Path, byteArrayRotacionado);

                contadorFotos++;
                btnCapturarFoto.Text = contadorFotos.ToString();
            }
        }
        public Android.Graphics.Bitmap RotacionarBitmap(int angulo, Android.Graphics.Bitmap bitmap)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(angulo);
            return Android.Graphics.Bitmap.CreateBitmap(bitmap, 0, 0,
                bitmap.Width, bitmap.Height, matrix, true);
        }

        private void BtnCapturarFoto_Clicked(object sender, EventArgs e)
        {
            cameraView.Shutter();
        }

        private void ImageButtonCancelado_Clicked(object sender, EventArgs e)
        {
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsVisible = false;
            btnCapturarFoto.IsVisible = true;
            boolImagemAprovada = false;
        }

        private void ImageButtonVerificado_Clicked(object sender, EventArgs e)
        {
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsVisible = false;
            btnCapturarFoto.IsVisible = true;
            boolImagemAprovada = true;
            if (boolImagemAprovada)
            {
                CapturandoFotos();
            }
        }
    }
}