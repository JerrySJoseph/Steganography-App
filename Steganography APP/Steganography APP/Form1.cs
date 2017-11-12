using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Steganography;
using System.Threading;
using SystemInfo;

namespace Steganography_APP
{
    public partial class Form1 : Form
    {
        #region Properties
        public static string _inpcommmand;
        public static string _originalimagepath,_encodedimagepath;
        public static Image encodedimage;
        #endregion
        public Form1()
        {
            InitializeComponent();
            tabPage3.Text = "Encrypt Data";
            PasswordGroupBox.Enabled = false;
            SystemGroupBox.Enabled = false;
            textBox1.Enabled =textBox2.Enabled= false;
            textBox3.Enabled = false;
            button4.Enabled= button2.Enabled = button5.Enabled=button6.Enabled= false;
            button3.Enabled = false;
            pictureBox1.Image = pictureBox2.Image= pictureBox3.Image = Properties.Resources.image1;
            checkBox1.Enabled = checkBox2.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
           
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Hide Data";
            tabPage2.Text = "Extract Data";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dialog.Filter = "Bitmap Images(*.bmp)|*.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Help.Text = "Add Data to Hide in TextBox given above";
                textBox1.Enabled = true;
                textBox1.Text = "";
                button3.Enabled = true;
                textBox3.Text=_encodedimagepath = dialog.FileName;
                MessageBox.Show(_encodedimagepath);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                HideData encoder = new HideData();
                encodedimage = encoder.embedText(textBox1.Text, encoder.ConvertToBitmap(_originalimagepath));
                
                encodedimage.Save(_encodedimagepath, System.Drawing.Imaging.ImageFormat.Bmp);
                MessageBox.Show("The encrypted image has been saved","Image saved",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (_originalimagepath==null)
                MessageBox.Show("Choose File to Extract Data", "File Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            else
            {
                try
                {
                    HideData hd = new HideData();
                    Bitmap _bmp = new Bitmap(_originalimagepath);
                    textBox2.Text = hd.extractText(_bmp);
                }
                catch (Exception a)
                {
                   DialogResult d= MessageBox.Show(a.Message, "Error decrypting file", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (d==DialogResult.OK)
                    {
                        if (a.Message == "This is an Password Encrypted Message.\nPress OK button to Enter the Password or Cancel to go back.")
                        {
                            int attempts = 0;
                            while (attempts<3)
                            {
                                
                                try
                                {

                                    Passwordform frm = new Passwordform();
                                    frm.ShowDialog();
                                    HideData hd = new HideData();
                                    Bitmap _bmp = new Bitmap(_originalimagepath);
                                    textBox2.Text = hd.extractTextwithPassPhrase(_bmp, frm._password);
                                    break;

                                }
                                catch (Exception q)
                                {
                                    attempts++;
                                    DialogResult w= MessageBox.Show(q.Message+"Note: After Invalid 3 Attempts, the data will be automatically Erased.\n Attempts Left:"+(3-attempts), "Invalid Passphrase", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                                    if (w != DialogResult.Retry) break;                                   
                                } 
                            }
                            if(attempts>=3)
                            {
                                MessageBox.Show("You have reached maximum number of attempts.\nExiting Application..", "Invalid Attempts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Thread.Sleep(1000);
                                Application.Exit();
                            }



                        }
                        else if(a.Message== "This is an Single System Encrypted Message.\nIf you believe this message has been created for your system, try decoding this image from 'Encrypt Message' tab.")
                        {
                            try
                            {
                                SystemInfo.SystemID SID = new SystemID();
                                HideData hd = new HideData();
                                Bitmap _bmp = new Bitmap(_originalimagepath);
                                textBox2.Text = hd.extractText(_bmp,SID.GetSystemID());
                            }
                            catch(Exception w)
                            {
                                MessageBox.Show(w.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }

                        }
                    }
                   
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // if (textBox1.Enabled = true && textBox1.Text != "")
               // Help.Text = "Type in the data to hide ";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.LControlKey)
            {
                MessageBox.Show("CommandLine Opened");
            }
        }

        private void cmdtextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cmdhistorybox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
               
                    Help.Text = "Type in the Data to hide. Then press Save As to Save File.";
                    checkBox1.Checked = false;
                    SystemGroupBox.Enabled = true;
                
            }
            else
                SystemGroupBox.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                Help.Text = "Type in the Password and Data to hide. Then press Save As to Save File.";
                checkBox2.Checked = false;
                PasswordGroupBox.Enabled = true;
            }
            else
                PasswordGroupBox.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                Steganography.HideData hd = new HideData();
                Bitmap bmp = new Bitmap(_originalimagepath);
                encodedimage = hd.embedTextwithPassphrase(textBox5.Text, bmp, textBox4.Text);               
                encodedimage.Save(_encodedimagepath, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] arr = ImageToByte(encodedimage);
                using (Image image = Image.FromStream(new MemoryStream(arr)))
                {
                    image.Save(_encodedimagepath,System.Drawing.Imaging.ImageFormat.Png);  // Or Png
                }
                MessageBox.Show("Encrypted Image has been saved to"+_encodedimagepath,"Image Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception q )
            {
                
                MessageBox.Show("This File is Encrypted.\nYou cannot Overwrite an encrypted file.", "Error Overwriting File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Bitmap Image (*.bmp)|*.bmp";
          
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Help.Text = "Press Proceed button after typing in the data to hide.";
                button5.Enabled =button6.Enabled=true;
                _encodedimagepath = saveFileDialog1.FileName;
               

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            SystemInfo.SystemID SID = new SystemID();
            string _systemID = SID.GetSystemID();
            try
            {
                Steganography.HideData hd = new HideData();
                Bitmap bmp = new Bitmap(_originalimagepath);
                encodedimage = hd.embedText(textBox6.Text, bmp,_systemID);
                encodedimage.Save(_encodedimagepath, System.Drawing.Imaging.ImageFormat.Bmp);
                MessageBox.Show("Image Saved Succesfully to\n"+_encodedimagepath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception a)
            {
                MessageBox.Show("This File is Encrypted.\nYou cannot Overwrite an encrypted file.", "Error Overwriting File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Browse File";
            openFileDialog1.InitialDirectory = @"C:\Users\User\Pictures";
            openFileDialog1.Filter = "Jpeg Images(*.jpeg)|*.jpeg|PNG Images (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                _originalimagepath = openFileDialog1.FileName;
                Help.Text = "Press Save as button from Save Dialog to create a new encrypted image";
                button4.Enabled =button2.Enabled= true;
                textBox2.Enabled = true;
                textBox2.Text = "";
                checkBox1.Enabled = checkBox2.Enabled = true;         
                var size =new FileInfo(openFileDialog1.FileName).Length;
                label2.Text=label17.Text= label9.Text = openFileDialog1.SafeFileName;
                label8.Text= label18.Text = label10.Text = openFileDialog1.FileName;
                label6.Text = label24.Text= label16.Text = Path.GetExtension(openFileDialog1.FileName);
                label4.Text = label20.Text= label12.Text = (size/1024).ToString()+"KB";
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox3.ImageLocation =pictureBox2.ImageLocation= openFileDialog1.FileName;
            }
        }
    }
}
