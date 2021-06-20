using PCLExt.FileStorage.Folders;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageOpcionais : ContentPage
    {
        public string stringPath;
        public string contentDadosCarro;
        public string contentRetornoOpcionais;
        public int contadorOpcionais = 0;
        public PageOpcionais()
        {
            InitializeComponent();
        }
        private async void BtnSalvarOpcionais_Clicked(object sender, EventArgs e)
        {
            await GravarOpcionais();
            await Navigation.PushModalAsync(new PageCamera());
        }
        private async Task GravarOpcionais()
        {
            contadorOpcionais = 0;
            var localPasta = new LocalRootFolder();
            //var pastaCarroEspecifico = await localPasta.GetFolderAsync(stringPath);
            //var arquivoCarroTxt = await localPasta.GetFileAsync(stringPath);
            GetQuantidadeOpcionais(stackLayout01);
            GetQuantidadeOpcionais(stackLayout02);
            await DisplayAlert("S", contentRetornoOpcionais, "pk");
            string[] RetornoDadosCarro = File.ReadAllLines(stringPath);
            string[] RetornoOpcionais = contentRetornoOpcionais.Split(',');
            string contentOpcionais = string.Format("{0} - {1} - {2}, {3} com ",
            RetornoOpcionais[0], RetornoOpcionais[1], RetornoOpcionais[2], RetornoOpcionais[3]);

            for (int i = 0; i < contadorOpcionais; i ++)
            {
                if(int.Parse(RetornoDadosCarro[6]) < 80000)
                {
                    int index = Array.IndexOf(RetornoOpcionais, "travas");
                    if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                    {
                        if (i < contadorOpcionais - 1)
                        {
                            contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                        }
                        else
                        {
                            contentOpcionais += string.Format("{0} com {1} KM. ", RetornoOpcionais[i], RetornoDadosCarro[6]);
                        }
                    }
                    else
                    {
                        if (i < contadorOpcionais - 1)
                        {
                            contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                        }
                        else
                        {
                            contentOpcionais += string.Format("{0} com {1} KM. ", RetornoOpcionais[i], RetornoDadosCarro[6]);
                        }
                    }
                }
                else
                {
                    if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                    {
                        if (i < contadorOpcionais - 1)
                        {
                            contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                        }
                        else
                        {
                            contentOpcionais += string.Format("{0}. ", RetornoOpcionais[i]);
                        }
                    }
                    else
                    {
                        if (i < contadorOpcionais - 1)
                        {
                            contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                        }
                        else
                        {
                            contentOpcionais += string.Format("{0}s.", RetornoOpcionais[i]);
                        }
                    }
                }
            }
        }
        private int GetQuantidadeOpcionais(StackLayout stack)
        {
            foreach (CheckBox verificar in stack.Children)
            {
                if (verificar.GetType().Equals(typeof(CheckBox)))
                {
                    if (verificar.IsChecked)
                    {
                        switch(verificar.StyleId)
                        {
                            case "chkBoxAlarme":
                                contentRetornoOpcionais += "alarme,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxAirbag":
                                contentRetornoOpcionais += "Air Bag,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxArQuente":
                                contentRetornoOpcionais += "ar quente,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxABS":
                                contentRetornoOpcionais += "freio ABS,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxVidro":
                                contentRetornoOpcionais += "vidros elétricos,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxTravas":
                                contentRetornoOpcionais += "travas elétricas,";
                                break;
                            case "chkBoxGPS":
                                contentRetornoOpcionais += "GPS,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxRadio":
                                contentRetornoOpcionais += "Rádio,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxLigaLeve":
                                contentRetornoOpcionais += "rodas de liga leve,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxStartStop":
                                contentRetornoOpcionais += "Start-Stop,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxArCondicionado":
                                contentRetornoOpcionais += "ar condicionado,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxPCdeBordo":
                                contentRetornoOpcionais += "computador de bordo,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxChavePresencial":
                                contentRetornoOpcionais += "chave presencial,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxEncostoCabeça":
                                contentRetornoOpcionais += "enconsto de cabeça,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxSensorEstacionamento":
                                contentRetornoOpcionais += "sensor de estacionamento,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxDesembaçadorTraseiro":
                                contentRetornoOpcionais += "desembaçador traseiro,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxControleTração":
                                contentRetornoOpcionais += "controle de tração,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxLimpadorTraseiro":
                                contentRetornoOpcionais += "limpador traseiro,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxControleVelocidade":
                                contentRetornoOpcionais += "controle automático da velocidade,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxRetrovisoresEletricos":
                                contentRetornoOpcionais += "retrovisores elétricos,";
                                contadorOpcionais++;
                                break;
                        }
                    }
                }
            }
                return contadorOpcionais;
        }
    }
}