//20161104 saori
//p.44


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
using System.Runtime.InteropServices;

namespace TestingSelectionUsingScripts
{
    

    public partial class Form1 : Form
    {
        INFITF.Application catia;
        object[] arrCoord1 = new object[2];
        object[] arrCoord2 = new object[2];
        Point2D p1;
        Point2D p2;
        Line2D lin;
        Point HyShpPtCoord1;
        Point HyShpPtCoord2;
        INFITF.Selection sel;

        public Form1()
        {
            InitializeComponent();
        }
        //form 실행과 동시에 카티아 실행한다---------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible = true;
            }
            PartDocument prtDoc = (PartDocument)catia.Documents.Open(@"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\TestingSelectionUsingScripts\Part.CATPart");
        }

        //point1 가지고 오기-------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
           // object[] arrCoord = new object[2];

            try
            {
                sel = catia.ActiveDocument.Selection;
                p1 = (Point2D)sel.FindObject("CATIAPoint2D");

                //txt에 표시하기
                p1.GetCoordinates(arrCoord1);
                textBox1.Text = p1.get_Name() + " ( " + arrCoord1[0] +  " , " +arrCoord1[1] + " )";

            }
            catch (Exception)
            {
                MessageBox.Show("선택된 정보가 없습니다.");
                return ;
            }
        }
        //point2 가져오기------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
           // object[] arrCoord = new object[2];

            try
            {
                sel = catia.ActiveDocument.Selection;
                p2 = (Point2D)sel.FindObject("CATIAPoint2D");

                p2.GetCoordinates(arrCoord2);
                textBox2.Text = p2.get_Name() + " ( " + arrCoord2[0] + " , " + arrCoord2[1] + " )";
            }
            catch (Exception)
            {
                MessageBox.Show("선택한 정보가 없습니다.");
                return ;
            }

        }
        //line 가져오기-------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sel = catia.ActiveDocument.Selection;
                lin = (Line2D)sel.FindObject("CATIALine2D");
                textBox3.Text = lin.get_Name();

             }
            catch (Exception)
            {
                MessageBox.Show("선택한 정보가 없습니다.");
                return;
            }
        }
        //create 3DLine------------------------------------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            //object[] Crd1 = new object[2];
            //object[] Crd2 = new object[2];
            Pobject[] CrdSt = new object[2];
            object[] CrdEn = new object[2];

            try
            {
                Point2D strP = lin.StartPoint;
                Point2D endP = lin.EndPoint;

                strP.GetCoordinates(CrdSt);
                endP.GetCoordinates(CrdEn);

                PartDocument prtDoc = (PartDocument)catia.ActiveDocument;
                

                GeometricElement geo2D = (GeometricElement)p1.Parent;
                Sketch skt = (Sketch)geo2D.Parent;
                Factory2D fac = skt.OpenEdition();

                Line2D lin2 = fac.CreateLine((double)CrdSt[0], (double)CrdSt[1], (double)arrCoord1[0], (double)arrCoord1[1]);
                lin2.StartPoint = strP;
                lin2.EndPoint = p1;
                Line2D lin3 = fac.CreateLine((double)CrdEn[0], (double)CrdEn[1], (double)arrCoord1[0], (double)arrCoord1[1]);
                lin3.StartPoint = endP;
                lin3.EndPoint = p1;
                Line2D lin4 = fac.CreateLine((double)arrCoord1[0], (double)arrCoord1[1], (double)arrCoord2[0], (double)arrCoord2[1]);
                lin4.StartPoint = p1;
                lin4.EndPoint = p2;
                //Line2D lin2 = CreateLine(fac,strP,p1) ;

                skt.CloseEdition();

            }
            catch (Exception)
            {
                MessageBox.Show("선택한 정보가 없습니다.");
                return;
            }
        }
        // Create3DLine-------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {

        }
        /*  // CreateLine method--------------------------------------
 private Line2D CreateLine(Factory2D fac, Point2D pt1, Point2D pt2)
 {
     pt1.GetCoordinates();
     Line2D lin = fac.CreateLine();
 }*/
    }
}
