using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Aula2projeto.Data;
using Aula2projeto.Models;
using System.Collections.Generic;


namespace Aula2projeto.Controllers
{
    public class EdicaoController : Controller
    {
        private readonly Database db = new Database();
        public IActionResult Index()
        {
            List<Edicao> edicoes = new List<Edicao>();
            using (MySqlConnection conn = db.GetConnection())
            {
                string sql = "SELECT * FROM tbEdicao";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        edicoes.Add(new Edicao
                        {
                            codEdicao = reader.GetInt32("codEdicao"),
                            Ano = reader.GetInt32("Ano"),
                            Sede = reader.GetString("Sede")
                        });


                    }
                }
            }
            return View(edicoes);
        }
    }
}
