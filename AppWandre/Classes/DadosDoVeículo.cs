using System;
using System.Collections.Generic;
using System.Text;

namespace WandreMobile.Classes
{
    public class DadosDoVeículo
    {
        string Marca { get; set; }
        string Modelo { get; set; }
        string Ano { get; set; }
        string Motor { get; set; }
        string TipoDoMotor { get; set; }
        string Cambio { get; set; }
        string Kilometragem { get; set; }
        string Valor { get; set; }
    }
    public partial class Opcionais
    {
        string Alarme { get; set; }
        string Airbag { get; set; }
        string ArQuente { get; set; }
        string ABS { get; set; }
        string Vidro { get; set; }
        string Travas { get; set; }
        string GPS { get; set; }
        string Rádio { get; set; }
        string LigaLeve { get; set; }
        string StarStop { get; set; }
        string ArCondicionado { get; set; }
        string PCdeBordo { get; set; }
        string ChavePresencial { get; set; }
        string EncostoCabeça { get; set; }
        string SensorEstacionamento { get; set; }
        string DesembaçadorTraseiro { get; set; }
        string ControleTraçao { get; set; }
        string LimpadorTraseiro { get; set; }
        string ControleVelocidade { get; set; }
        string RetrovisoresEletricos { get; set; }
    }
}
