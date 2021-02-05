using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Sistema_de_Gestão_da_Fazenda_Vertente.ChildForms
{
    public partial class Animais : Form
    {

        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;

        string strSQL;

        public static string caminho = System.Environment.CurrentDirectory;
        string origemCompleto = "";
        string foto = "";
        string pastaDestino = caminho + @"\fotos\";
        string destinoCompleto = "";

        public Animais()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                origemCompleto = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                destinoCompleto = pastaDestino + foto;
            }
           /* if (File.Exists(destinoCompleto))
            {
                if (MessageBox.Show("Arquivo Ja Existe, Deseja Substituir??", "Substituir", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    MessageBox.Show("Arquivo não copiado");
                    return;
                }
                System.IO.File.Copy(origemCompleto, destinoCompleto, true);
                if (File.Exists(destinoCompleto))
                {
                    pictureBox1.ImageLocation = origemCompleto;
                } */
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            try
            {

                if (destinoCompleto == "")
                {
                    MessageBox.Show("Sem Foto Selecionada");
                        return;
                }

                if (destinoCompleto != "")
                {
                    System.IO.File.Copy(origemCompleto, destinoCompleto, true);
                    if (File.Exists(destinoCompleto))
                    {
                        pictureBox1.ImageLocation = destinoCompleto;
                    }
                    else
                    {
                        MessageBox.Show("Foto Nao Encontrada");
                        return;
                    }
                }

                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "INSERT INTO TB_Animais (RACAANIMAL, NUMEROBRINCO, PESOESTIMADO, LOCALFOTO) VALUES (@RACAANIMAL, @NUMEROBRINCO, @PESOESTIMADO, @LOCALFOTO)";

                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", numero.Text);
                comando.Parameters.AddWithValue("@NUMEROBRINCO", numerobrinco.Text);
                comando.Parameters.AddWithValue("@RACAANIMAL", raca.Text);
                comando.Parameters.AddWithValue("@PESOESTIMADO", peso.Text);
                comando.Parameters.AddWithValue("@LOCALFOTO", destinoCompleto);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_Animais";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

                dvg.Columns["ID"].Visible = false;
                dvg.Columns["LOCALFOTO"].Visible = false;

                pictureBox1.ImageLocation = "";


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

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "DELETE FROM TB_Animais WHERE NUMEROBRINCO = @NUMEROBRINCO";

                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@NUMEROBRINCO", numerobrinco.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_Animais";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

                dvg.Columns["ID"].Visible = false;
                dvg.Columns["LOCALFOTO"].Visible = false;


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

                strSQL = "UPDATE TB_Animais SET RACAANIMAL = @RACAANIMAL, NUMEROBRINCO = @NUMEROBRINCO, PESOESTIMADO = @PESOESTIMADO WHERE ID = @ID";
                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", numero.Text);
                comando.Parameters.AddWithValue("@NUMEROBRINCO", numerobrinco.Text);
                comando.Parameters.AddWithValue("@RACAANIMAL", raca.Text);
                comando.Parameters.AddWithValue("@PESOESTIMADO", peso.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                strSQL = "SELECT * FROM TB_Animais";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

                dvg.Columns["ID"].Visible = false;
                dvg.Columns["LOCALFOTO"].Visible = false;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvg.SelectedRows.Count > 0)
            {
                numero.Text = dvg.CurrentRow.Cells["ID"].Value.ToString();
                raca.Text = dvg.CurrentRow.Cells["RACAANIMAL"].Value.ToString();
                numerobrinco.Text = dvg.CurrentRow.Cells["NUMEROBRINCO"].Value.ToString();
                peso.Text = dvg.CurrentRow.Cells["PESOESTIMADO"].Value.ToString();
                pictureBox1.ImageLocation = dvg.CurrentRow.Cells["LOCALFOTO"].Value.ToString();
            }
        }

        private void Animais_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(@"Server=DESKTOP-BI7MMRS\SQLEXPRESS;Database=DB_Gestao;User Id=sa;Password=123456;");

                strSQL = "SELECT * FROM TB_Animais";

                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSQL, conexao);

                conexao.Open();

                da.Fill(ds);

                dvg.DataSource = ds.Tables[0];

                dvg.Columns["ID"].Visible = false;
                dvg.Columns["LOCALFOTO"].Visible = false;



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
    }
}
