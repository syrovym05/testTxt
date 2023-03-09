using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textnatxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("windows-1250"));
                
                StreamWriter sw = new StreamWriter("best.txt", false, Encoding.GetEncoding("windows-1250"));

                string s;
                string best = "0";
                int maxmzda = 0;
                int pocetzen = 0;
                int soucet = 0;
                int pocetlidi = 0;
                string[] slova;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();  
                    while(s.Contains("\""))             //odstraneni uvozovek
                    {
                        s = s.Replace("\"","");
                    }                   
                    listBox1.Items.Add(s);

                    slova = s.Split(',');                    
                    s = Zaznam(slova);                

                    int mzda = Convert.ToInt32(slova[4]);
                    int vek = Convert.ToInt32(slova[3]);
                    soucet += vek;
                    pocetlidi++;

                    if (mzda > maxmzda)
                    {
                        maxmzda = mzda;                        
                        best = s;
                    }

                    if(mzda < 17300)
                    {
                        listBox2.Items.Add(s);
                    }

                    if(slova[2]=="Female") pocetzen++;
                    
                }
                
                sw.WriteLine(best);                
                sw.WriteLine("Prumerný věk: "+ soucet/pocetlidi);
                sw.Close();

                MessageBox.Show("Pocet zen: " + pocetzen, "Informace", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        int CZK(double i)
        {
            return Convert.ToInt32(Math.Round(i*22.35));            
        }

        string Zaznam(string[] pole)
        {            
            pole[4] = CZK(Convert.ToDouble(pole[4])).ToString();
            string s = "";
            for (int i = 0; i < pole.Length; i++)
            {               
                s += pole[i];
                if (i < pole.Length - 1) s += ",";
            }

            return s;
        }
    }
}
