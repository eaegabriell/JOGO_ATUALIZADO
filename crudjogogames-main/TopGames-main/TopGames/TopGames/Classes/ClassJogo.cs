﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopGames.Classes
{
    class ClassJogo
    {
        private object id;
        private bool checkbox;

        public int Id { get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public string editora { get; set; }
        public string valor { get; set; }
        public string quantidade { get; set; }
        public DateTime data_cadastro { get; set; }


        public List<ClassJogo> listajogo()
        {
            List<ClassJogo> li = new List<ClassJogo>();
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM jogo";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ClassJogo j = new ClassJogo();
                j.Id = (int)dr["Id"];
                j.nome = dr["nome"].ToString();
                j.categoria = dr["categoria"].ToString();
                j.editora = dr["editora"].ToString();
                j.valor = dr["valor"].ToString();
                j.quantidade = dr["quantidade"].ToString();
                j.data_cadastro = Convert.ToDateTime(dr["data_cadastro"]);
                li.Add(j);
            }
            return li;
        }

        public async Task<bool> Inserir(string nome, string categoria, string editora, string valor, string quantidade, DateTime data_cadastro)
            {
                SqlConnection con = DBContext.ObterConexao();
                SqlCommand cmd = con.CreateCommand();
                var busca = await BuscarPorId(id);
            if (busca)
                {
                    cmd.CommandText = "INSERT INTO jogo(nome,categoria,editora,valor,quantidade,data_cadastro) VALUES ('" + nome + "','" + categoria + "','" + editora + "','" + valor + "','" + quantidade + "','" + data_cadastro + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DBContext.FecharConexao();
                    return true;
                }
                else
                {
                    DBContext.FecharConexao();
                    return false;
                }
        }

        private object BuscarPorId(object id)
        {
            throw new NotImplementedException();
        }

        public void Procurar(string id)
        {
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM jogo WHERE id='" + id + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                categoria = dr["categoria"].ToString();
                editora = dr["editora"].ToString();
                valor = dr["valor"].ToString();
                quantidade = dr["quantidade"].ToString();
                data_cadastro = Convert.ToDateTime(dr["data_cadastro"]);
            }
        }

        public bool BuscarPorId(string id)
        {
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM jogo WHERE id='" + id + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Já existe um jogo cadastrado com esse ID", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Exclui(string id)
        {
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM jogo WHERE id='" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            DBContext.FecharConexao();
        }

        public void Atualizar(string nome, string categoria, string editora, string valor, string quantidade, DateTime data_cadastro)
        {
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE jogo SET nome='" + nome + "',categoria='" + categoria + "',editora'" + editora + "',valor='" + valor + "',quantidade='" + quantidade + "'data_cadastro='" + data_cadastro + "' WHERE id = '" + id + "'";
            cmd.CommandType = CommandType.Text; 
            cmd.ExecuteNonQuery();
            DBContext.FecharConexao();
        }

        public void AtualizarAdmin(string nome, string categoria, string editora, string valor, string quantidade, DateTime data_cadastro)
        {
            var processo = ""; 
            if (checkbox)    
            {
                processo = "admin"; 
            }
            else
            {                       
                processo = "user";  
            }
            SqlConnection con = DBContext.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE jogo SET nome='" + nome + "',categoria='" + categoria + "',editora'" + editora + "',valor='" + valor + "',quantidade='" + quantidade + "'data_cadastro='" + data_cadastro + "' WHERE id = '" +id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            DBContext.FecharConexao();
        }
    }
}

