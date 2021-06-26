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
        public string stringPathPasta;
        public string stringPathTxt;
        public string contentDadosCarro;
        public string contentRetornoOpcionais;
        public int contadorOpcionais = 0;
        public PageOpcionais()
        {
            InitializeComponent();
        }
        private async void BtnSalvarOpcionais_Clicked(object sender, EventArgs e)
        {
            contentRetornoOpcionais = string.Empty;
            await GravarOpcionais();
            PageCamera abrirCamera = new PageCamera();
            abrirCamera.stringPath = stringPathPasta;
            await Navigation.PushModalAsync(abrirCamera);
        }
        private async Task GravarOpcionais()
        {
            try
            {
                contadorOpcionais = 0;
                var localPasta = new LocalRootFolder();
                var pastaCarroEspecifico = await localPasta.GetFolderAsync(stringPathPasta);
                var arquivoCarroTxt = await localPasta.GetFileAsync(stringPathTxt);
                GetQuantidadeOpcionais(gridOpcionais);
                string[] RetornoDadosCarro = File.ReadAllLines(stringPathTxt);
                string[] RetornoOpcionais = contentRetornoOpcionais.Split(',');
                string contentOpcionais = string.Empty;

                for (int i = 0; i < contadorOpcionais; i++)
                {
                    if (int.Parse(RetornoDadosCarro[5].Replace(".", "")) < 80000)
                    {
                        if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                        {
                            int indexTravas = Array.IndexOf(RetornoOpcionais, "travas");
                            int indexVidros = Array.IndexOf(RetornoOpcionais, "vidros");

                            if (i < contadorOpcionais - 1)
                            {
                                if (i == indexVidros || i == indexTravas)
                                {
                                    i++;
                                }
                                else
                                {
                                    contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                                }
                            }
                            else
                            {
                                contentOpcionais += string.Format("{0}, {1} e {2} elétricas com {3} KM. ", RetornoOpcionais[i],
                                RetornoOpcionais[indexVidros], RetornoOpcionais[indexTravas], RetornoDadosCarro[5]);
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
                                contentOpcionais += string.Format("{0} com {1} KM. ", RetornoOpcionais[i], RetornoDadosCarro[5]);
                            }
                        }
                    }
                    else
                    {
                        if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                        {
                            int indexTravas = Array.IndexOf(RetornoOpcionais, "travas");
                            int indexVidros = Array.IndexOf(RetornoOpcionais, "vidros");

                            if (i < contadorOpcionais - 1)
                            {
                                if (i == indexVidros || i == indexTravas)
                                {
                                    i++;
                                }
                                else
                                {
                                    contentOpcionais += string.Format("{0}, ", RetornoOpcionais[i]);
                                }
                            }
                            else
                            {
                                contentOpcionais += string.Format("{0}, {1} e {2} elétricas. ", RetornoOpcionais[i],
                                RetornoOpcionais[indexVidros], RetornoOpcionais[indexTravas]);
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
                                contentOpcionais += string.Format("{0}.", RetornoOpcionais[i]);
                            }
                        }
                    }
                }
                string txtAnuncio = string.Format("\n{0}- {1}- {2}- {3}com {4} \nPor apenas {5},00.",
                RetornoDadosCarro[0], RetornoDadosCarro[1], RetornoDadosCarro[2], RetornoDadosCarro[3], contentOpcionais, RetornoDadosCarro[6]);
                File.AppendAllText(arquivoCarroTxt.Path, txtAnuncio);
            }
            catch (Exception erro)
            {
                await DisplayAlert("Erro - Capture a Tela e contacte o desenvolvedor", erro.Message, "OK");
            }
        }
        private int GetQuantidadeOpcionais(Grid grid)
        {
            foreach (CheckBox verificar in grid.Children)
            {
                if (verificar.GetType().Equals(typeof(CheckBox)))
                {
                    if (verificar.IsChecked)
                    {
                        switch (verificar.StyleId)
                        {
                            case "chkBoxAlarme":
                                contentRetornoOpcionais += "alarme,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxAirbag":
                                contentRetornoOpcionais += "AirBag,";
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
                                contadorOpcionais++;
                                break;
                            case "chkBoxGPS":
                                contentRetornoOpcionais += "GPS,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxRadio":
                                contentRetornoOpcionais += "rádio,";
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
                            case "chkBox4x4":
                                contentRetornoOpcionais += "4X4,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxMyLink":
                                contentRetornoOpcionais += "MYLINK,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxTetoSolar":
                                contentRetornoOpcionais += "teto solar,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxBancoCouro":
                                contentRetornoOpcionais += "bancos em couro,";
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
                                contentRetornoOpcionais += "encosto de cabeça,";
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
                            case "chkBoxSomVolante":
                                contentRetornoOpcionais += "comando de som no volante,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxVolMultifuncional":
                                contentRetornoOpcionais += "volante multifuncional,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxEletroHidraulica":
                                contentRetornoOpcionais += "direção eletro-hidráulica,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxFotocromico":
                                contentRetornoOpcionais += "retrovisor fotocrômico,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxCDPlayer":
                                contentRetornoOpcionais += "CD Player,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxChaveReserva":
                                contentRetornoOpcionais += "chave reserva,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxManual":
                                contentRetornoOpcionais += "manual do proprietário,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxHidraulica":
                                contentRetornoOpcionais += "direção hidráulica,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxEletrica":
                                contentRetornoOpcionais += "direção elétrica,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxCanivete":
                                contentRetornoOpcionais += "chave canivete,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxIsofix":
                                contentRetornoOpcionais += "ISOFIX,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxCamRe":
                                contentRetornoOpcionais += "câmera de ré,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxSantoAntonio":
                                contentRetornoOpcionais += "Santo Antônio,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxEstriboLateral":
                                contentRetornoOpcionais += "estribos laterais,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxMaritma":
                                contentRetornoOpcionais += "capota marítma,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxDualogic":
                                contentRetornoOpcionais += "câmbio Dualogic,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxBorboleta":
                                contentRetornoOpcionais += "câmbio borboleta,";
                                contadorOpcionais++;
                                break;
                            case "chkBoxCVT":
                                contentRetornoOpcionais += "câmbio CVT,";
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