using AP17;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProyectoFichajes.Class
{
    class Empleado
    {
        private string nif;
        private string nombre;
        private string apellidos;
        private bool administrador;
        private string clave;

        public string Nif { get { return nif; } set { nif = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellidos { get { return apellidos; } set { apellidos = value; } }
        public bool Administrador { get { return administrador; } set { administrador = value; } }
        public string Clave { get { return clave; } set { clave = value; } }

        public Empleado() { }

        public Empleado(string NIF, string nom, string apell, bool admin, string contr)
        {
            nif = NIF;
            nombre = nom;
            apellidos = apell;
            administrador = admin;
            clave = contr;
        }
        public Empleado(string nom, string apell)
        {
            nombre = nom;
            apellidos = apell;
        }

        /// <summary>
        /// Comprueba si existe un empleado en la base de datos por su NIF.
        /// </summary>
        /// <param name="nif">NIF del empleado a comprobar.</param>
        /// <returns>True si el empleado existe, false sino existe.</returns>
        public static bool ExisteEmpleado(string nif)
        {
            string consulta = String.Format("SELECT * FROM empleados WHERE nif LIKE ('{0}')", nif);
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        /// <summary>
        /// Comprueba si un empleado por su NIF es administrador.
        /// </summary>
        /// <param name="nif">NIF del empleado a comprobar.</param>
        /// <returns>True si es adminstrador, false sino es administrador.</returns>
        public static bool EsAdministrador(string nif)
        {
            bool esAdmin = false;
            string consulta = String.Format("SELECT administrador FROM empleados WHERE nif LIKE ('{0}')", nif);
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        esAdmin = reader.GetBoolean("administrador");
                    }
                }
            }
            return esAdmin;
        }

        /// <summary>
        /// Comprueba si la clave proporcionada corresponde al empleado identificado por su NIF.
        /// </summary>
        /// <param name="nif">NIF del empleado.</param>
        /// <param name="clave">La clave a comprobar.</param>
        /// <returns>True si la clave es correcta, false en caso contrario.</returns>
        public static bool ComprobarClave(string nif, string clave)
        {
            bool comprobar = false;
            string consulta = String.Format("SELECT clave FROM empleados WHERE nif LIKE ('{0}')", nif);
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.GetString("clave") == clave)
                        {
                            comprobar = true;
                        }
                    }
                }
            }
            return comprobar;
        }

        /// <summary>
        /// Busca empleados en la base de datos según la consulta proporcionada.
        /// </summary>
        /// <param name="consulta">La consulta SQL para buscar empleados.</param>
        /// <returns>Una lista de objetos Empleado que coinciden con la consulta.</returns>
        public static List<Empleado> BuscarEmpleados(string consulta)
        {
            List<Empleado> lista = new List<Empleado>();

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Empleado empleado = new Empleado(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3),
                                    reader.GetString(4));
                                lista.Add(empleado);
                            }
                            catch (ArgumentException) { }
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Agrega un nuevo empleado a la base de datos.
        /// </summary>
        /// <param name="emp">El empleado a agregar.</param>
        /// <returns>El número de filas afectadas por la inserción.</returns>
        public int AgregarEmpleado(Empleado emp)
        {
            int resultado;
            int sqlBool = emp.administrador ? 1 : 0;
            string consulta = String.Format("INSERT INTO empleados VALUES ('{0}','{1}','{2}','{3}','{4}')", emp.nif, emp.nombre, emp.apellidos, sqlBool, emp.clave);

            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                resultado = comando.ExecuteNonQuery();
            }
            return resultado;
        }

        /// <summary>
        /// Elimina un empleado de la base de datos dado su NIF.
        /// </summary>
        /// <param name="nif">El NIF del empleado a eliminar.</param>
        /// <returns>El número de filas afectadas por la eliminación.</returns>
        public static int EliminaUsuario(string nif)
        {
            int resultado = 0;

            string consulta = String.Format("DELETE FROM empleados WHERE nif LIKE ('{0}')", nif);
            using (MySqlCommand comando = new MySqlCommand(consulta, ConBD.Conexion))
            {
                resultado = comando.ExecuteNonQuery();
            }
            return resultado;
        }

    }
}