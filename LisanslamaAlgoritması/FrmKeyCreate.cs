using Lisans;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LisanslamaAlgoritması
{
    public partial class FrmKeyCreate : Form
    {
        public FrmKeyCreate()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new LiDemoFile().PathWriteAdd("Deneme karadeniz");
        }

        private void button2_Click(object sender, EventArgs e)
        {
          MessageBox.Show(new LiDemoFile().PathRead()); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new LiDatePath().PathKey());
        }

        private void FrmLisansPaneli_Load(object sender, EventArgs e)
        {
            label3.Text = new LiDataCtrl().LISHOWPARAMETRE().ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new LiDateFile().PathRead()); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new LiDateFile().PathWriteAdd("Bismillahirrahmanirrahim");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new LiDemoFile().PathWrite("");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new LiDateFile().PathWrite("");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          label3.Text= new LiDataCtrl().LISHOWPARAMETRE().ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {

           maskedTextBox2. Text=new LiDataCtrl().LIPARAMETRESERINUMBER().ToString();
            //new LiDataCtrl().LIPARAMETRESUBMIT().ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            maskedTextBox3.Text = new LiDataCtrl().LIPARAMETREDATEREPLACEKEY().ToString();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = new LiDataCtrl().LIKEY();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label3.Text = new LiDataCtrl().LISHOWPARAMETRE().ToString();
            maskedTextBox2.Text = new LiDataCtrl().LIPARAMETRESERINUMBER().ToString();
            maskedTextBox3.Text = new LiDataCtrl().LIPARAMETREDATEREPLACEKEY().ToString();
            maskedTextBox1.Text = new LiDataCtrl().LIKEY();
            label7.Text ="Count: "+ new LiDataCtrl().LIKEY().Count().ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new LiDataCtrl().KEYSPLIT();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new LiBackCount().LiDateDifference().ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new ClsLiCtrl().LiDemoCtrl().ToString());

        }

        private void button16_Click(object sender, EventArgs e)
        {
            new ClsLiCtrl().LiDemoStarts();
        }

        private void button17_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(new ClsLiCtrl().LiKeyCompare(maskedTextBox4.Text).ToString());
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            new LiDataCtrl().InputParametre(maskedTextBox5.Text);
        
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
         richTextBox1.Text= new LiDatePath().Encrypt(textBox1.Text);
        }
    }
}
