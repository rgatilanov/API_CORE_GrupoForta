using Api_Core_GrupoForta.Models.UsuariosModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Core_GrupoForta.Data
{
    public class UsuariosRepository
    {
        private readonly string _ConectionString;
        public UsuariosRepository(IConfiguration configuration)
        {
            _ConectionString = configuration.GetConnectionString("defultConnection");
        }
        public async Task<UsuarioMin> EstablecerLogin(ReqLogin stockRequest)
        {
            UsuarioMin response = new UsuarioMin();
            try
            {
                using (SqlConnection sql = new SqlConnection(_ConectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerTiposSolicitudes", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response = new UsuarioMin
                                {
                                    Usr_Nombre = "Pueba",
                                    Usr_Password = "pass",
                                    Usr_UsuarioID = 1
                                };
                            }
                        }

                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return response;
        }
    }
}
