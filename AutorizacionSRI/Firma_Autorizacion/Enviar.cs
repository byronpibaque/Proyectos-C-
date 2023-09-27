using FirmaXadesNet;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
namespace Firma_Autorizacion
{
    public class Enviar
    {

        private static X509Certificate2 GetCertificateByPath(string rutaFirma, string claveFirma)
        {

            var cert = new X509Certificate2(rutaFirma, claveFirma);

            return cert;

        }

        public Recepcion.respuestaSolicitud EnviarComprobante(String rutafirma, String xml, String clavefirma)
        {

            Recepcion.respuestaSolicitud response = null;
            try
            {
                var xmlDoc = new XmlDocument();
                var uidCert = GetCertificateByPath(rutafirma, clavefirma);
                FirmaXades firmaXades = new FirmaXades(SignMethod.RSAwithSHA1, DigestMethod.SHA1);
                xmlDoc.Load(xml);

                xmlDoc.PreserveWhitespace = true;
                firmaXades.SetContentEnveloped(xmlDoc);
                firmaXades.Sign(uidCert, SignMethod.RSAwithSHA1);

                using (Recepcion.RecepcionComprobantesOfflineClient envio = new Recepcion.RecepcionComprobantesOfflineClient())
                {

                    response = envio.validarComprobante(System.Text.Encoding.UTF8.GetBytes(firmaXades.Document.InnerXml));
                    return response;
                }

            }
            catch (Exception Ex)
            {
                return response;
            }
        }


    }
}
