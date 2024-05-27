using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using MVVM_TarjetaCredito.Models;
using Newtonsoft.Json;

namespace MVVM_TarjetaCredito.ViewModels;

public struct EstructIdioma
{
    public string ID;
    public string? ES;
    public string? EN;
}
public sealed partial class MainWindowViewModel : INotifyPropertyChanged
{
    string _t1 = "4857";
    string _t2 = "6961";
    string _t3 = "7919";
    string _t4 = "2589";
    string _titular = "Pepito Grillo y Familia";
    string _fecha = "";
    bool _tipoTarjeta = true;
    bool _valida = true;

    readonly Tarjeta _tarjeta;

    private string _idioma = "ES";
    private List<EstructIdioma>? _cadenas = CargarJson("Assets/idioma.json");
    
    public MainWindowViewModel()
    {
        _tarjeta = new Tarjeta(_t1 + _t2 + _t3 + _t4, _titular);
    }
    private void NotificarCambioIdiomas()
    {
        OnPropertyChanged(nameof(LabelTitulo));
        OnPropertyChanged(nameof(LabelNumTarjeta));
        OnPropertyChanged(nameof(LabelTitular));
        OnPropertyChanged(nameof(LabelSalir));
        OnPropertyChanged(nameof(LabelValidar));
        OnPropertyChanged(nameof(LabelTarjetaNoValida));
        OnPropertyChanged((nameof(EnEspanol)));
    }

    public bool EnEspanol => _idioma == "ES";
    
    private string? LabelGenerica(int id)
    {
        string? salida = String.Empty;
        switch (_idioma) 
        {
            case "EN":
                salida =  _cadenas![id].EN;
                break;
            case "ES": 
                salida =  _cadenas![id].ES;
                break;
        };
        return salida;    
    }

    public string? LabelTitulo => LabelGenerica(0);
    public string? LabelNumTarjeta => LabelGenerica(1);
    public string? LabelTitular => LabelGenerica(2);
    public string? LabelValidar => LabelGenerica(3);
    public string? LabelSalir => LabelGenerica(4);
    public string? LabelTarjetaNoValida => LabelGenerica(5);

    public string Titular
    {
        get => _titular;
        set
        {
            if (_titular != value)
            {
                _titular = value;
                OnPropertyChanged();
            }
        }
    }
    private static List<EstructIdioma>? CargarJson(string ruta) {
        if (!File.Exists(ruta))
            return null;

        string json = File.ReadAllText(ruta); 
        return JsonConvert.DeserializeObject<List<EstructIdioma>>(json);
    }
    public string T1
    {
        get => _t1;
        set
        {
            if (_t1 != value)
            {
                _t1 = value;
                _tarjeta.Numero = _t1 + _t2 + _t3 + _t4;
                OnPropertyChanged();
            }
        }
    }
    public string T2
    {
        get => _t2;
        set
        {
            if (_t2 != value)
            {
                _t2 = value;
                _tarjeta.Numero = _t1 + _t2 + _t3 + _t4;
                OnPropertyChanged();
            }
        }
    }
    public string T3
    {
        get => _t3;
        set
        {
            if (_t3 != value)
            {
                _t3 = value;
                _tarjeta.Numero = _t1 + _t2 + _t3 + _t4;
                OnPropertyChanged();
            }
        }
    }
    public string T4
    {
        get => _t4;
        set
        {
            if (_t4 != value)
            {
                _t4 = value;
                _tarjeta.Numero = _t1 + _t2 + _t3 + _t4;
                OnPropertyChanged();
            }
        }
    }
    public string FechaValidez
    {
        get => _tarjeta.FechaValidez;
        set {
            if (_fecha != value)
            {
                _fecha = value;
                OnPropertyChanged();
            }
        }
    }
    public bool TarjetaNoValida
    {
        get => _valida;
        set
        {
            if (_valida != value)
            {
                _valida = value;
                OnPropertyChanged();
            }
        }
    }
    
    public bool TipoTarjeta 
    {
        get => _tipoTarjeta;
        set
        {
            if (_tipoTarjeta != value)
            {
                _tipoTarjeta = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RutaImagenTarjeta));
            }
        }
    }
    public string RutaImagenTarjeta
    {
        get {
            if (TipoTarjeta)
                return "avares://MVVM_TarjetaCredito/Assets/visa.jpg";
            else
                return "avares://MVVM_TarjetaCredito/Assets/mastercard.jpg";
        }
    }

    private string _lblEstado = "vacio";
    public string LblEstado
    {
        get => _lblEstado;
        set
        {
            _lblEstado = value;
            OnPropertyChanged();
        }
    }
    
    public void CmdSalir(object ventana)
    {
        if (ventana is Window window)
            window.Close();
    }

    public void CmdIdioma(string eleccion)
    {
        _idioma = eleccion;
        LblEstado = $"Idioma seleccionado: {_idioma}";
        NotificarCambioIdiomas();
    }
    public void CmdValidar()
    {
        TarjetaNoValida = !_tarjeta.EsValida();
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}