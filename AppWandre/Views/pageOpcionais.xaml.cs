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

                GetQuantidadeOpcionais();
                /*
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
                File.AppendAllText(arquivoCarroTxt.Path, txtAnuncio);*/
                await DisplayAlert("Erro - Capture a Tela e contacte o desenvolvedor", contentRetornoOpcionais, "OK");
            }
            catch (Exception erro)
            {
                await DisplayAlert("Erro - Capture a Tela e contacte o desenvolvedor", erro.Message, "OK");
            }
        }
        private int GetQuantidadeOpcionais()
        {
            if(chkBoxAlarme.IsChecked)
            {
                contentRetornoOpcionais += "alarme,";
                contadorOpcionais++;
            }
            if(chkBoxAirbag.IsChecked)
            {
                contentRetornoOpcionais += "AirBag,";
                contadorOpcionais++;
            }
            if(chkBoxArQuente.IsChecked)
            {
                contentRetornoOpcionais += "ar quente,";
                contadorOpcionais++;
            }
            if(chkBoxABS.IsChecked)
            {
                contentRetornoOpcionais += "freio ABS,";
                contadorOpcionais++;
            }
            if(chkBoxVidro.IsChecked)
            {
                contentRetornoOpcionais += "vidros elétricos,";
                contadorOpcionais++;
            }
            if(chkBoxTravas.IsChecked)
            {
                contentRetornoOpcionais += "travas elétricas,";
                contadorOpcionais++;
            }
            if(chkBoxGPS.IsChecked)
            {
                contentRetornoOpcionais += "GPS,";
                contadorOpcionais++;
            }
            if(chkBoxRadio.IsChecked)
            {
                contentRetornoOpcionais += "rádio,";
                contadorOpcionais++;
            }
            if(chkBoxLigaLeve.IsChecked)
            {
                contentRetornoOpcionais += "rodas de liga leve,";
                contadorOpcionais++;
            }
            if(chkBoxStartStop.IsChecked)
            {
                contentRetornoOpcionais += "Start-Stop,";
                contadorOpcionais++;
            }
            if(chkBox4x4.IsChecked)
            {
                contentRetornoOpcionais += "4X4,";
                contadorOpcionais++;
            }
            if(chkBoxMyLink.IsChecked)
            {
                contentRetornoOpcionais += "MYLINK,";
                contadorOpcionais++;
            }
            if(chkBoxTetoSolar.IsChecked)
            {
                contentRetornoOpcionais += "teto solar,";
                contadorOpcionais++;
            }
            if(chkBoxBancoCouro.IsChecked)
            {
                contentRetornoOpcionais += "bancos em couro,";
                contadorOpcionais++;
            }
            if(chkBoxArCondicionado.IsChecked)
            {
                contentRetornoOpcionais += "ar condicionado,";
                contadorOpcionais++;
            }
            if(chkBoxPCdeBordo.IsChecked)
            {
                contentRetornoOpcionais += "computador de bordo,";
                contadorOpcionais++;
            }
            if(chkBoxChavePresencial.IsChecked)
            {
                contentRetornoOpcionais += "chave presencial,";
                contadorOpcionais++;
            }
            if(chkBoxEncostoCabeça.IsChecked)
            {
                contentRetornoOpcionais += "encosto de cabeça,";
                contadorOpcionais++;
            }
            if(chkBoxSensorEstacionamento.IsChecked)
            {
                contentRetornoOpcionais += "sensor de estacionamento,";
                contadorOpcionais++;
            }
            if(chkBoxDesembaçadorTraseiro.IsChecked)
            {
                contentRetornoOpcionais += "desembaçador traseiro,";
                contadorOpcionais++;
            }
            if(chkBoxControleTração.IsChecked)
            {
                contentRetornoOpcionais += "controle de tração,";
                contadorOpcionais++;
            }
            if(chkBoxLimpadorTraseiro.IsChecked)
            {
                contentRetornoOpcionais += "limpador traseiro,";
                contadorOpcionais++;
            }
            if(chkBoxControleVelocidade.IsChecked)
            {
                contentRetornoOpcionais += "controle automático da velocidade,";
                contadorOpcionais++;
            }
            if(chkBoxRetrovisoresEletricos.IsChecked)
            {
                contentRetornoOpcionais += "retrovisores elétricos,";
                contadorOpcionais++;
            }
            if(chkBoxSomVolante.IsChecked)
            {
                contentRetornoOpcionais += "comando de som no volante,";
                contadorOpcionais++;
            }
            if(chkBoxVolMultifuncional.IsChecked)
            {
                contentRetornoOpcionais += "volante multifuncional,";
                contadorOpcionais++;
            }
            if(chkBoxEletroHidraulica.IsChecked)
            {
                contentRetornoOpcionais += "direção eletro-hidráulica,";
                contadorOpcionais++;
            }
            if(chkBoxFotocromico.IsChecked)
            {
                contentRetornoOpcionais += "retrovisor fotocrômico,";
                contadorOpcionais++;
            }
            if(chkBoxCDPlayer.IsChecked)
            {
                contentRetornoOpcionais += "CD Player,";
                contadorOpcionais++;
            }
            if(chkBoxChaveReserva.IsChecked)
            {
                contentRetornoOpcionais += "chave reserva,";
                contadorOpcionais++;
            }
            if(chkBoxManual.IsChecked)
            {
                contentRetornoOpcionais += "manual do proprietário,";
                contadorOpcionais++;
            }
            if(chkBoxHidraulica.IsChecked)
            {
                contentRetornoOpcionais += "direção hidráulica,";
                contadorOpcionais++;
            }
            if(chkBoxEletrica.IsChecked)
            {
                contentRetornoOpcionais += "direção elétrica,";
                contadorOpcionais++;
            }
            if(chkBoxCanivete.IsChecked)
            {
                contentRetornoOpcionais += "chave canivete,";
                contadorOpcionais++;
            }
            if(chkBoxIsofix.IsChecked)
            {
                contentRetornoOpcionais += "ISOFIX,";
                contadorOpcionais++;
            }
            if(chkBoxCamRe.IsChecked)
            {
                contentRetornoOpcionais += "câmera de ré,";
                contadorOpcionais++;
            }
            if(chkBoxSantoAntonio.IsChecked)
            {
                contentRetornoOpcionais += "Santo Antônio,";
                contadorOpcionais++;
            }
            if(chkBoxEstriboLateral.IsChecked)
            {
                contentRetornoOpcionais += "estribos laterais,";
                contadorOpcionais++;
            }
            if(chkBoxMaritma.IsChecked)
            {
                contentRetornoOpcionais += "capota marítma,";
                contadorOpcionais++;
            }
            if(chkBoxDualogic.IsChecked)
            {
                contentRetornoOpcionais += "câmbio Dualogic,";
                contadorOpcionais++;
            }
            if(chkBoxBorboleta.IsChecked)
            {
                contentRetornoOpcionais += "câmbio borboleta,";
                contadorOpcionais++;
            }
            if(chkBoxCVT.IsChecked)
            {
                contentRetornoOpcionais += "câmbio CVT,";
                contadorOpcionais++;
            }
            return contadorOpcionais;
        }
    }
}