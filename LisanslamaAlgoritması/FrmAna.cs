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
    public partial class FrmAna : Form
    {
        public FrmAna()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        bool TimerAktif = true;
        private void FrmAna_Load(object sender, EventArgs e)
        {

            backgroundWorker1.RunWorkerAsync();

            if (!new LiDataCtrl().IfDateFileNull(Application.StartupPath + "\\LiDaP.dll"))
            {
                MessageBox.Show("Sistem Dosyası eksiktir.Lütfen programı tekrar kurunuz ya da yazılım sahibine başvurunuz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
            
            try
            {
                if (new LiBackCount().LiDateDifference())
                {
                    TimerAktif = false;
                    Hide();
                    new FrmLisans().ShowDialog();
                }
            }
            catch
            {
                TimerAktif = false;
                Hide(); new FrmLisans().ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new LiDateFile().PathRead()); 

        }

        private void FrmAna_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void FrmAna_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void LisansCtrl_Tick(object sender, EventArgs e)
        {


            if (!TimerAktif) return;
            try
            {
                if (new LiBackCount().LiDateDifference())
                {

                    LisansCtrl.Stop();
                    Hide();
                    new FrmLisans().ShowDialog();
                }
            }
            catch { }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            new LiDateCtrl().NetSaati();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int DateDifference =Convert.ToInt16((new LiBackCount().LiDateMinutes() - new LiBackCount().BackCountRemaining())/60/24);
            MessageBox.Show(DateDifference.ToString());
        }
    }
}
