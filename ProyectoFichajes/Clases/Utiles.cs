namespace ProyectoFichajes.Clases
{
    class Utiles
    {
        /// <summary>
        /// Calcula la letra de un nif en base a los dígitos de un nif.
        /// </summary>
        /// <param name="valor">Dígitos del nif</param>
        /// <returns>Letra calculada en base a los dígitos proporcionados</returns>
        private static char CalculaLetra(int valor)
        {
            const string tabla = "TRWAGMYFPDXBNJZSQVHLCKET";

            int indice = (int)valor % 23;
            return tabla[indice];
        }

        /// <summary>
        /// Elimina un guion entre los digitos del nif y la letra correspondiente
        /// </summary>
        /// <param name="valorNIF">Nif que pasamos con un guion procedente del control maskTextBox</param>
        /// <returns>Nif completo sin guion del maskTextBox </returns>
        public static string EliminarGuion(string valorNIF)
        {
            string aux = "";
            for (int i = 0; i < valorNIF.Length; i++)
            {
                if (valorNIF[i] != '-')
                {
                    aux += valorNIF[i].ToString();
                }
            }
            return aux;
        }

        /// <summary>
        /// Valida la letra del nif 
        /// </summary>
        /// <param name="valorNIF">Nif completo</param>
        /// <returns>True si la letra corresponde, false si la letra no corresponde.</returns>
        public static bool ValidarLetraNIF(string valorNIF)
        {
            valorNIF = valorNIF.ToUpper();
            char letra1 = valorNIF[valorNIF.Length - 1];
            string dni = valorNIF.Substring(0, valorNIF.Length - 1);
            char letra2 = CalculaLetra(int.Parse(dni));
            return (letra1 == letra2) ? true : false;
        }
    }
}
