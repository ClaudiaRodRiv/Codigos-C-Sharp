using System.Configuration;

/*

BitacoraC#.cs
Este script se utiliza para registrar eventos y errores en una bitácora mientras se ejecuta una interfaz. 
La bitácora ayuda a llevar un control de las operaciones realizadas y a visualizar cualquier error que ocurra durante la ejecución de la interfaz.

Uso:
1. Al iniciar el programa, se obtiene la ruta de la bitácora desde el archivo .config y se genera un archivo de texto (.txt) con la fecha actual.
2. El programa registra el inicio del programa, cualquier operación realizada, y los errores capturados durante la ejecución.
3. Al finalizar la ejecución, se registra el final del programa en la bitácora.

Asegúrate de tener configurada la ruta de la bitácora en el archivo .config de tu proyecto. 
Puedes adaptar esta bitácora para su uso en interfaces más avanzadas para llevar un control detallado de su funcionamiento.

*/

namespace BitacoraC_
{
  
    public partial class FormBitacora : Form
    {
    
        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            // Obtener la ruta de la bitácora desde el archivo .config
            string rutaBitacora = ConfigurationManager.AppSettings["BitacoraPath"];
            // Generar el nombre del archivo con la fecha actual
            string nombreArchivo = $"Bitacora {DateTime.Now:yyyyMMdd}.txt";
            string rutaCompleta = Path.Combine(rutaBitacora, nombreArchivo);

            // Registrar un mensaje en la bitácora
            RegistrarEnBitacora(rutaCompleta, "Inicio del programa.", "Info");

            try
            {
                // Operación que generará un error (división por cero)
                int numerador = 10;
                int denominador = 0; // Esto causará una excepción
                int resultado = numerador / denominador;

                // Si, por alguna razón, la división no falla, registramos un éxito
                RegistrarEnBitacora(rutaCompleta, "Operación realizada con éxito", "Exito");

            }
            catch (Exception ex)
            {
                // Capturar la excepción y registrar el error en la bitácora
                RegistrarEnBitacora(rutaCompleta, $"Error: {ex.Message}", "Error");
            }
            finally
            {
                // Registrar el final del programa y cerrar la aplicación
                RegistrarEnBitacora(rutaCompleta, "Fin del programa. \n", "Info");
                Application.Exit(); // Cierra la aplicación
            }
        }

        private void RegistrarEnBitacora(string rutaArchivo, string mensaje, string tipo)
        {
            // Agregar el prefijo correspondiente según el tipo de mensaje
            string prefijo = tipo switch
            {
                "Info" => "----",
                "Exito" => "++++",
                "Error" => "XXXX",
                _ => ""
            };

            // Verificar si el archivo existe, si no, se crea
            using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
            {
                // Escribir el mensaje en la bitácora con la hora actual
                writer.WriteLine($"{prefijo} {DateTime.Now:HH:mm:ss} {mensaje}");
            }
        }

    }
    
}
