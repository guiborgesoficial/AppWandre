using Android;
using AppWandre.Classes;
using AppWandre.Views;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppWandre
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly ObservableCollection<ListaCarros> ObslistaCarros = new ObservableCollection<ListaCarros>();
        private readonly ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        public MainPage()
        {
            InitializeComponent();
            listviewCarros.BeginRefresh();
            listviewCarros.EndRefresh();

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("AppWandre." + "pop.mp3");

            player.Load(audioStream);
        }

       

        private void BtnAdicionarCarro_Clicked(object sender, EventArgs e)
        {
            player.Play();

            btnAdicionarCarro.IsEnabled = false;    
            Navigation.PushAsync(new PageDadosCarro());
            btnAdicionarCarro.IsEnabled = true;
        }
        public async Task ConsultandoCarros()
        {
            ObslistaCarros.Clear();
            
            try
            {
                var localPasta = new LocalRootFolder();
                var pastaCarros = await localPasta.GetFolderAsync("Carros");

                List<IFolder> listaPastas = new List<IFolder>();
                listaPastas.AddRange(await pastaCarros.GetFoldersAsync());

                for (int i = 0; i < listaPastas.Count; i++)
                {
                    var pastaCarroInterna = await pastaCarros.GetFolderAsync(listaPastas[i].Name);
                    var pastaFotosCruas = await pastaCarroInterna.GetFolderAsync("fotos");
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
            listviewCarros.EndRefresh();
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
            player.Play();
            var itemSelect = ((MenuItem)sender);
            ListaCarros item = (ListaCarros)itemSelect.CommandParameter;

            var localPasta = new LocalRootFolder();
            var pastaCarrosCompactados = localPasta.CreateFolder("CarrosCompactados", CreationCollisionOption.OpenIfExists);

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
            player.Play();
            if (string.IsNullOrEmpty(searchBarCarros.Text))
            {
                await ConsultandoCarros();
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
