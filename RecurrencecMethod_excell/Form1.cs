//20131103 saori
//excel 를 읽어 products호출하기 
// 재기method 사용하기

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MECMOD;
using ProductStructureTypeLib;
using System.Runtime.InteropServices;

namespace RecurrencecMethod_excell
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
          
        private void button1_Click(object sender, EventArgs e)
        {
            //catia실행----------------------
            INFITF.Application catia;
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Applicationa"));
            }
            catia.Visible = true;

            ProductDocument pdctDoc = (ProductDocument)catia.Documents.Add("Pruduct");
            Product pdct = pdctDoc.Product;
            Products pdcts = pdct.Products;

            //excel ---------------------------
            INFITF.Application excel;
            excel = 

        }
    }
}
