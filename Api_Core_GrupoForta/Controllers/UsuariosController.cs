using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Core_GrupoForta.Data;
using Api_Core_GrupoForta.Models.UsuariosModel;
using Api_Core_GrupoForta.Security;
using Api_Core_GrupoForta.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api_Core_GrupoForta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosRepository _repository;

        public UsuariosController(UsuariosRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Stock/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stock
        [HttpPost]
        [Route("[action]")]
        public async Task<UsuarioMin> EstablecerLogin(ReqLogin request)
        {
            var usuarioMin = await _repository.EstablecerLogin(request);

            usuarioMin.Usr_Token = usuarioMin.Usr_UsuarioID > 0 ?  TokenGenerator.GenerarJSONWebToken(Funciones.GetSHA256(usuarioMin.Usr_Nombre + usuarioMin.Usr_Password + DateTime.Now.Ticks.ToString())) : null;

            return usuarioMin;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> EnvioUsuario(ReqCorreo request)
        {
            return await _repository.EnviarCorreo(request);
        }

        // PUT: api/Stock/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
