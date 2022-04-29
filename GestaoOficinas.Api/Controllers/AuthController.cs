using GestaoOficinas.Api.Data;
using GestaoOficinas.Api.Models;
using GestaoOficinas.Api.Services;
using GestaoOficinas.Api.ValueObjects;
using Microsoft.AspNetCore.Mvc;


namespace GestaoOficinas.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly GestaoOficinasApiContext _context;

        public AuthController(GestaoOficinasApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginObject model)
        {
            var encryptedPwd = TokenService.CreateMD5(model.Password);
            var user = _context.Oficina
                .Where(o => (o.CNPJ == model.CNPJ && o.Password == encryptedPwd)).ToList()[0];

            if(user == null)
                return NotFound(new {Message="Usuario ou senha inválidos"});

            var token = TokenService.GenerateToken(user);

            user.Password = "";
            
            var objReturn = new
            {
                user = user.Name,
                token = token
            };

            return new OkObjectResult(objReturn);
            

        }

    }
}
