using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWandre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageDadosCarro : ContentPage
    {
        public pageDadosCarro()
        {
            InitializeComponent();
        }
       
        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            if (VerificandoPreenchimentoFormulario())
            {
                Navigation.PushAsync(new pageOpcionais());
            }
        }
        private bool VerificandoPreenchimentoFormulario()
        {
            if (pickerMarca.SelectedIndex == -1 || entryModelo.Text == string.Empty
               || entryAno.Text == string.Empty || entryMotor.Text == string.Empty ||
               pickerTipoMotor.SelectedIndex == -1 || pickerCambio.SelectedIndex == -1 ||
               entryKM.Text == string.Empty || entryValor.Text == string.Empty || pickerCompleto.SelectedIndex == - 1
            )
            {
                DisplayAlert("Campos Obrigatórios", "Preencha todos os campos!", "OK");
                return true; ;
            }
            else
            {
                return true;
            }
        }
    }
}