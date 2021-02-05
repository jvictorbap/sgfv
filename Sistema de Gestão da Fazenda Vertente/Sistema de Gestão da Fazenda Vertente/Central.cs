using FontAwesome.Sharp;
using Sistema_de_Gestão_da_Fazenda_Vertente.ChildForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_da_Fazenda_Vertente
{
    public partial class Central : Form
    {

        private IconButton iconebotao;
        private Panel borda;
        private Form atualChildForm;

        public Central()
        {
            InitializeComponent();
            borda = new Panel();
            borda.Size = new Size(7, 100);
            MenuLateral.Controls.Add(borda);
          //  pictureBox2.BackColor = Color.Transparent;

        }

        private void Central_Load(object sender, EventArgs e)
        {

        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
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
            PainelPrincipal.Controls.Add(childForm);
            PainelPrincipal.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                iconebotao = (IconButton)senderBtn;
                iconebotao.ForeColor = color;
                iconebotao.ImageAlign = ContentAlignment.MiddleRight;
                iconebotao.IconColor = color;
                iconebotao.TextImageRelation = TextImageRelation.TextBeforeImage;
                iconebotao.TextAlign = ContentAlignment.MiddleCenter;

                borda.BackColor = color;
                borda.Location = new Point(0, iconebotao.Location.Y);
                borda.Visible = true;
                borda.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (iconebotao != null)
            {
                iconebotao.BackColor = Color.Black;
                iconebotao.ForeColor = Color.White;
                iconebotao.TextAlign = ContentAlignment.MiddleLeft;
                iconebotao.IconColor = Color.White;
                iconebotao.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconebotao.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            AbrirChildForm(new Extrato());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            AbrirChildForm(new Gastos());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            AbrirChildForm(new Entradas());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            AbrirChildForm(new Funcionarios());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            AbrirChildForm(new Animais());
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
