

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

using MECMOD;   // 아래서 사용할 떄 안 적어도 된다
using INFITF;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            INFITF.Application catia;        //INFITF.Application; returntype, catia; 변수명

            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application"); //실행되 있는 카티아 가져오기
            }
            catch (Exception ex)
            {
                //MessageBox.Show("카티아가 실행돼 있지 않습니다.");
                //MessageBox.Show("카티아 가져오기 오류: "+ex.Message);
                //catia 열기
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible = true;
            }
            //creat a part
            /*
            catia.Documents.Add("Part1");
            //catia.Documents.Open();
            MECMOD.PartDocument part1;  
            */
            MECMOD.PartDocument partDoc;
            partDoc = (MECMOD.PartDocument)catia.Documents.Add("Part");

            MECMOD.Part prt = partDoc.Part; //partDoc에 part를 담은 것

            MECMOD.Bodies bodies = prt.Bodies;

            MECMOD.Body body = bodies.Add();    //다름 body만들기
            //MECMOD.Body body0 = bodies.Item(1); //생성되 있는 partbody가져오기
            //MECMOD.Body body2 = bodies.Item("PartBody"); //해당되는 것에 이름
            //MECMOD.Body body1 = prt.MainBody;   //partbody가져오기

            MECMOD.Sketches skts = body.Sketches;

            INFITF.Reference xyPln =(INFITF.Reference)prt.OriginElements.PlaneXY;  //creat a plan

            MECMOD.Sketch skt = skts.Add(xyPln);    //plane에 skt를 작성한다.
                                                    //open the skt
            MECMOD.Factory2D fac2d = skt.OpenEdition();

            //6.create the points
            MECMOD.GeometricElements gme = prt.GeometricElements;
            MECMOD.Point2D p1 = fac2d.CreatePoint(10,10);
            MECMOD.Point2D p2 = fac2d.CreatePoint(10, 40);
            MECMOD.Point2D p3 = fac2d.CreatePoint(40, 40);
            MECMOD.Point2D p4 = fac2d.CreatePoint(40, 10);
            MECMOD.Point2D p5 = fac2d.CreatePoint(30, 20);
             MECMOD.Point2D p6 = fac2d.CreatePoint(20, 5);

            //7.create the lines
            MECMOD.Line2D line1 = fac2d.CreateLine(10, 10, 10, 40);
            MECMOD.Line2D line2 = fac2d.CreateLine(10, 40, 40, 40);
            MECMOD.Line2D line3 = fac2d.CreateLine(40, 40, 40, 10);

            //8.9 create  the spline
            Object[] pa = {p1,p6,p5,p4 };
            MECMOD.Spline2D spline = fac2d.CreateSpline(pa);

            //구속조건 추가
            //10
            line1.StartPoint = p1;
            line1.EndPoint = p2;

            line2.StartPoint = p2;
            line2.EndPoint = p3;

            line3.StartPoint = p3;
            line3.EndPoint = p4;

            //11.create references
            INFITF.Reference rline1 = prt.CreateReferenceFromGeometry(line1);
            INFITF.Reference rline2 = prt.CreateReferenceFromGeometry(line2);

            INFITF.Reference rline3 = prt.CreateReferenceFromGeometry(line3);

            //12.
            MECMOD.Constraint cns = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeAxisPerpendicularity, rline1,rline2);

            MECMOD.Constraint cns1 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeAxisPerpendicularity, rline3, rline2);

            //13.
            skt.CloseEdition();

            //catia.Visible = true;

        }
    }
}
