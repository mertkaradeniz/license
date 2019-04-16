using Lisans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LisanslamaAlgoritması
{
    public partial class FrmLisans : Form
    {
        public FrmLisans()
        {   
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        bool BtnLisansCtrl(string Key,string Parametre)
        {
            try
            {
                if (new ClsLiCtrl().LiKeyCompare(Key, Parametre))//Gelen Anahtar
                {
                    System.Threading.Thread.Sleep(500);
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/ImageL/ok.png");
                    button17.Enabled = true;
                    return true;
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/ImageL/no.png");
                    button17.Enabled = false;
                    return false;
                }
            }
            catch {
                System.Threading.Thread.Sleep(1500);
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/ImageL/no.png");
                button17.Enabled = false;
                return false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new FrmKeyCreate().Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;

            if (textBox1.Text.Count() >= 29)
            {
                BtnLisansCtrl(textBox1.Text, label2.Text);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(label2.Text);
        }

        private void FrmAna_Load(object sender, EventArgs e)
        {

            if (!new LiDataCtrl().IfDateFileNull(Application.StartupPath + "\\LiDaP.dll"))
            {
                MessageBox.Show("Sistem Dosyası eksiktir.Lütfen programı tekrar kurunuz ya da yazılım sahibine başvurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            try
            {
                if (!new LiBackCount().LiDateDifference())
                {
                    Hide();
                    new FrmAna().ShowDialog();

                }
            }
            catch { }

            label2.Text = new LiDataCtrl().LISHOWPARAMETRE().ToString();
           
            try
            {
                if (new LiBackCount().LiDemoFinishR())
                {
                    radioButton2.Visible = false;
                }
            }
            catch { }
                
            

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (BtnLisansCtrl(textBox1.Text, label2.Text))
                {
                    new LiDateFile().PathWriteAdd(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    Hide(); new FrmAna().ShowDialog();
                }
                else
                {
                    MessageBox.Show("false");
                }
              
            }

            if (radioButton2.Checked)
            {
                new ClsLiCtrl().LiDEMO();
                new LiDateFile().PathWriteAdd(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                Hide(); new FrmAna().ShowDialog();
              
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            { button17.Enabled = true; textBox1.Enabled = false; label2.Enabled = false; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          Hide();  new FrmAna().Show(); 
        }

        private void FrmLisans_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            { button17.Enabled = false; textBox1.Enabled = true; label2.Enabled = true; }
        }
    }
}
