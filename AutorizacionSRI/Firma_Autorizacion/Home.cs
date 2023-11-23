using System;

namespace Firma_Autorizacion
{
    class Home
    {
        public static void Main(string[] args)
        {

         
            String rutaxml = args[0];
            String rutafirma = args[1];
            String rutadevu = args[2];
            String rutaauto = args[3];
            String clavefirma = args[4];
            String rutanoauto = args[5];

            Principal principal = new Principal();
            String respuesta = principal.TestPrueba(rutafirma, rutaxml, rutadevu, rutaauto, rutanoauto, null, clavefirma);

            if (respuesta == "OK")
            {
                Console.Write(respuesta);  
            }
            else
            {
                throw new Exception(respuesta);
            }
        }
    }
}
