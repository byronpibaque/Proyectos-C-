using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;




namespace AC_facturero
{
    class CrearTicket
    {
        StringBuilder linea = new StringBuilder();
        int maxCar = 35, cortar;
        public String lineasGuion()
        {
            String lineasguion = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasguion += "-";
            }
            return linea.AppendLine(lineasguion).ToString();
        }
        public String lineasAsteriscos()
        {
            String lineasasterisco = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasasterisco += "*";
            }
            return linea.AppendLine(lineasasterisco).ToString();
        }
        public String lineasIgual()
        {
            String lineasigual = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasigual += "=";
            }
            return linea.AppendLine(lineasigual).ToString();
        }
        public void EncabezadoArticulo()
        {
            linea.AppendLine("ARTICULO      |STOCK|FRACC|LAB     ");
        }
        public void textoIzquierda(String texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                linea.AppendLine(texto);
            }
        }
        public void textoDerecha(String texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                string espacios = "";
                for (int i = 0; i < (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                for (int i = 0; i < (maxCar - texto.Length); i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(espacios + texto);
            }
        }
        public void textoCentro(String texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                string espacios = "";
                int centrar = (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;

                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                int centrar = (maxCar - texto.Length) / 2;

                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(espacios + texto);
            }
        }
        public void textoExtremos(string textoIzquierdo, string textoDerecho)
        {
            string textoizq, textoder, textocompleto = "", espacios = "";
            if (textoIzquierdo.Length > 18)
            {
                cortar = textoIzquierdo.Length - 18;
                textoizq = textoIzquierdo.Remove(18, cortar);
            }
            else
            {
                textoizq = textoIzquierdo;
            }
            textocompleto = textoizq;
            if (textoDerecho.Length > 20)
            {
                cortar = textoDerecho.Length - 20;
                textoder = textoDerecho.Remove(20, cortar);
            }
            else
            {
                textoder = textoDerecho;
            }
            int nroEspacios = maxCar - (textoizq.Length + textoder.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textocompleto += espacios + textoDerecho;
            linea.AppendLine(textocompleto);
        }
        public void AgregarTotales(string texto, decimal total)
        {
            string resumen, valor, textocompleto, espacios = "";
            if (texto.Length > 25)
            {
                cortar = texto.Length - 25;
                resumen = texto.Remove(25, cortar);
            }
            else
            {
                resumen = texto;
            }
            textocompleto = resumen;
            valor = total.ToString("#,#.00");
            int nroEspacios = maxCar - (resumen.Length + valor.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textocompleto += espacios + valor;
            linea.AppendLine(textocompleto);
        }
        //Metodo para agreagar articulos al ticket de venta
        public void AgregaArticulo(string articulo, decimal cant, decimal fracciones,string lab)
        {
            //Valida que cant precio e importe esten dentro del rango.
            if (cant.ToString().Length <= 5 && fracciones.ToString().Length <= 5 && lab.ToString().Length <= 8)
            {
                string elemento = "", espacios = "";
                bool bandera = false;//Indicara si es la primera linea que se escribe cuando bajemos a la segunda si el nombre del articulo no entra en la primera linea
                int nroEspacios = 0;

                //Si el nombre o descripcion del articulo es mayor a 23, bajar a la siguiente linea
                if (articulo.Length > 14)
                {
                    //Colocar la cantidad a la derecha.
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Generamos los espacios necesarios para alinear a la derecha
                    }
                    elemento += espacios + cant.ToString();//agregamos la cantidad con los espacios

                    

                    //Colocar el importe a la derecha.
                    nroEspacios = (5 - fracciones.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + fracciones.ToString();//Agregamos el importe alineado a la derecha
                                                                 //Colocar el importe a la derecha.
                    nroEspacios = (8 - lab.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + lab.ToString();//Agregamos el importe alineado a la derecha

                    int caracterActual = 0;//Indicara en que caracter se quedo al bajae a la siguiente linea

                    //Por cada 23 caracteres se agregara una linea siguiente
                    for (int longitudTexto = articulo.Length; longitudTexto > 14; longitudTexto -= 14)
                    {
                        if (bandera == false)//si es false o la primera linea en recorrerer, continuar...
                        {
                            //agregamos los primeros 20 caracteres del nombre del articulos, mas lo que ya tiene la variable elemento
                            linea.AppendLine(articulo.Substring(caracterActual, 14) + elemento);
                            bandera = true;//cambiamos su valor a verdadero
                        }
                        else
                            linea.AppendLine(articulo.Substring(caracterActual, 14));//Solo agrega el nombre del articulo

                        caracterActual += 14;//incrementa en 20 el valor de la variable caracterActual
                    }
                    //Agrega el resto del fragmento del  nombre del articulo
                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));

                }
                else //Si no es mayor solo agregarlo, sin dar saltos de lineas
                {
                    for (int i = 0; i < (14 - articulo.Length); i++)
                    {
                        espacios += " "; //Agrega espacios para completar los 20 caracteres
                    }
                    elemento = articulo + espacios;

                    //Colocar la cantidad a la derecha.
                    nroEspacios = (5 - cant.ToString().Length);// +(20 - elemento.Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    

                    //Colocar el importe a la derecha.
                    nroEspacios = (5 - fracciones.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + fracciones.ToString();//Colocar el importe a la derecha.

                    nroEspacios = (8 - lab.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + lab.ToString();

                    linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                }
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }
        //Metodos para enviar secuencias de escape a la impresora
        //Para cortar el ticket
        public void CortaTicket()
        {
            linea.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comando varian segun el tipo de impresora
            linea.AppendLine("\x1B" + "d" + "\x09"); //Avanza 9 renglones, Tambien varian
        }
        //Para abrir el cajon
        public void AbreCajon()
        {
            //Estos tambien varian, tienen que ever el manual de la impresora para poner los correctos.
            linea.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96"); //Caracteres de apertura cajon 0
            //linea.AppendLine("\x1B" + "p" + "\x01" + "\x0F" + "\x96"); //Caracteres de apertura cajon 1
        }
        //Para mandara a imprimir el texto a la impresora que le indiquemos.
        public void ImprimirTicket(string impresora)
        {
            //Este metodo recibe el nombre de la impresora a la cual se mandara a imprimir y el texto que se imprimira.
            //Usaremos un código que nos proporciona Microsoft. https://support.microsoft.com/es-es/kb/322091

            RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString()); //Imprime texto.
            linea.Clear();//Al cabar de imprimir limpia la linea de todo el texto agregado.
        }
    }


    //Clase para mandara a imprimir texto plano a la impresora
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
            di.pDataType = "RAW";//de tipo texto plano

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    

    }
}
