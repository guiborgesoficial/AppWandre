﻿using PCLExt.FileStorage;
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
        public PageCamera()
        {
            InitializeComponent();
        }
        private void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            CapturandoFotos(e);
        }
        private void CapturandoFotos(MediaCapturedEventArgs byteCamera)
        {
            if (contadorFotos < 10)
            {

                LocalRootFolder localPasta = new LocalRootFolder();
                var pastaPrincipal = localPasta.GetFolder("Carros");
                var pastaCarro = pastaPrincipal.GetFolder(stringPath);
                var pastaFotosCruas = pastaCarro.CreateFolder("fotos_cruas", CreationCollisionOption.OpenIfExists);
                var arquivo = pastaFotosCruas.CreateFile("0" + contadorFotos + ".jpeg", CreationCollisionOption.ReplaceExisting);

                File.WriteAllBytes(arquivo.Path, byteCamera.ImageData);
                contadorFotos++;
                btnCapturarFoto.Text = contadorFotos.ToString();
            }
        }
        private void CameraView_OnAvailable(object sender, bool e)
        {
                   
        }
        private void BtnCapturarFoto_Clicked(object sender, EventArgs e)
        {
            cameraView.Shutter();
        }
    }
}