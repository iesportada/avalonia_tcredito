namespace MVVM_TarjetaCredito.Models;

public interface ITarjeta
{
    public string Numero { set; }
    public string Titular { get; set; }
    public string FechaValidez { get; }
    public bool EsValida();
}