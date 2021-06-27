using AppWandre.Classes;
using AppWandre.Views;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppWandre
{
    public partial class MainPage : ContentPage
    {
        private readonly ObservableCollection<ListaCarros> ObslistaCarros = new ObservableCollection<ListaCarros>();

        public MainPage()
        {
            InitializeComponent();
            listviewCarros.BeginRefresh();
            listviewCarros.EndRefresh();
        }

        private void BtnAdicionarCarro_Clicked(object sender, EventArgs e)
        {
            ObslistaCarros.Clear();
            Navigation.PushAsync(new PageDadosCarro());
        }
        public async Task ConsultandoCarros()
        {
            ObslistaCarros.Clear();
            var localPasta = new LocalRootFolder();
            var pastaCarros = await localPasta.CreateFolderAsync("Carros", CreationCollisionOption.OpenIfExists);

            List<IFolder> listaPastas = new List<IFolder>();
            listaPastas.AddRange(await pastaCarros.GetFoldersAsync());

            try
            {
                for (int i = 0; i < listaPastas.Count; i++)
                {
                    var pastaCarroInterna = await pastaCarros.GetFolderAsync(listaPastas[i].Name);
                    var pastaFotosCruas = await pastaCarroInterna.GetFolderAsync("fotos_cruas");
                    var imagem = await pastaFotosCruas.GetFileAsync("01.jpeg");

                    ObslistaCarros.Add(new ListaCarros() { Name = listaPastas[i].Name, Path = listaPastas[i].Path, Imagem = imagem.Path });
                }
            }
            catch (Exception erro)
            {
                if (erro.Message.Contains("not exist"))
                {

                }
                else
                {
                    await DisplayAlert("Erro", "Este erro não é crítico, mas contacte o desenvolvedor" + erro, "PROSSEGUIR");
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

            if (File.Exists(pastaCarrosCompactados.Path + @"/" + item.Name + ".zip"))
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
        public async void ListviewCarros_Refreshing(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBarCarros.Text))
            {
                await ConsultandoCarros();
                listviewCarros.EndRefresh();
            }
            else
            {
                listviewCarros.EndRefresh();
            }
        }
        private void SearchBarCarros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue.ToLower()))
            {
                listviewCarros.ItemsSource = ObslistaCarros;
            }
            else
            {
                listviewCarros.ItemsSource = ObslistaCarros.Where(x => x.Name.StartsWith(e.NewTextValue.ToLower()));
            }
        }
    }
}
