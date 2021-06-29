using OfficeOpenXml;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDadosCarro : ContentPage
    {
        public PageDadosCarro()
        {
            InitializeComponent();
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            activIndicator.IsRunning = true;
            activIndicator.IsVisible = true;
            btnSalvar.IsEnabled = false;
            await GerarPlanilha();
        }
        private async Task GerarPlanilha()
        {
            if (VerificandoPreenchimentoFormulario())
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var excelPackage = new ExcelPackage())
                    {
                        string KM;
                        excelPackage.Workbook.Properties.Author = "guiborgesoficial";
                        excelPackage.Workbook.Properties.Title = "Meu Excel";

                        var sheet = excelPackage.Workbook.Worksheets.Add("Planilha 1");
                        sheet.Name = "Planilha 1";

                        var coluna = 1;
                        var headers = new String[] { "modelo", "descricao", "valor", "marca", "loja", "foto" };

                        foreach (var titulo in headers)
                        {
                            sheet.Cells[1, coluna++].Value = titulo;
                        }

                        int foto = 0;
                        string loja = "wl";

                        for (int linha = 2; linha < 20; linha++)
                        {

                            if (int.Parse(entryKM.Text.Replace(".", "")) < 80000)
                            {
                                if(int.Parse(entryKM.Text.Replace(".","")) == 0)
                                {
                                    KM = " - ZERO KM";
                                }
                                else
                                {
                                    KM = "- " + entryKM.Text + "\tKM";
                                }
                            }
                            else
                            {
                                KM = string.Empty;
                            }

                            coluna = 1;

                            if (foto < 9)
                            {
                                foto++;
                            }
                            else
                            {
                                foto = 1;
                                loja = "lj2";
                            }

                            var valores = new String[] { };
                            
                            if(pickerTipoMotor.SelectedIndex == 2 || pickerTipoMotor.SelectedIndex == 3)
                            {
                                if (pickerCambio.SelectedIndex == 1)
                                {
                                    if (pickerCompleto.SelectedIndex == 1)
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                }
                                else
                                {
                                    if (pickerCompleto.SelectedIndex == 1)
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                }
                            }
                            else
                            {
                                if (pickerCambio.SelectedIndex == 1)
                                {
                                    if (pickerCompleto.SelectedIndex == 1)
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                }
                                else
                                {
                                    if (pickerCompleto.SelectedIndex == 1)
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                }
                            }
                            
                            foreach (var valor in valores)
                            {
                                sheet.Cells[linha, coluna++].Value = valor;
                            }
                        }

                        var localPasta = new LocalRootFolder();
                        var pastaCarros = await localPasta.CreateFolderAsync("Carros", CreationCollisionOption.OpenIfExists);
                        var pastaCarroEspecifico = await pastaCarros.CreateFolderAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper()), CreationCollisionOption.OpenIfExists);

                        var arquivoXLSX = await pastaCarroEspecifico.CreateFileAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".xlsx"), CreationCollisionOption.OpenIfExists);
                        //var arquivoCSV = pastaCarros.CreateFile(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".csv"), CreationCollisionOption.OpenIfExists);
                        var arquivoTXT = await pastaCarroEspecifico.CreateFileAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".txt"), CreationCollisionOption.OpenIfExists);

                        string descricaoCarroContent = string.Format("{0}\b{1} \n{2} \n{3} \n{4} \n{5} \n{6} \nPlaca {7} \n{8} KM \nR$ {9}",
                        pickerMarca.SelectedItem.ToString().ToUpper().Trim(), entryModelo.Text.ToUpper().Trim(), entryMotor.Text.Trim(), 
                        entryDescricao.Text.ToUpper().Trim(), entryAno.Text.Trim(), pickerTipoMotor.SelectedItem.ToString().Trim(), 
                        pickerCambio.SelectedItem.ToString().Trim(), entryPlaca.Text.ToUpper().Trim(), entryKM.Text.Trim(), entryValor.Text.Trim()
                        );

                        File.WriteAllBytes(arquivoXLSX.Path, excelPackage.GetAsByteArray());
                        //File.WriteAllBytes(arquivoCSV.Path, excelPackage.GetAsByteArray());
                        File.WriteAllText(arquivoTXT.Path, descricaoCarroContent);
                        excelPackage.Dispose();

                        PageOpcionais abrirOpicionais = new PageOpcionais
                        {
                            stringPathPasta = pastaCarroEspecifico.Path,
                            stringPathTxt = arquivoTXT.Path,
                            contentDadosCarro = descricaoCarroContent
                        };
                        await Navigation.PushAsync(abrirOpicionais);
                    }
                }
                catch (Exception erro)
                {
                    await DisplayAlert("Erro", "Erro ao gerar planilha. Tire um print e contacte o desenvolvedor" + erro, "Ok");
                    btnSalvar.IsEnabled = true;
                }
                activIndicator.IsRunning = false;
                activIndicator.IsVisible = false;
            }
        }
        private bool VerificandoPreenchimentoFormulario()
        {
            var regexTratamentoInput = new Regex(@"^[a-zA-Z0-9-.\s]+$");
            if (pickerMarca.SelectedIndex == -1 || entryModelo.Text == string.Empty || entryDescricao.Text == string.Empty
               || entryAno.Text.Length != 4 || entryMotor.Text.Length != 3 ||
               pickerTipoMotor.SelectedIndex == -1 || pickerCambio.SelectedIndex == -1 ||
               entryKM.Text == string.Empty || entryValor.Text.Length < 5 || pickerCompleto.SelectedIndex == -1
               || entryPlaca.Text == string.Empty
            )
            {
                DisplayAlert("Campos Obrigatórios", "Preencha todos os campos!", "OK");
                btnSalvar.IsEnabled = true;
                activIndicator.IsRunning = false;
                activIndicator.IsVisible = false;
                return false;
            }
            else if(!regexTratamentoInput.IsMatch(entryModelo.Text) || !regexTratamentoInput.IsMatch(entryDescricao.Text) || !regexTratamentoInput.IsMatch(entryPlaca.Text))
            {
                DisplayAlert("Campos incorretos", "Os campos não admitem caracteres especiais. Apenas hífen (-) e ponto (.) são permitidos.", "OK");
                btnSalvar.IsEnabled = true;
                activIndicator.IsRunning = false;
                activIndicator.IsVisible = false;
                return false;
            }
            else
            {
                if(!entryKM.Text.Contains(".") || !entryValor.Text.Contains("."))
                {
                    DisplayAlert("Campos Incorretos", "Os campos KM e/ou Valor estão incorretos. Utilize o ponto (.) como separador decimal.", "OK");
                    btnSalvar.IsEnabled = true;
                    activIndicator.IsRunning = false;
                    activIndicator.IsVisible = false;
                    return false;
                }
                else
                {
                    string[] retornoValor = entryValor.Text.Split('.');
                    string[] retornoKM = entryValor.Text.Split('.');
                    if (retornoValor[1].Length == 3 || retornoKM[1].Length == 3)
                    {
                        return true;
                    }
                    else
                    {
                        DisplayAlert("Campos Incorretos", "Os campos KM e/ou Valor estão incorretos. Verifique a quantidade de zeros depois do separador decimal.", "OK");
                        btnSalvar.IsEnabled = true;
                        activIndicator.IsRunning = false;
                        activIndicator.IsVisible = false;
                        return false;
                    }
                }
            }
        }
    }
}