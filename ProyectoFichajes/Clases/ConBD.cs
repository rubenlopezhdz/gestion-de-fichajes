using MySql.Data.MySqlClient;


namespace AP17
{
    public class ConBD
    {
        private static MySqlConnection instancia = null;

        private static readonly object padlock = new object();

        private ConBD() { }

        public static MySqlConnection Conexion
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new MySqlConnection();

                        string server = "server=127.0.0.1;";
                        string database = "database=bdfichajes;";
                        string usuario = "uid=root;";
                        string password = "pwd=;";
                        instancia.ConnectionString = server + database + usuario + password;
                    }

                    return instancia;
                }
            }
        }

        /// <summary>
        /// Método que abre la conexión a la base de datos si la instancia no es nula.
        /// </summary>
        public static void AbrirConexion()
        {
            if (instancia != null)
                instancia.Open();
        }

        /// <summary>
        /// Método que cierra la conexión a la base de datos si la instancia no es nula.
        /// </summary>
        public static void CerrarConexion()
        {
            if (instancia != null)
                instancia.Close();
        }


    }
}
