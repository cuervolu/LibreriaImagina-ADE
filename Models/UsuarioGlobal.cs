namespace SistemaLibreriaImagina.Models
{
    public class UsuarioGlobal
    {
        private static UsuarioGlobal instancia;
        public USUARIO Usuario { get; set; }

        private UsuarioGlobal()
        {
            // Evita que se cree una instancia directamente
        }

        public static UsuarioGlobal Instancia
        {
            get
            {
                // Si la instancia no existe, crear una nueva
                if (instancia == null)
                {
                    instancia = new UsuarioGlobal();
                }
                return instancia;
            }
        }
    }

}
