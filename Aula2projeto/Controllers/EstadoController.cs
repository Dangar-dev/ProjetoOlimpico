using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Aula2projeto.Data;
using Aula2projeto.Data;
using Aula2projeto.Models;


namespace Aula2projeto.Controllers
{
    public class EstadoController : Controller
    {
        private readonly Database db = new Database();
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Criar(Estado estado)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"INSERT INTO tbEstados (NomEstado) VALUES (@nome)";

                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", estado.nomeEstado);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Edicao");
        }
    }
}
