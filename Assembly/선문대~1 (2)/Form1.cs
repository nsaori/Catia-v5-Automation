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
using PARTITF;

namespace WindowsFormsApplication45
{


    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;
        int Level = 0;

        public Form1()
        {
            InitializeComponent();
        }

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

            INFITF.Document tdoc = null;
            if (Level != 0)
            {
                tdoc = Catia.Documents.Open(FullPath);
            }
            INFITF.Viewer Vw = Catia.ActiveWindow.ActiveViewer;
            object[] oldColor = new object[3];
            Vw.GetBackgroundColor(oldColor);

            object[] newColor = { 1, 1, 1 };
            Vw.PutBackgroundColor(newColor);

            Vw.CaptureToFile(INFITF.CatCaptureFormat.catCaptureFormatJPEG, FullPath + ".jpg");

            Vw.PutBackgroundColor(oldColor);

            if (Level != 0)
            {
                tdoc.Close();
            }

            textBox1.Text += Level.ToString() + "." + PrtNum + "." + FullPath + "." + Vol.ToString()+"," +FullPath+".jpg"+"\r\n";
            
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



   
