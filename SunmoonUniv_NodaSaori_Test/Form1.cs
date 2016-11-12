//2161112 saori
//test

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
using DRAFTINGITF;
using HybridShapeTypeLib;

namespace SunmoonUniv_NodaSaori_Test
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        MECMOD.PartDocument prtDoc = null;
        DRAFTINGITF.DrawingDocument drwDoc = null;
        HybridShapeTypeLib.HybridShapePointCoord pt1 = null;
        HybridShapeTypeLib.HybridShapePointCoord pt2 = null;
        HybridShapeTypeLib.HybridShapePointCoord pt3 = null;


        public Form1()
        {
            InitializeComponent();

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
            //1. open the file
            if (textBox1.Text == "") {
                MessageBox.Show("파일 주소를 입력해주세요.");
                return;
            }
            prtDoc = (MECMOD.PartDocument)catia.Documents.Open(textBox1.Text);

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (catia == null)
            {
                MessageBox.Show("Please run CATIA");
                return;
            }

            try
            {
                prtDoc = (MECMOD.PartDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("please open a document.");
                return;
            }

            //2.Gs 생성
            MECMOD.HybridBody hbdy = prtDoc.Part.HybridBodies.Add();
            hbdy.set_Name("PointForDraeingGS");

            //3.add points
            HybridShapeTypeLib.HybridShapeFactory hfac = (HybridShapeTypeLib.HybridShapeFactory)prtDoc.Part.HybridShapeFactory;
            pt1 = hfac.AddNewPointCoord(0, 0, 0);
            pt2 = hfac.AddNewPointCoord(-250, 100, -300);
            pt3 = hfac.AddNewPointCoord(90, -250, 60);

            pt1.set_Name("PT-1");
            pt1.set_Name("PT-2");
            pt1.set_Name("PT-3");

            hbdy.AppendHybridShape(pt1);
            hbdy.AppendHybridShape(pt2);
            hbdy.AppendHybridShape(pt3);

            prtDoc.Part.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //5. open the file
            if (textBox1.Text == "")
            {
                MessageBox.Show("파일 주서를 입력해주세요.");
                return;
            }
            drwDoc = (DRAFTINGITF.DrawingDocument)catia.Documents.Open(textBox2.Text);

            //6. 도면 update
            drwDoc.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DRAFTINGITF.DrawingView DrwView = null;

            if (catia == null)
            {
                MessageBox.Show("Please run CATIA");
                return;
            }

            try
            {
                drwDoc = (DRAFTINGITF.DrawingDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("please open a document.");
                return;
            }

            DRAFTINGITF.DrawingRoot DrwRoot = drwDoc.DrawingRoot;

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

            //7.point txt, leader생성
            CreateTxt(DrwGenBeh, pt1, DrwView);
            CreateTxt(DrwGenBeh, pt2, DrwView);
            CreateTxt(DrwGenBeh, pt3, DrwView);

            /*
            double X1, Y1, Z1, X2, Y2, Z2;
            DrwGenBeh.GetProjectionPlane(out X1, out Y1, out Z1, out X2, out Y2, out Z2);

            ProductStructureTypeLib.Product Prd = (ProductStructureTypeLib.Product)DrwGenBeh.Document;
            MECMOD.PartDocument PrtDoc = (MECMOD.PartDocument)Prd.ReferenceProduct.Parent;
            MECMOD.Part Prt = PrtDoc.Part;

            MECMOD.Body Bdy = Prt.MainBody;
            MECMOD.Shapes Shps = Bdy.Shapes;

            //7.point txt, leader생성
            CreateTxt(DrwGenBeh,pt1, DrwView);
            object[] ap1 = new object[3];
            object[] ap2 = new object[3];
            object[] ap3 = new object[3];

            pt1.GetCoordinates(ap1);
            pt2.GetCoordinates(ap2);
            pt3.GetCoordinates(ap3);

            double X = (double)ap1[0];
            double Y = (double)ap1[1];
            double Z = (double)ap1[2];

            double COS_ALPHA = 0, VW_H = 0, VW_V = 0;

            //계산과정
            COS_ALPHA = (X * X1 + Y * Y1 + Z * Z1) / ((Math.Pow(X1, 2) + Math.Pow(Y1, 2) + Math.Pow(Z1, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_H = Math.Sqrt(X * X + Y * Y + Z * Z) * COS_ALPHA;

            COS_ALPHA = (X * X2 + Y * Y2 + Z * Z2) / ((Math.Pow(X2, 2) + Math.Pow(Y2, 2) + Math.Pow(Z2, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_V = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)) * COS_ALPHA;

            MECMOD.Factory2D fac = DrwView.Factory2D;
            fac.CreatePoint(VW_H, VW_V);

            DRAFTINGITF.DrawingText txt = DrwView.Texts.Add(pt1.get_Name()+"( " + (int)X + " , " + (int)Y + " , " + (int)Z + " )", VW_H + 20, VW_V - 20);
            txt.SetFontSize(0, 0, 12);

            DRAFTINGITF.DrawingLeader FDleadr = txt.Leaders.Add(VW_H, VW_V);
            */

            drwDoc.Update();

            //8.save the 도면 파일
            drwDoc.Save();
        }

        private void CreateTxt(DrawingViewGenerativeBehavior drwGenBeh, HybridShapePointCoord pt, DrawingView drwView)
        {
            double X1, Y1, Z1, X2, Y2, Z2;
            drwGenBeh.GetProjectionPlane(out X1, out Y1, out Z1, out X2, out Y2, out Z2);

            ProductStructureTypeLib.Product Prd = (ProductStructureTypeLib.Product)drwGenBeh.Document;
            MECMOD.PartDocument PrtDoc = (MECMOD.PartDocument)Prd.ReferenceProduct.Parent;
            MECMOD.Part Prt = PrtDoc.Part;

            MECMOD.Body Bdy = Prt.MainBody;
            MECMOD.Shapes Shps = Bdy.Shapes;

            //7.point txt, leader생성
            CreateTxt(drwGenBeh, pt, drwView);
            object[] ap = new object[3];
           
            pt1.GetCoordinates(ap);

            double X = (double)ap[0];
            double Y = (double)ap[1];
            double Z = (double)ap[2];

            double COS_ALPHA = 0, VW_H = 0, VW_V = 0;

            //계산과정
            COS_ALPHA = (X * X1 + Y * Y1 + Z * Z1) / ((Math.Pow(X1, 2) + Math.Pow(Y1, 2) + Math.Pow(Z1, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_H = Math.Sqrt(X * X + Y * Y + Z * Z) * COS_ALPHA;

            COS_ALPHA = (X * X2 + Y * Y2 + Z * Z2) / ((Math.Pow(X2, 2) + Math.Pow(Y2, 2) + Math.Pow(Z2, 2)) * Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));
            VW_V = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)) * COS_ALPHA;

            MECMOD.Factory2D fac = drwView.Factory2D;
            fac.CreatePoint(VW_H, VW_V);

            DRAFTINGITF.DrawingText txt = drwView.Texts.Add(pt.get_Name() + "( " + (int)X + " , " + (int)Y + " , " + (int)Z + " )", VW_H + 20, VW_V - 20);
            txt.SetFontSize(0, 0, 12);

            DRAFTINGITF.DrawingLeader FDleadr = txt.Leaders.Add(VW_H, VW_V);

        }
    }
}
