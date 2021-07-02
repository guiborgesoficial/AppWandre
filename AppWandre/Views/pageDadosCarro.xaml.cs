using OfficeOpenXml;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Globalization;
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
                                if(int.Parse(entryKM.Text.Replace(".", "")) == 0)
                                {
                                    KM = "-ZERO KM";
                                }
                                else
                                {
                                    KM = "-" + entryKM.Text + " KM";
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
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + " " + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + " " + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
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
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + " " + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + " " + pickerTipoMotor.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" +
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
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + " " + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + pickerCambio.SelectedItem.ToString().ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + " " + KM,
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
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text.Trim() + " " + KM,
                                        entryValor.Text.Trim(), @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString().ToLower().Trim() + ".png",
                                        @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower().Trim() + "-" +
                                        entryPlaca.Text.ToUpper().Trim() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                        };
                                    }
                                    else
                                    {
                                        valores = new String[] {entryModelo.Text.ToUpper().Trim().Replace("\n",""), entryMotor.Text.Trim() + "\t-\t" +
                                        entryDescricao.Text.ToUpper().Trim() + "\t-\t" + entryAno.Text.Trim() + " " + KM,
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
                        var arquivoCSV = await pastaCarroEspecifico.CreateFileAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".csv"), CreationCollisionOption.OpenIfExists);
                        var arquivoTXT = await pastaCarroEspecifico.CreateFileAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".txt"), CreationCollisionOption.OpenIfExists);

                        string descricaoCarroContent = string.Format("{0} {1} \n{2} \n{3} \n{4} \n{5} \n{6} \nPlaca {7} \n{8} KM \nR$ {9}",
                        pickerMarca.SelectedItem.ToString().ToUpper().Trim(), entryModelo.Text.ToUpper().Trim(), entryMotor.Text.Trim(), 
                        entryDescricao.Text.ToUpper().Trim(), entryAno.Text.Trim(), pickerTipoMotor.SelectedItem.ToString().Trim(), 
                        pickerCambio.SelectedItem.ToString().Trim(), entryPlaca.Text.ToUpper().Trim(), entryKM.Text.Trim(), entryValor.Text.Trim()
                        );
                        var file = new FileInfo(arquivoCSV.Path);
                        var format = new ExcelOutputTextFormat();

                        File.WriteAllBytes(arquivoXLSX.Path, excelPackage.GetAsByteArray());
                        await sheet.Cells["A1:F19"].SaveToTextAsync(file, format);
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
            if (pickerMarca.SelectedIndex == -1 || string.IsNullOrEmpty(entryModelo.Text)|| string.IsNullOrEmpty(entryDescricao.Text)
               || string.IsNullOrEmpty(entryAno.Text) || string.IsNullOrEmpty(entryMotor.Text) ||
               pickerTipoMotor.SelectedIndex == -1 || pickerCambio.SelectedIndex == -1 ||
               string.IsNullOrEmpty(entryKM.Text) || string.IsNullOrEmpty(entryValor.Text) || pickerCompleto.SelectedIndex == -1
               || string.IsNullOrEmpty(entryPlaca.Text)
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
                if(entryAno.Text.Length != 4 || entryMotor.Text.Length != 3 || entryValor.Text.Length < 5)
                {
                    DisplayAlert("Complete os campos", "Os campos Ano e/ou Motor e/ou Valor estão incompletos.", "OK");
                    btnSalvar.IsEnabled = true;
                    activIndicator.IsRunning = false;
                    activIndicator.IsVisible = false;
                    return false;
                }
                else if(entryAno.Text.Contains("."))
                {
                    DisplayAlert("Campo incorreto", "O campo Ano não admite o ponto (.).", "OK");
                    btnSalvar.IsEnabled = true;
                    activIndicator.IsRunning = false;
                    activIndicator.IsVisible = false;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void EntryKM_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                entryKM.Text = String.Format(new CultureInfo("pt-BR"), "{0:0,0}", Convert.ToInt32(entryKM.Text.Replace(".", "")));
            }
            catch
            {
                entryKM.Text = e.OldTextValue;
            }
        }

        private void EntryValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                entryValor.Text = String.Format(new CultureInfo("pt-BR"), "{0:0,0}", Convert.ToInt32(entryValor.Text.Replace(".", "")));
            }
            catch
            {
                entryValor.Text = e.OldTextValue;
            }
        }

        private void EntryMotor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(entryMotor.Text.Length <= 3)
            {
                if(e.OldTextValue == null || e.OldTextValue.Length == 0)
                { 
                    entryMotor.Text += ".";
                }
            }
            else
            {
                entryMotor.Text = e.OldTextValue;
            }
        }

        private void EntryAno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(entryAno.Text.Length > 4)
            {
                entryAno.Text = e.OldTextValue;
            }
        }
    }
}