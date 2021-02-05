using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_da_Fazenda_Vertente.ChildForms
{
    public partial class Gastos : Form
    {

        private Form atualChildForm;

        public Gastos()
        {
            InitializeComponent();
        }

        private void AbrirChildForm(Form childForm)
        {
            //open only form
            if (atualChildForm != null)
            {
                atualChildForm.Close();
            }
            atualChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PainelPrincipal2.Controls.Add(childForm);
            PainelPrincipal2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            AbrirChildForm(new Gados());
        }

        private void xuiButton2_Click(object sender, EventArgs e)
        {
            AbrirChildForm(new Porcos());
        }

        private void xuiButton5_Click(object sender, EventArgs e)
        {
            AbrirChildForm(new Remedios());
        }

        private void xuiButton3_Click(object sender, EventArgs e)
        {
            AbrirChildForm(new Ferramentas());
        }

        private void xuiButton4_Click(object sender, EventArgs e)
        {
            AbrirChildForm(new Construcoes());
        }
    }
}
