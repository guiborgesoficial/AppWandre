using OfficeOpenXml;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                           
                            if (int.Parse(entryKM.Text.Replace(".","")) < 80000)
                            {
                                KM = entryKM.Text + "\nKM";
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
                                loja = "l2";
                            }

                            var valores = new String[] {};

                            if (pickerCambio.SelectedIndex == 1)
                            {
                                if(pickerCompleto.SelectedIndex == 1)
                                {
                                    valores = new String[] {entryModelo.Text.ToUpper(), entryMotor.Text + "\t-\t" +
                                    entryDescricao.Text.ToUpper() + "\t-\t" + pickerCambio.SelectedItem.ToString() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text + "\t-\t" + KM,
                                    entryValor.Text, @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString() + ".png",
                                    @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower() + "-" +
                                    entryPlaca.Text.ToUpper() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                    };
                                }
                                else
                                {
                                    valores = new String[] {entryModelo.Text.ToUpper(), entryMotor.Text + "\t-\t" +
                                    entryDescricao.Text.ToUpper() + "\t-\t" + pickerCambio.SelectedItem.ToString() + "\t-\t" + entryAno.Text + "\t-\t" + KM,
                                    entryValor.Text, @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString() + ".png",
                                    @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower() + "-" +
                                    entryPlaca.Text.ToUpper() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                    };
                                }
                            }
                            else
                            {
                                if (pickerCompleto.SelectedIndex == 1)
                                {
                                    valores = new String[] {entryModelo.Text.ToUpper(), entryMotor.Text + "\t-\t" +
                                    entryDescricao.Text.ToUpper() + "\t-\t" + "COMPLETO" + "\t-\t" + entryAno.Text + "\t-\t" + KM,
                                    entryValor.Text, @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString() + ".png",
                                    @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower() + "-" +
                                    entryPlaca.Text.ToUpper() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                    };
                                }
                                else
                                {
                                    valores = new String[] {entryModelo.Text.ToUpper(), entryMotor.Text + "\t-\t" +
                                    entryDescricao.Text.ToUpper() + "\t-\t" + entryAno.Text + "\t-\t" + KM,
                                    entryValor.Text, @"padrao_carro\marca\" + pickerMarca.SelectedItem.ToString() + ".png",
                                    @"padrao_carro\loja\" + loja + ".png", @"carros_para_fazer_arte\" + entryModelo.Text.ToLower() + "-" +
                                    entryPlaca.Text.ToUpper() + @"\fotos\0" + foto.ToString() + ".jpeg"
                                    };
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
                        //var arquivoCSV = pastaCarro.CreateFile(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".csv"), CreationCollisionOption.OpenIfExists);
                        var arquivoTXT = await pastaCarroEspecifico.CreateFileAsync(string.Concat(entryModelo.Text.ToLower(), "-", entryPlaca.Text.ToUpper(), ".txt"), CreationCollisionOption.OpenIfExists);

                        string descricaoCarroContent = string.Format("{0}\b{1} \n{2} \n{3} \n{4} \nPlaca {5} \n{6} \nR${7}",
                        pickerMarca.SelectedItem.ToString().ToUpper(), entryModelo.Text.ToUpper(), entryMotor.Text, entryDescricao.Text.ToUpper(), entryAno.Text,
                        entryPlaca.Text.ToUpper(), entryKM.Text, entryValor.Text
                        );

                        File.WriteAllBytes(arquivoXLSX.Path, excelPackage.GetAsByteArray());
                        //File.WriteAllBytes(arquivoCSV.Path, excelPackage.GetAsByteArray());
                        File.WriteAllText(arquivoTXT.Path, descricaoCarroContent);

                        PageOpcionais abrirOpicionais = new PageOpcionais();
                        abrirOpicionais.stringPathPasta = pastaCarroEspecifico.Path;
                        abrirOpicionais.stringPathTxt = arquivoTXT.Path;
                        abrirOpicionais.contentDadosCarro = descricaoCarroContent;
                        await Navigation.PushModalAsync(abrirOpicionais);
                    }
                }
                catch (Exception erro)
                {
                    await DisplayAlert("Erro", "Erro ao gerar planilha" + erro, "Ok");
                }
            }
        }
        private bool VerificandoPreenchimentoFormulario()
        {
            if (pickerMarca.SelectedIndex == -1 || entryModelo.Text == string.Empty || entryDescricao.Text == string.Empty
               || entryAno.Text == string.Empty || entryMotor.Text == string.Empty ||
               pickerTipoMotor.SelectedIndex == -1 || pickerCambio.SelectedIndex == -1 ||
               entryKM.Text == string.Empty || entryValor.Text == string.Empty || pickerCompleto.SelectedIndex == -1 
               || entryPlaca.Text == string.Empty
            )
            {
                DisplayAlert("Campos Obrigatórios", "Preencha todos os campos!", "OK");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}