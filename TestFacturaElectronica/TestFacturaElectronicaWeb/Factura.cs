using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;

namespace TestFacturaElectronica.WebService
{
    [Serializable]
    public class Factura
    {
        public int CantRegistros { get; set; }
        public int PuntoVenta { get; set; }
        public int TipoComprobante { get; set; }
        public List<Detalle> DetalleFactura { get; set; }

        /// <summary>
        /// Convierte una fecha con formato manejado por FECAEDetRequest a un System.DateTime
        /// </summary>
        /// <param name="fechaComprobante">String con la fecha, pero en el formato que maneja el FECAEDetRequest.</param>
        /// <returns>Fecha en formato System.DateTime</returns>
        public static DateTime ConvertirAFormatoFecha(string fechaComprobante)
        {
            try
            {
                string _dia, _mes, _anio;
                _anio = fechaComprobante.Substring(0, 4);
                _mes = fechaComprobante.Substring(4, 2);
                _dia = fechaComprobante.Substring(6, 2);
                DateTime _fechaADevolver = new DateTime(Convert.ToInt32(_anio), Convert.ToInt32(_mes), Convert.ToInt32(_dia));

                return _fechaADevolver;
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
        }
    }

    [Serializable]
    public class Detalle
    {
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public DateTime FechaComp { get; set; }
        public double ImporteTotal { get; set; }
        public double ImporteTotalConc { get; set; }
        public double ImporteNeto { get; set; }
        public double ImporteIVA { get; set; }
        public double ImporteOpExento { get; set; }
        public double ImporteTrib { get; set; }
        public DateTime FechaServDesde { get; set; }
        public DateTime FechaServHasta { get; set; }
        public DateTime FechaVtoPago { get; set; }
        public string MonedaId { get; set; }
        public double MonedaCotiz { get; set; }
        public List<CbteAsoc> CbtesAsoc { get; set; }
        public List<Tributo> Tributos { get; set; }
        public List<AlicIva> Iva { get; set; }
        public List<Opcional> Opcionales { get; set; }
    }
}
