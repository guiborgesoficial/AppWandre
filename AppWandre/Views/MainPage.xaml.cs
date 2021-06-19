using AppWandre.Classes;
using AppWandre.Views;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppWandre
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<ListaCarros> ObslistaCarros = new ObservableCollection<ListaCarros>();

        public MainPage()
        {
            InitializeComponent();
            ConsultandoCarros();
        }

        private void BtnAdicionarCarro_Clicked(object sender, EventArgs e)
        {
            ObslistaCarros.Clear();
            ConsultandoCarros();
            Navigation.PushAsync(new PageDadosCarro());
        }
        public void ConsultandoCarros()
        {
            var localPasta = new LocalRootFolder();
            var pastaCarros = localPasta.CreateFolder("Carros", CreationCollisionOption.OpenIfExists);

            List<IFolder> listaPastas = new List<IFolder>();
            listaPastas.AddRange(pastaCarros.GetFolders());

            try
            {
                for (int i = 0; i < listaPastas.Count; i++)
                {
                    var pastaCarroInterna = pastaCarros.GetFolder(listaPastas[i].Name);
                    var pastaFotosCruas = pastaCarroInterna.GetFolder("fotos_cruas");
                    var imagem = pastaFotosCruas.GetFile("01.jpeg");

                    ObslistaCarros.Add(new ListaCarros() { Name = listaPastas[i].Name, Path = listaPastas[i].Path, Imagem = imagem.Path });
                }
            }
            catch(Exception erro)
            {
                if(erro.Message.Contains("not exist"))
                {

                }
                else
                {
                    DisplayAlert("Erro", "Este erro não é crítico, mas contacte o desenvolvedor" + erro, "PROSSEGUIR");
                }
            }
            

            listviewCarros.ItemsSource = ObslistaCarros;
        }
        public void DeletarItem(object sender, EventArgs e)
        {
            var itemSelect = ((MenuItem)sender);
            ListaCarros item = (ListaCarros)itemSelect.CommandParameter;
            ObslistaCarros.Remove(item);
            Directory.Delete(item.Path, true);
        }
        public async void CompartilharItem(object sender, EventArgs e)
        {
            var itemSelect = ((MenuItem)sender);
            ListaCarros item = (ListaCarros)itemSelect.CommandParameter;

            var localPasta = new LocalRootFolder();
            var pastaCarros = localPasta.GetFolder("Carros");
            var pastaCarrosCompactados = pastaCarros.CreateFolder("CarrosCompactados", CreationCollisionOption.OpenIfExists);

            if(File.Exists(pastaCarrosCompactados.Path + @"/" + item.Name + ".zip"))
            {
                try
                {
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Compartilhe no WhatsApp",
                        File = new ShareFile(pastaCarrosCompactados.Path + @"/" + item.Name + ".zip")
                    });
                }
                catch (Exception erro)
                {
                    await DisplayAlert("WhatsApp não instalado", erro.Message, "ok");
                }
            }
            else
            {
                ZipFile.CreateFromDirectory(item.Path, pastaCarrosCompactados.Path + @"/" + item.Name + ".zip");
                try
                {
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Compartilhe no WhatsApp",
                        File = new ShareFile(pastaCarrosCompactados.Path + @"/" + item.Name + ".zip")
                    });
                }
                catch (Exception erro)
                {
                    await DisplayAlert("WhatsApp não instalado", erro.Message, "ok");
                }
            }
        }
    }
}
