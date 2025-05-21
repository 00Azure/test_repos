using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testMainpanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
            btn3.Click += Btn3_Click;
            btnLogout.Click += BtnLogout_Click;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new test3());
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new test2());
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new test1 ());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*if (this.mainpanel.Controls.Count > 0) 
                this.mainpanel.Controls.RemoveAt(0);
              Form test = Form as Form;
              test.TopLevel = false;
              test.Dock = DockStyle.Fill;
              this.mainpanel.Controls.Add(test);
              this.mainpanel.Tag = test;
              test.Show();*/
            /*// Ensure the mainpanel is cleared of previous controls
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);

            // Cast the passed object to a Form
            Form loadedForm = Form as Form;

            if (loadedForm != null)
            {
                // Customize the form before embedding
                loadedForm.TopLevel = false;       // Makes it a child form
                loadedForm.Dock = DockStyle.Fill; // Ensures it fills the mainpanel
                this.mainpanel.Controls.Add(loadedForm); // Adds the form to the panel
                this.mainpanel.Tag = loadedForm;        // Sets the panel's tag property
                loadedForm.Show();                      // Display the form
            }
            else
            {
                MessageBox.Show("The object provided is not a form.");
            }*/
            LoadFormIntoPanel(new test1());
        }
        private void LoadFormIntoPanel(Form form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);

            if (form != null)
            {
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                this.mainpanel.Controls.Add(form);
                this.mainpanel.Tag = form;
                form.Show();
            }
            else
            {
                MessageBox.Show("The object provided is not a valid form.");
            }
        }

    }
}
