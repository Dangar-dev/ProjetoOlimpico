using Aula2projeto.Data;
using Aula2projeto.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Olimpiada.Controllers
{
    public class CidadeController : Controller
    {
        private readonly Database db = new Database();

        public IActionResult Criar()
        {
            ViewBag.Estado = GetEstado();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Cidade cidade)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"INSERT INTO tbCidade(nomeCidade, codEstado) 

                     VALUES (@nome, @estado)";

                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", cidade.nomeCidade);

                cmd.Parameters.AddWithValue("@estado", cidade.codCidade);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Edicao");
        }

        private List<Estado> GetEstado()
        {
            List<Estado> estado = new List<Estado>();
            using (var conn = db.GetConnection())

            {

                var sql = "SELECT Distinct * FROM tbEstados order by NomeEstado";

                var cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    estado.Add(new Estado   
                    {


                        codEstado = reader.GetInt32("codEstado"),

                        nomeEstado = reader.GetString("NomeEstado"),
                    });

                }

            }
            return estado;
        }
    }
}