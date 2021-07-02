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
        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Atenção", "Não é possível voltar para as páginas anteriores. Complete a rotina para prosseguir.", "OK");
            return true;
        }
        private async void BtnSalvarOpcionais_Clicked(object sender, EventArgs e)
        {
            activIndicator.IsRunning = true;
            activIndicator.IsVisible = true;
            btnSalvarOpcionais.IsEnabled = false;
            contentRetornoOpcionais = string.Empty;
            await GravarOpcionais();
            PageCamera abrirCamera = new PageCamera
            {
                stringPath = stringPathPasta
            };
            await Navigation.PushAsync(abrirCamera);
        }
        private async Task GravarOpcionais()
        {
            btnSalvarOpcionais.IsEnabled = true;
            try
            {
                contadorOpcionais = 0;
                
                var localPasta = new LocalRootFolder();
                var pastaCarroEspecifico = await localPasta.GetFolderAsync(stringPathPasta);
                var arquivoCarroTxt = await localPasta.GetFileAsync(stringPathTxt);
                GetQuantidadeOpcionais();
                string[] RetornoDadosCarro = File.ReadAllLines(stringPathTxt);
                string[] RetornoOpcionais = contentRetornoOpcionais.Split(',');
                string contentOpcionais = string.Empty;

                for (int i = 0; i < contadorOpcionais; i++)
                {
                    if (int.Parse(RetornoDadosCarro[7].Replace(".", "").Replace("KM","")) < 80000)
                    {
                        if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                        {
                            int indexTravas = Array.IndexOf(RetornoOpcionais, "travas elétricas");
                            int indexVidros = Array.IndexOf(RetornoOpcionais, "vidros elétricos");

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
                                if(int.Parse(RetornoDadosCarro[7].Replace(".","").Replace("KM","")) == 0)
                                {
                                    contentOpcionais += string.Format("{0}, {1} e {2}, ZERO KM.", RetornoOpcionais[i],
                                    RetornoOpcionais[indexVidros], RetornoOpcionais[indexTravas]);
                                }
                                else
                                {
                                    contentOpcionais += string.Format("{0}, {1} e {2} com {3}.", RetornoOpcionais[i],
                                    RetornoOpcionais[indexVidros], RetornoOpcionais[indexTravas], RetornoDadosCarro[7].Trim());
                                }
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
                                if (int.Parse(RetornoDadosCarro[7].Replace(".", "").Replace("KM","")) == 0)
                                {
                                    contentOpcionais += string.Format("{0}, ZERO KM.", RetornoOpcionais[i]);
                                }
                                else
                                {
                                    contentOpcionais += string.Format("{0} com {1}.", RetornoOpcionais[i], RetornoDadosCarro[7].Trim());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (contentRetornoOpcionais.Contains("vidros") && contentRetornoOpcionais.Contains("travas"))
                        {
                            int indexTravas = Array.IndexOf(RetornoOpcionais, "travas elétricas");
                            int indexVidros = Array.IndexOf(RetornoOpcionais, "vidros elétricos");

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
                                contentOpcionais += string.Format("{0}, {1} e {2}.", RetornoOpcionais[i],
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

                if(RetornoDadosCarro[4].Contains("Flex") || RetornoDadosCarro[4].Contains("Diesel"))
                {
                    string txtAnuncio = string.Format("\n{0}- {1}{2}- {3}- {4}com {5} \n\nPor apenas {6},00.",
                    RetornoDadosCarro[0], RetornoDadosCarro[1], RetornoDadosCarro[4].ToUpper(), RetornoDadosCarro[2], RetornoDadosCarro[3], contentOpcionais, RetornoDadosCarro[8]);
                    File.AppendAllText(arquivoCarroTxt.Path, txtAnuncio);
                }
                else
                {
                    string txtAnuncio = string.Format("\n\n{0}- {1}- {2}- {3}com {4} \n\nPor apenas {5},00.",
                    RetornoDadosCarro[0], RetornoDadosCarro[1], RetornoDadosCarro[2], RetornoDadosCarro[3], contentOpcionais, RetornoDadosCarro[8]);
                    File.AppendAllText(arquivoCarroTxt.Path, txtAnuncio);
                }                
            }
            catch (Exception erro)
            {
                await DisplayAlert("Erro - Capture a Tela e contacte o desenvolvedor", erro.ToString(), "OK");
                btnSalvarOpcionais.IsEnabled = true;
            }
            activIndicator.IsRunning = false;
            activIndicator.IsVisible = false;
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