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

using Excel = Microsoft.Office.Interop.Excel;
//using NPOI.SS.UserModel;

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
                catia.Visible = true;
            }
            /////Product.Products.count
            ProductDocument pdctDoc = (ProductDocument)catia.Documents.Add("Pruduct");
            

            //excel ---------------------------
            INFITF.Application excel;
            try
            {
                excel = (INFITF.Application)Marshal.GetActiveObject("EXCEL.Application");
            }
            catch (Exception)
            {
                excel = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("EXCEL.Application"));
                excel.Visible = true;
            }
            Excel.Workbook wb = excel.;
        }
    }
}
