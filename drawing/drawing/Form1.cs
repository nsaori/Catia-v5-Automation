//201611111 saori
//drawing

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

namespace drawing
{
    public partial class Form1 : Form
    {
        INFITF.Application catia;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
                catia.Visible = true;
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DRAFTINGITF.DrawingDocument DrwDoc = null;
            DRAFTINGITF.DrawingView DrwView = null;

            try
            {
                DrwDoc = (DRAFTINGITF.DrawingDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("please open a DrawingDocument");
                return;
            }

            DRAFTINGITF.DrawingRoot DrwRoot = DrwDoc.DrawingRoot;

            try
            {
                DrwView = DrwRoot.ActiveSheet.Views.Item(DrwRoot.ActiveSheet.Views.Count);
            }
            catch (Exception)
            {
                MessageBox.Show("sheet가 없습니다.");
                return;
            }
            DRAFTINGITF.DrawingViewGenerativeBehavior DrwGenBeh = DrwView.GenerativeBehavior;

            double X1, Y1, Z1, X2, Y2, Z2;
            DrwGenBeh.GetProjectionPlane(out X1, out Y1, out Z1, out X2, out Y2, out Z2);

            ProductStructureTypeLib.Product Prd = (ProductStructureTypeLib.Product)DrwGenBeh.Document;
            MECMOD.PartDocument PrtDoc = (MECMOD.PartDocument)Prd.ReferenceProduct.Parent;
            MECMOD.Part Prt = PrtDoc.Part;

            MECMOD.Body Bdy = Prt.MainBody;
            MECMOD.Shapes Shps = Bdy.Shapes;

            PARTITF.Hole tHole = (PARTITF.Hole)Shps.GetItem("Hole.1");
            object[] opt = new object[3];
            tHole.GetOrigin(opt);
            //radiuse
            //tHole.

            //3D 위치 값
            // double X = -37.935, Y = 96.8, Z = 104.207;
            double X = (double)opt[0];
            double Y = (double)opt[1];
            double Z = (double)opt[2];

            //결과 값
            double COS_ALPHA = 0, VW_H = 0, VW_V = 0;

            //계산과정
            COS_ALPHA = (X * X1 + Y * Y1 + Z * Z1) / ((Math.Pow(X1, 2) + Math.Pow(Y1, 2) + Math.Pow(Z1, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_H = Math.Sqrt(X * X + Y * Y + Z * Z) * COS_ALPHA;

            COS_ALPHA = (X * X2 + Y * Y2 + Z * Z2) / ((Math.Pow(X2, 2) + Math.Pow(Y2, 2) + Math.Pow(Z2, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_V = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)) * COS_ALPHA;

            //create the point
            MECMOD.Factory2D fac = DrwView.Factory2D;
            fac.CreatePoint(VW_H, VW_V);

            //add a text (20정도 뛰어보자)

            DRAFTINGITF.DrawingText txt = DrwView.Texts.Add("Hole( " + (int)X +" , " +(int)Y + " , " + (int)Z +" )", VW_H + 20, VW_V - 20);
           // DRAFTINGITF.DrawingText txt = DrwView.Texts.Add(("( {0} , {1} , {2} )"X,Y,Z), VW_H + 20, VW_V - 20);
            txt.SetFontSize(0, 0, 12);

            DRAFTINGITF.DrawingLeader FDleadr = txt.Leaders.Add(VW_H, VW_V);

            //update the drawing document.
            DrwDoc.Update();
        }
    }
}
