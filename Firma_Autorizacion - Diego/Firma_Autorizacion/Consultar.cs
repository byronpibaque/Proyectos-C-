using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma_Autorizacion
{
   public class Consultar
    {
        public Autorizacion.respuestaComprobante ConsultarComprobante(String clave)
        {
            Autorizacion.respuestaComprobante response = null;
            try
            {
                using (Autorizacion.AutorizacionComprobantesOfflineClient consulta = new Autorizacion.AutorizacionComprobantesOfflineClient())
                {
                    response = consulta.autorizacionComprobante(clave);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
