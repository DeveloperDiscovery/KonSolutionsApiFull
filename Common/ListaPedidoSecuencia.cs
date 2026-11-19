using System.Globalization;

namespace KonSolutionsApi.Common;

/// <summary>Lee la cadena de líneas seleccionadas que reciben
/// PROC_MOVIMIENTOS_ALMACEN_PEDIDOS_COMERCIALES_ASIGNA y _DESASIGNA:
///
///     P&lt;pedido&gt;S&lt;sec&gt;C&lt;cantidad&gt;S&lt;sec&gt;C&lt;cantidad&gt;_P&lt;pedido&gt;S&lt;sec&gt;C&lt;cantidad&gt;
///
/// Por ejemplo 'P10S1C25S2C10_P25S3C4.5' son tres líneas con sus cantidades.
///
/// ACÁ SÓLO SE LEE, NO SE ARMA. La cadena la construye la pantalla —el armador vive en
/// KONSolutions.Shared— y esta API no comparte proyecto con la web. En vez de duplicar el
/// armado, que sería tener dos formatos capaces de separarse sin que nada lo note, del lado
/// del servidor vive únicamente el lector.
///
/// Y el lector hace falta igual: la API no puede confiar en que lo que llega esté bien
/// formado. Del otro lado se parsea con TRY_CONVERT, así que una cadena rota NO da error:
/// devuelve una lista vacía, el SP recorre cero líneas, y la operación informa ÉXITO sin
/// haber hecho nada. Eso es lo que hay que atajar antes de llegar a la base.</summary>
public static class ListaPedidoSecuencia
{
    public static List<(int Pedido, int Secuencia, decimal Cantidad)> Leer(string? cadena)
    {
        var salida = new List<(int, int, decimal)>();
        if (string.IsNullOrWhiteSpace(cadena)) return salida;

        foreach (var bloque in cadena.Split('_', StringSplitOptions.RemoveEmptyEntries))
        {
            if (bloque.Length < 4 || bloque[0] != 'P') continue;

            var corte = bloque.IndexOf('S');
            if (corte < 2) continue;
            if (!int.TryParse(bloque[1..corte], NumberStyles.Integer, CultureInfo.InvariantCulture, out var pedido)) continue;

            foreach (var parte in bloque[corte..].Split('S', StringSplitOptions.RemoveEmptyEntries))
            {
                var sec = parte;
                decimal cantidad = 0;

                var c = parte.IndexOf('C');
                if (c >= 0)
                {
                    sec = parte[..c];
                    decimal.TryParse(parte[(c + 1)..], NumberStyles.Number, CultureInfo.InvariantCulture, out cantidad);
                }

                if (int.TryParse(sec, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n))
                    salida.Add((pedido, n, cantidad));
            }
        }
        return salida;
    }
}
