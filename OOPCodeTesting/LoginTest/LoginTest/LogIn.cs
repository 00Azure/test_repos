using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testLogIn
{
    public partial class LogIn : Form
    {

        public LogIn()
        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register(this);
            register.Show();
            this.Hide();
        }

        /*private void LogIn_Load(object Form)
        {
            if (this.panelLogin.Controls.Count > 0)
                this.panelLogin.Controls.RemoveAt(0);
            Form test = Form as Form;
            test.TopLevel = false;
            test.Dock = DockStyle.Fill;
            this.panelLogin.Controls.Add(test);
            this.panelLogin.Tag = test;
            test.Show();
        }*/
    }
}
