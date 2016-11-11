using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INFITF;

namespace WindowsFormsApplication45
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        INFITF.Application Catia = null;
        int Level = 0;




        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch
            {
                Catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                Catia.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductStructureTypeLib.ProductDocument PrdDoc;
            PrdDoc = (ProductStructureTypeLib.ProductDocument)Catia.ActiveDocument;
            GetSubPrd(PrdDoc.Product);
        }

        private void GetSubPrd(ProductStructureTypeLib.Product Prd)
        {
            string PrtNum = Prd.get_PartNumber();
            INFITF.Document Doc = (INFITF.Document)Prd.ReferenceProduct.Parent;
            string FullPath = Doc.FullName;

            SPATypeLib.SPAWorkbench SPAWB;
            SPAWB = (SPATypeLib.SPAWorkbench)Doc.GetWorkbench("SPAWorkbench");
            SPATypeLib.Measurable Mea = SPAWB.GetMeasurable((INFITF.Reference)Prd);
            double Vol;
            try
            {
                Vol = Mea.Volume;
            }
            catch
            {
                Vol = 0;
            }


            textBox1.Text +=Level.ToString()+ "."+PrtNum + "." + FullPath + "\r\n";


            for (int i = 1; i <= Prd.Products.Count; i++)
            {
                ProductStructureTypeLib.Product tPrd = Prd.Products.Item(i);
                Level++;
                GetSubPrd(tPrd);
                Level--;
               
            }

        }

        



    }
}
   
