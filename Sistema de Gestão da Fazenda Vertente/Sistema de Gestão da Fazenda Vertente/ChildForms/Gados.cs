﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sistema_de_Gestão_da_Fazenda_Vertente.ChildForms
{
    public partial class Gados : Form
    {

        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;

        string strSQL;

        public Gados()
        {
            InitializeComponent();
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "INSERT INTO TB_GastosGado (NOMEGASTO, VALORGASTO, DATA) VALUES (@NOMEGASTO, @VALORGASTO, @DATA)";

                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@DATA", dateTimePicker1.Value.Date);
                comando.Parameters.AddWithValue("@NOMEGASTO", nome.Text);
                comando.Parameters.AddWithValue("@VALORGASTO", valor.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_GastosGado";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                comando.Clone();
                conexao = null;
                comando = null;
            }
        }

        private void Gados_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "SELECT * FROM TB_GastosGado";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                conexao.Open();

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "DELETE FROM TB_GastosGado WHERE ID = @ID";

                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", numero.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_GastosGado";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                comando.Clone();
                conexao = null;
                comando = null;
            }
        }

        private void xuiButton3_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "UPDATE TB_GastosGado SET NOMEGASTO = @NOMEGASTO, VALORGASTO = @VALORGASTO WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", numero.Text);
                comando.Parameters.AddWithValue("@NOMEGASTO", nome.Text);
                comando.Parameters.AddWithValue("@VALORGASTO", valor.Text);
                comando.Parameters.AddWithValue("@DATA", dateTimePicker1.Value.Date);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_GastosGado";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                comando.Clone();
                conexao = null;
                comando = null;
            }
        }

        private void dvg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvg.SelectedRows.Count > 0)
            {
                nome.Text = dvg.CurrentRow.Cells["NOMEGASTO"].Value.ToString();
                numero.Text = dvg.CurrentRow.Cells["ID"].Value.ToString();
                valor.Text = dvg.CurrentRow.Cells["VALORGASTO"].Value.ToString();
            }
        }
    }
}
