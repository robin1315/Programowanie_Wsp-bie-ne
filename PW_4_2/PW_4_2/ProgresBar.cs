using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ProgresBar : Form
    {
        private int i = 1;
        private static int vSilnia =0;
        private static int tmp=0;
        private static Thread counter;


        private static int silnia(int i) { 
            if(i < 1 )
                return 1;
            else
                return i * silnia(i-1);
        }

        public ProgresBar()
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            

            InitializeComponent();
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            vSilnia = silnia(Convert.ToInt32(textBox1.Text));
            label1.Text = Convert.ToString(vSilnia);
            progressBar1.BackColor = Color.Aqua;
            progressBar1.ForeColor = Color.Aqua;
            progressBar1.Maximum = vSilnia;
            progressBar1.Minimum = 0;
            counter = new Thread(new ThreadStart(start));


            counter.Start();
        }
        public void start() {
            while (true) {
                MethodInvoker method = new MethodInvoker(() => progressBar1.Value = tmp);
                progressBar1.Invoke(method);
                tmp++;
                Thread.Sleep(10);
                if (vSilnia < tmp) {
                    counter.Abort();
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           counter.Suspend();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           i = 1;
           vSilnia =0;
           tmp=0;
           textBox1.Clear();
           if (counter.ThreadState == ThreadState.Suspended)
           {
               counter.Resume();
               counter.Abort();
           }
           else
               counter.Abort();

           label1.Text= "Wynik:";
           progressBar1.ResetText();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (counter.ThreadState == ThreadState.Suspended)
                    counter.Resume();
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            i = 1;
            vSilnia = 0;
            tmp = 0;
            textBox1.Clear();
            if (counter.ThreadState == ThreadState.Suspended)
            {
                counter.Resume();
                counter.Abort();
            }
            else
                counter.Abort();

            label1.Text = "Wynik:";
            progressBar1.ResetText();
            Form.ActiveForm.Close();
        }
    }
}
