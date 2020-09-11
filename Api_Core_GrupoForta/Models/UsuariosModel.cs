using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Core_GrupoForta.Models.UsuariosModel
{
    public class ReqLogin
    {
        public string Usr_Identificacion { get; set; }
        public string Usr_Password { get; set; }
    }

    public class UsuarioMin
    {
        public short Usr_UsuarioID { get; set; }
        public string Usr_Nombre { get; set; }
        public string Usr_Password { get; set; }
        public string Usr_Token { get; set; }
    }

    public class Usuriario: UsuarioMin
    {
        public string Usr_Identificacion { get; set; }

        public string Usr_TipoUsuario { get; set; }

        public bool Usr_Mostrar { get; set; }

        public bool Usr_Elegir { get; set; }

        public DateTime Usr_FechaPassword { get; set; }

        public bool Usr_OperadorCaja { get; set; }

        public bool Usr_Activo { get; set; }

        public DateTime Usr_FechaInicioSesion { get; set; }

        public string Usr_CorreoUsuario { get; set; }

        public string Usr_CorreoPassword { get; set; }

        public string Usr_CorreoCuenta { get; set; }

        public string Usr_CorreoServidor { get; set; }

        public short Usr_CorreoPuerto { get; set; }

        public bool Usr_SSL { get; set; }

        public short Usr_AgenteID { get; set; }
    }
}
