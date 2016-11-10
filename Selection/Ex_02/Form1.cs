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

namespace Ex_02
{
    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;        
        INFITF.Reference PtStart = null;
        INFITF.Reference PtEnd = null;

        public Form1()
        {
            InitializeComponent();

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
            INFITF.Selection Sel;
            Sel = Catia.ActiveDocument.Selection;

            object[] inputObjectType = { "Point" };
            string Status;

            Sel.Clear();
            Status = Sel.SelectElement2(inputObjectType, "Select an Start Point", false);

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택했나";
                return;
            }

            PtStart = Sel.Item(1).Reference;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            INFITF.Selection Sel;
            Sel = Catia.ActiveDocument.Selection;

            object[] inputObjectType = { "Point" };
            string Status;

            Sel.Clear();
            Status = Sel.SelectElement2(inputObjectType, "Select an End Point", false);

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택했나";
                return;
            }

            INFITF.SelectedElement SelItem;
            SelItem = Sel.Item(1);

            PtEnd = Sel.Item(1).Reference;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PtStart == null)
            {
                return;
            }

            if (PtEnd == null)
            {
                return;
            }

            MECMOD.PartDocument PrtDoc = (MECMOD.PartDocument)Catia.ActiveDocument;
            MECMOD.Part prt = PrtDoc.Part;

            MECMOD.HybridBodies HBs = prt.HybridBodies;

            MECMOD.HybridBody HyBody = HBs.Add();

            HybridShapeTypeLib.HybridShapeFactory HSFac = (HybridShapeTypeLib.HybridShapeFactory)prt.HybridShapeFactory;
            HybridShapeTypeLib.HybridShapeLinePtPt Lineptpt = null;
            Lineptpt = HSFac.AddNewLinePtPt((INFITF.Reference)PtStart, (INFITF.Reference)PtEnd);
            
            HyBody.AppendHybridShape(Lineptpt);
            prt.Update();
        }
    }
}
