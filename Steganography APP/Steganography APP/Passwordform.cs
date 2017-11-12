using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography_APP
{
    public partial class Passwordform : Form
    {
        public string _password;
       
        public Passwordform()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _password = textBox1.Text;
            this.Close();
         
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                button1_Click(sender,e);
            }
        }
    }
}
