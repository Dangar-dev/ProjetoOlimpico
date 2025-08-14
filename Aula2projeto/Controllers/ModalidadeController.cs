using Aula2projeto.Data;
using Aula2projeto.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Aula2projeto.Data;

namespace Aula2projeto.Controllers
{
    public class ModalidadeController : Controller
    {
        private readonly Database db = new Database();

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Modalidades modalidade)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"INSERT INTO tbModalidades (NomeModalidade) 

                     VALUES (@nome)";

                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", modalidade.nomeModalidade);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Edicao");
        }

    }
}