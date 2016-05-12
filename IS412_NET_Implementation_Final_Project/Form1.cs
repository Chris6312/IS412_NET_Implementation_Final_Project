using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS412_NET_Implementation_Final_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {      
            Environment.Exit(-1); // closes the application
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm2 = new Form2(); // assign variable frm2 to Form2
            frm2.Show(); // open Form2
            this.Hide(); // hide Form1
        }
    }
}
