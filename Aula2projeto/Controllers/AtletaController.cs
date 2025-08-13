using Aula2projeto.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Aula2projeto.Data;



namespace Aula2projeto.Controllers
{
    public class AtletaController : Controller
    {
        private readonly Database db = new Database();

        public IActionResult Criar()
        {
            ViewBag.Cidades = GetCidades(); // Para dropdown
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Atletas atleta)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"INSERT INTO tbAtletas (nomeAtleta, dataNascimento, sexo, altura, peso, codCidade)
                     VALUES (@nome, @data, @sexo, @altura, @peso, @cidade)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", atleta.nomeAtleta);
                cmd.Parameters.AddWithValue("@data", atleta.dataNascimento);
                cmd.Parameters.AddWithValue("@sexo", atleta.Sexo);
                cmd.Parameters.AddWithValue("@altura", atleta.Altura);
                cmd.Parameters.AddWithValue("@peso", atleta.Peso);
                cmd.Parameters.AddWithValue("@cidade", atleta.codCidade);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index","Edicao");
        }

        private List<Cidade> GetCidades()
        {
            List<Cidade> cidades = new List<Cidade>();
            using (var conn = db.GetConnection())
            {
                var sql = "SELECT Distinct * FROM tbCidades order by nomeCidade";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cidades.Add(new Cidade
                    {
                        codCidade = reader.GetInt32("codCidade"),
                        nomeCidade = reader.GetString("nomeCidade"),
                        codEstado = reader.GetInt32("codEstado")
                    });
                }
            }
            return cidades;
        }


    }
}
