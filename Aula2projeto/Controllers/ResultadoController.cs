using Microsoft.AspNetCore.Mvc;
using Aula2projeto.Models;
using MySql.Data.MySqlClient;
using Aula2projeto.Data;
namespace Aula2projeto.Controllers
{
    public class ResultadoController : Controller
    {
        private readonly Database db = new Database();

        public IActionResult CriarResultado()
        {
            ViewBag.Provas = GetProva();
            ViewBag.Atleta = GetAtletas();
            ViewBag.Edicao = GetEdicao();
            return View();
        }
        [HttpPost]

        public IActionResult CriarResultado(Resultado resultado)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"Insert into tbResultadosatletas (codAtleta,codProva,codEdicao,resultado,medalha)
                       values (@codatleta, @codprova, @codEdicao, @resultado, @medalha)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@codatleta", resultado.codAtleta);
                cmd.Parameters.AddWithValue("@codProva", resultado.codProva);
                cmd.Parameters.AddWithValue("@codEdicao",resultado.codEdicao);
                cmd.Parameters.AddWithValue("@resultado", resultado.resultado);
                cmd.Parameters.AddWithValue("@medalha", resultado.medalha);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index","Edicao");
        }
    }
}
