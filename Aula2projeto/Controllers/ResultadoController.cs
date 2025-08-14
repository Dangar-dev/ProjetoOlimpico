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

        public IActionResult CriarResultado(Resultado resultados)
        {
            using (var conn = db.GetConnection())
            {
                var sql = @"Insert into tbResultadosatletas (codAtleta, codProva, codEdicao, resultado, medalha)
                       values (@atleta, @prova, @edicao, @resultado, @medalha)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@atleta", resultados.codAtleta);
                cmd.Parameters.AddWithValue("@prova", resultados.codProva);
                cmd.Parameters.AddWithValue("@edicao", resultados.codEdicao);
                cmd.Parameters.AddWithValue("@resultado", resultados.resultado);
                cmd.Parameters.AddWithValue("@medalha", resultados.medalha);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("CriarResultado");
        }


        private List<Prova> GetProva()
        {
            List<Prova> provas = new List<Prova>();
            using (var conn = db.GetConnection())
            {
                var sql = "select  Distinct * from tbProvas order by nomeProva";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    provas.Add(new Prova
                    {
                        codProva = reader.GetInt32("codProva"),
                        nomeProva = reader.GetString("nomeProva"),
                        codModalidade = reader.GetInt32("codModalidade")
                    });
                }
            }
            return provas;
        }

        private List<Atletas> GetAtletas()
        {
            List<Atletas> atletas = new List<Atletas>();
            using (var conn = db.GetConnection())
            {
                var sql = "SELECT Distinct codAtleta, nomeAtleta, dataNascimento, Sexo, " +
                    "codCidade FROM tbAtletas order by nomeAtleta";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    atletas.Add(new Atletas
                    {
                        codAtleta = reader.GetInt32("codAtleta"),
                        nomeAtleta = reader.GetString("nomeAtleta"),
                        dataNascimento = reader.GetString("dataNascimento"),
                        Sexo = reader.GetChar("Sexo"),
                        codCidade = reader.GetInt32("codCidade")
                    });
                }
            }
            return atletas;
        }

        private List<Edicao> GetEdicao()
        {
            List<Edicao>  edicaos = new List<Edicao>();
            using (var conn = db.GetConnection())
            {
                var sql = "select Distinct * from tbEdicao order by Ano";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    edicaos.Add(new Edicao
                    {   
                        codEdicao = reader.GetInt32("codEdicao"),
                        Ano = reader.GetInt32("Ano"),
                        Sede = reader.GetString("Sede")
                    });
                }
            }   
            return edicaos;
        }
    }
}
        

