using Android.Graphics;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using Plugin.SimpleAudioPlayer;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
        private readonly ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        public PageCamera()
        {
            InitializeComponent();
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("AppWandre." + "cameraSound.mp3");
            player.Load(audioStream);
        }
        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Atenção", "Não é possível voltar para as páginas anteriores. Complete a rotina para prosseguir.", "OK");
            return true;
        }
        private void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            player.Play();
            activIndicator.IsRunning = true;
            activIndicator.IsVisible = true;
            btnCapturarFoto.IsEnabled = false;
            btnCapturarFoto.IsVisible = false;
            universalByteArrayFoto = null;
            RetornandoFoto(e);
        }
        private void RetornandoFoto(MediaCapturedEventArgs fotoRetorno)
        {
            imgRetornoCaptura.Source = fotoRetorno.Image;
            imgRetornoCaptura.Rotation = fotoRetorno.Rotation;
            imgRetornoCaptura.IsVisible = true;
            btnCancelado.IsVisible = true;
            btnCancelado.IsEnabled = true;
            btnVerificado.IsVisible = true;
            btnVerificado.IsEnabled = true;
            btnCapturarFoto.IsVisible = false;
            btnCapturarFoto.IsEnabled = false;
            universalByteArrayFoto = fotoRetorno.ImageData;
        }
        private async Task CapturandoFotos()
        {
            if (contadorFotos < 10)
            {
                LocalRootFolder localPasta = new LocalRootFolder();
                var pastaPrincipal = localPasta.GetFolder("Carros");
                var pastaCarro = pastaPrincipal.GetFolder(stringPath);
                var pastaFotosCruas = pastaCarro.CreateFolder("fotos", CreationCollisionOption.OpenIfExists);
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
                btnCapturarFoto.IsEnabled = true;
                btnCapturarFoto.IsVisible = true;
                if (contadorFotos == 10)
                {
                    await DisplayActionSheet("Sucesso", "OK", "", "Fotos amarzenadas com sucesso. Voltando para a página inicial...");
                    await Navigation.PopToRootAsync();
                }
            }
        }
        public Android.Graphics.Bitmap RotacionarBitmap(int angulo, Android.Graphics.Bitmap bitmap)
        {
            double x;
            if (bitmap.Height != bitmap.Width)
            {
                x = (bitmap.Width / 8) + 5;
            }
            else
            {
                x = 0;
            }

            Android.Graphics.Matrix matrix = new Android.Graphics.Matrix();
            matrix.PostRotate(angulo);
            return Android.Graphics.Bitmap.CreateBitmap(bitmap, Convert.ToInt32(x), 0,
                bitmap.Height, bitmap.Height, matrix, true);
        }

        private void ImageButtonCancelado_Clicked(object sender, EventArgs e)
        {
            activIndicator.IsVisible = false;
            activIndicator.IsRunning = false;
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsVisible = false;
            btnCapturarFoto.IsEnabled = true;
            btnCapturarFoto.IsVisible = true;
            boolImagemAprovada = false;
        }

        private async void ImageButtonVerificado_Clicked(object sender, EventArgs e)
        {
            imgRetornoCaptura.IsVisible = false;
            btnCancelado.IsVisible = false;
            btnVerificado.IsEnabled = false;
            btnVerificado.IsVisible = false;
            boolImagemAprovada = true;
            if (boolImagemAprovada)
            {
                await CapturandoFotos();
            }
        }
    }
}