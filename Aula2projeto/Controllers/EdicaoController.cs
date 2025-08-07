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
        public IActionResult Atletas(int id)
        {
            List<Atletas> atletas = new List<Atletas>();
            string nomeEdicao = "";
            int totalAtletas = 0;

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = @"
                  SELECT DISTINCT 
                           a.codAtleta, 
                           a.nomeAtleta, 
                           a.dataNascimento, 
                           a.sexo, 
                           a.codCidade,
                           m.codModalidade, 
                           m.nomeModalidade
                       FROM tbresultadosatletas  r
                       JOIN tbProvas p ON p.codProva = r.codProva
                       JOIN tbAtletas a ON a.codAtleta = r.codAtleta
                       LEFT JOIN tbModalidades m ON m.codModalidade = p.codModalidade
                       WHERE r.CodEdicao = @id;
                    ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        atletas.Add(new Atletas
                        {
                            codAtleta = reader.GetInt32(reader.GetOrdinal("codAtleta")),

                            nomeAtleta = reader.IsDBNull(reader.GetOrdinal("nomeAtleta")) ? null : reader.GetString(reader.GetOrdinal("nomeAtleta")),

                            dataNascimento = reader.IsDBNull(reader.GetOrdinal("dataNascimento")) ? null
                                : reader.GetString(reader.GetOrdinal("dataNascimento")),

                            Sexo = reader.IsDBNull(reader.GetOrdinal("Sexo"))
                                ? '\0'  // valor padrão para char
                                : reader.GetChar(reader.GetOrdinal("Sexo")),

                            codCidade = reader.IsDBNull(reader.GetOrdinal("codCidade"))
                                ? 0  // ou (int?)null se for Nullable<int>
                                : reader.GetInt32(reader.GetOrdinal("codCidade")),

                            codModalidade = reader.IsDBNull(reader.GetOrdinal("codModalidade"))
                                ? 0  // ou (int?)null se sua propriedade for Nullable
                                : reader.GetInt32(reader.GetOrdinal("codModalidade")),

                            Modalidade = reader.IsDBNull(reader.GetOrdinal("nomeModalidade"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("nomeModalidade"))
                        });
                    }

                }

                totalAtletas = atletas.Count;
            }

            ViewBag.EdicaoId = id;
            ViewBag.TotalAtletas = totalAtletas;
            return View(atletas);
        }

    }
}
