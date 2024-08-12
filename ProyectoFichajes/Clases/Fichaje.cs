using AP17;
using MySql.Data.MySqlClient;
using ProyectoFichajes.Class;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProyectoFichajes
{
    class Fichaje
    {
        private int numFichaje;
        private string nifEmpleado;
        private DateTime dia;
        private TimeSpan horaEntrada;
        private TimeSpan horaSalida;
        private bool fichajeEntrada;
        private bool fichajeSalida;
        private bool fichajeCompletado;
        private TimeSpan duracionFichaje;

        public string NifEmpleado { get { return nifEmpleado; } set { nifEmpleado = value; } }
        public DateTime Dia { get { return dia; } set { dia = value; } }
        public TimeSpan HoraEntrada { get { return horaEntrada; } set { horaEntrada = value; } }
        public TimeSpan HoraSalida { get { return horaSalida; } set { horaSalida = value; } }
        public bool FichajeEntrada { get { return fichajeEntrada; } set { fichajeEntrada = value; } }
        public bool FichajeSalida { get { return fichajeSalida; } set { fichajeSalida = value; } }
        public bool FichajeCompletado { get { return fichajeCompletado; } set { fichajeCompletado = value; } }
        public Fichaje() { }

        public Fichaje(DateTime di, TimeSpan horaEnt, TimeSpan horaSal, TimeSpan durFichaje)
        {
            dia = di;
            horaEntrada = horaEnt;
            horaSalida = horaSal;
            duracionFichaje = durFichaje;
        }
        public Fichaje(string nifEmp, DateTime di, TimeSpan horaEnt, TimeSpan horaSal, bool fichajeEnt,
            bool fichajeSal, bool fichajeCom)
        {
            nifEmpleado = nifEmp;
            dia = di;
            horaEntrada = horaEnt;
            horaSalida = horaSal;
            fichajeEntrada = fichajeEnt;
            fichajeSalida = fichajeSal;
            fichajeCompletado = fichajeCom;
        }
        public Fichaje(int numFich, string nifEmp, DateTime di, TimeSpan horaEnt, TimeSpan horaSal,
            bool fichajeEnt, bool fichajeSal)
        {
            numFichaje = numFich;
            nifEmpleado = nifEmp;
            dia = di;
            horaEntrada = horaEnt;
            horaSalida = horaSal;
            fichajeEntrada = fichajeEnt;
            fichajeSalida = fichajeSal;
        }

        /// <summary>
        /// Registra un nuevo fichaje en la base de datos.
        /// </summary>
        /// <param name="fichaje">Objeto Fichaje a registrar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int RegistrarFichaje(Fichaje fichaje)
        {
            int resultado;
            int entrada = fichaje.fichajeEntrada ? 1 : 0;
            int salida = fichaje.fichajeSalida ? 1 : 0;
            int completado = fichaje.fichajeCompletado ? 1 : 0;

            string consulta = String.Format("INSERT INTO fichajes (nifEmpleado,dia,horaEntrada,fichajeEntrada,fichajeSalida,fichajeCompletado) " +
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", fichaje.nifEmpleado, fichaje.dia.ToString("yyyy-MM-dd"), fichaje.horaEntrada, entrada, salida, completado);

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                resultado = comando.ExecuteNonQuery();
            }
            return resultado;
        }

        /// <summary>
        /// Verifica si existe un fichaje pendiente para un empleado específico.
        /// </summary>
        /// <param name="fichaje">Objeto Fichaje a verificar.</param>
        /// <returns>True si existe un fichaje pendiente, false en caso contrario.</returns>
        public static bool ExisteFichaje(Fichaje fichaje)
        {
            string consulta = String.Format("SELECT * FROM fichajes WHERE nifEmpleado LIKE ('{0}') AND fichajeCompletado = 0", fichaje.NifEmpleado);
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        /// <summary>
        /// Registra la hora de salida de un fichaje en la base de datos.
        /// </summary>
        /// <param name="fichaje">Objeto Fichaje que contiene la información de salida.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int RegistrarSalida(Fichaje fichaje)
        {
            int retorno;
            int salida = fichaje.fichajeSalida ? 1 : 0;
            int completado = fichaje.fichajeCompletado ? 1 : 0;
            string consulta = String.Format("UPDATE fichajes SET dia='{0}',horaSalida='{1}',fichajeSalida='{2}',fichajeCompletado='{3}' " +
                "WHERE numFichaje = '{4}'", fichaje.dia.ToString("yyyy-MM-dd"), fichaje.horaSalida, salida, completado, EncontrarID(fichaje.nifEmpleado));

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                retorno = comando.ExecuteNonQuery();
            }
            return retorno;
        }

        /// <summary>
        /// Encuentra el ID del fichaje más reciente para un empleado específico.
        /// </summary>
        /// <param name="nif">NIF del empleado.</param>
        /// <returns>El ID del fichaje más reciente.</returns>
        public int EncontrarID(string nif)
        {
            List<int> lista = new List<int>();
            int valor = 0;

            string consulta = $"SELECT numFichaje FROM fichajes WHERE nifEmpleado='{nif}'";
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        valor = reader.GetInt16("numFichaje");
                        lista.Add(valor);
                    }
                }
            }
            int ultimoValor = lista.Count - 1;
            return lista[ultimoValor];
        }

        /// <summary>
        /// Rellena un DataTable con los datos obtenidos de la base de datos según la consulta especificada.
        /// </summary>
        /// <param name="consulta">Consulta SQL para obtener los datos.</param>
        /// <returns>Una DataTable cargada en memoria con los datos obtenidos.</returns>
        public static DataTable RellenarDatos(string consulta)
        {
            DataTable tablaDatos = new DataTable();

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(comando))
                {
                    adapter.Fill(tablaDatos);
                }
            }
            return tablaDatos;
        }

        /// <summary>
        /// Calcula la duración de los fichajes completados de un empleado específico.
        /// </summary>
        /// <param name="nif">NIF del empleado.</param>
        /// <returns>Una lista de objetos Fichaje con la duración calculada para cada fichaje.</returns>
        public static List<Fichaje> CalcularDuracionFichaje(string nif)
        {
            List<Fichaje> lista = new List<Fichaje>();

            string consulta = $"SELECT numFichaje, horaEntrada, horaSalida FROM fichajes WHERE nifEmpleado='{nif}' AND fichajeCompletado=1";
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Fichaje fichaje = new Fichaje();
                        fichaje.numFichaje = reader.GetInt16("numFichaje");
                        fichaje.horaEntrada = reader.GetTimeSpan("horaEntrada");
                        fichaje.horaSalida = reader.GetTimeSpan("horaSalida");
                        fichaje.duracionFichaje = fichaje.horaSalida - fichaje.horaEntrada;
                        lista.Add(fichaje);
                    }
                }
            }
            // Actualiza la duración de cada Fichaje en la lista
            foreach (Fichaje ficha in lista)
            {
                ActualizarDuracionFichaje(ficha);
            }
            return lista;
        }

        /// <summary>
        /// Actualiza la duración de un fichaje en la base de datos.
        /// </summary>
        /// <param name="fichaje">Objeto Fichaje con la duración actualizada.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        private static int ActualizarDuracionFichaje(Fichaje fichaje)
        {
            string consulta = String.Format("UPDATE fichajes SET duracionFichaje='{0}' WHERE numFichaje='{1}'", fichaje.duracionFichaje, fichaje.numFichaje);

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                return comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Calcula la duración total de los fichajes completados de un empleado en un rango de fechas específico.
        /// </summary>
        /// <param name="nif">NIF del empleado.</param>
        /// <param name="desde">Fecha de inicio del rango en formato americano.</param>
        /// <param name="hasta">Fecha de fin del rango en formato americano.</param>
        /// <returns>La suma total de las duraciones de los fichajes en el rango de fechas especificado.</returns>
        public static TimeSpan CalcularDuracionTotal(string nif, string desde, string hasta)
        {
            TimeSpan horasTotales = TimeSpan.Zero;
            string consulta = $"SELECT duracionFichaje FROM fichajes WHERE nifEmpleado='{nif}' AND fichajeCompletado=1 AND dia BETWEEN '{desde}' AND '{hasta}'";
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        horasTotales += reader.GetTimeSpan("duracionFichaje");
                    }
                }
            }
            return horasTotales;
        }

        /// <summary>
        /// Actualiza el indicador de salida para un empleado si no existe en la base de datos.
        /// </summary>
        /// <param name="nif">NIF del empleado.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public static int ActualizarSalida(string nif)
        {
            string consulta = "";
            if (!Empleado.ExisteEmpleado(nif))
            {
                consulta = String.Format("UPDATE fichajes SET fichajeSalida = true WHERE nifEmpleado LIKE '{0}';", nif);
            }

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                return comando.ExecuteNonQuery();
            }
        }
    }
}