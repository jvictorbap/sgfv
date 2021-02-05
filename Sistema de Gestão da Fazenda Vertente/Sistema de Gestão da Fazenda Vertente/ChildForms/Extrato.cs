using System;
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
    public partial class Extrato : Form
    {
        public static int entradas, correcao;

        public static double total;

        public static double total1;

        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;

        string strSQL;

        public Extrato()
        {
            InitializeComponent();
            Saidas();
            Entradas();
            Total();
        }

        private void Extrato_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "SELECT * FROM TB_Entradas";

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

        private void Saidas()
        {
            conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

            int gado, porco, ferramentas, remedio, construcoes, funcionarios;
            
            correcao = 6;

            SqlDataAdapter sda = new SqlDataAdapter("select sum(ValorGasto) from TB_GastosGado", conexao);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gado = Convert.ToInt32(dt.Rows[0][0].ToString());
            monoFlatLabel1.Text = dt.Rows[0][0].ToString() + " Reais";

            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ValorGasto) from TB_GastosFerramentas", conexao);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            ferramentas = Convert.ToInt32(dt1.Rows[0][0].ToString());
            monoFlatLabel2.Text = dt1.Rows[0][0].ToString() + " Reais";

            SqlDataAdapter sda2 = new SqlDataAdapter("select sum(ValorGasto) from TB_GastosPorco", conexao);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            porco = Convert.ToInt32(dt2.Rows[0][0].ToString());
            monoFlatLabel3.Text = dt2.Rows[0][0].ToString() + " Reais"; 

            SqlDataAdapter sda3 = new SqlDataAdapter("select sum(ValorGasto) from TB_GastosRemedio", conexao);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            remedio = Convert.ToInt32(dt3.Rows[0][0].ToString());
            monoFlatLabel4.Text = dt3.Rows[0][0].ToString() + " Reais";

            SqlDataAdapter sda4 = new SqlDataAdapter("select sum(ValorGasto) from TB_GastosConstrucoes", conexao);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            construcoes = Convert.ToInt32(dt4.Rows[0][0].ToString());
            monoFlatLabel6.Text = dt4.Rows[0][0].ToString() + " Reais";

            SqlDataAdapter sda5 = new SqlDataAdapter("select sum(ValorPago) from TB_Funcionarios", conexao);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            funcionarios = Convert.ToInt32(dt5.Rows[0][0].ToString());
            monoFlatLabel8.Text = dt5.Rows[0][0].ToString() + " Reais"; 

            total1 = remedio + gado + ferramentas + funcionarios + construcoes + porco - correcao;

            metroTextBox1.Text = total1 + " Reais";

            conexao.Close();
        }

        public void Entradas()
        {
            conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

            
            correcao = 1;

            SqlDataAdapter sda = new SqlDataAdapter("select sum(ValorEntrada) from TB_Entradas", conexao);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            entradas = Convert.ToInt32(dt.Rows[0][0].ToString());
            total = entradas - correcao;
            monoFlatLabel7.Text = total + " Reais";
            conexao.Close();
        }

        public void Total()
        {
            double total3;

            total3 = total - total1;

            metroTextBox2.Text = Convert.ToString(total1) + " Reais";

            metroTextBox3.Text = Convert.ToString(total) + " Reais";

            metroTextBox4.Text = Convert.ToString(total3) + " Reais";
        }
    }
}
