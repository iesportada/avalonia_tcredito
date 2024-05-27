using System;

namespace MVVM_TarjetaCredito.Models
{
    class Tarjeta: ITarjeta
    {
        private string _numero;
        public string Numero {
            set => _numero = value;
        }
        public string Titular { get; set; }
        public string FechaValidez { get; }
        
        public Tarjeta(string num, string tit)
        {
            _numero = num;
            Titular = tit;
            DateTime fecha = DateTime.Now.AddYears(4);
            FechaValidez =  $"{fecha.Month.ToString()} / {fecha:yy}";
        }
        public bool EsValida()
        {
            // Este número es válido: 4857 6961 7919 2589
            // 1.Tomemos un número de tarjeta de crédito del cual queramos verificar su validez
            // 2.Separemos los números de las posiciones impares: (x = posición impar)

            int suma = 0, digito;
            
            for (int i = 0; i < _numero.Length; i++)
            {
                if (!int.TryParse(_numero[i].ToString(), out digito))
                    return false;

                if (i % 2 == 0) // tratamiento de cifras impares que ocupan posiciones pares del string
                {
                    var aux = digito * 2;
                    suma += (aux > 9) ?  aux - 9: aux;
                }
                else 
                    suma += digito;
            }
            // 5. Si el resultado anterior es múltiplo de 10 , entonces es válido.
            return suma % 10 == 0;
        }
    }
}
