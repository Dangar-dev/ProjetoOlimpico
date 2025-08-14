using Aula2projeto.Data;
using Aula2projeto.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Olimpiada.Controllers
{
    public class ProvaController : Controller
    {
        private readonly Database db = new Database();

        public IActionResult Criar()
        {
            ViewBag.Modalidade = GetModalidade();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Prova prova)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"INSERT INTO tbProvas(nomeProva, codModalidade) 

                     VALUES (@nome, @modalidade)";

                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", prova.nomeProva);

                cmd.Parameters.AddWithValue("@modalidade", prova.codModalidade);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Edicao");
        }

        private List<Modalidades> GetModalidade()
        {
            List<Modalidades> modalidades = new List<Modalidades>();
            using (var conn = db.GetConnection())

            {

                var sql = "SELECT Distinct * FROM tbModalidades order by NomeModalidade";

                var cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    modalidades.Add(new Modalidades
                    {

                        codModalidade = reader.GetInt32("codModalidade"),

                        nomeModalidade = reader.GetString("NomeModalidade"),

                    });

                }

            }

            return modalidades;
        }
    }
}
