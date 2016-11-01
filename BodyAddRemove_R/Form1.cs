using MECMOD;
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

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            INFITF.Application Catia;


            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
             }


            catch
            {
                Catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                  Catia.Visible = true;
            }
            INFITF.Documents Docts = Catia.Documents;

            MECMOD.PartDocument PrtDoc = (MECMOD.PartDocument)Docts.Add("Part");

            MECMOD.Part Prt = PrtDoc.Part;

            MECMOD.Bodies Bodis = Prt.Bodies;

            MECMOD.Body PartBody = Bodis.Item(1);

            MECMOD.Body Body = Bodis.Add();

            MECMOD.Sketches Skts = Body.Sketches;


            INFITF.Reference plane = (INFITF.Reference)Prt.OriginElements.PlaneXY;

            MECMOD.Sketch Skt = Skts.Add(plane);

            MECMOD.Factory2D Fac2D = Skt.OpenEdition();

           


            Point2D Pt1 = Fac2D.CreatePoint(50, 50);
            Point2D Pt2 = Fac2D.CreatePoint(50, 100);
            Point2D Pt3 = Fac2D.CreatePoint(100, 100);
            Point2D Pt4 = Fac2D.CreatePoint(100, 50);


            //  <<  Create Line >>
            Line2D Lin1 = Fac2D.CreateLine(50, 50, 50, 100);

            Line2D Lin2 = Fac2D.CreateLine(50, 100, 100, 100);

            Line2D Lin3 = Fac2D.CreateLine(100, 100, 100, 50);

            Line2D Lin4 = Fac2D.CreateLine(100, 50, 50, 50);

            //     Line2D Lin22 = MCreateLine(Fac2d, Pt1, Pt2);



            //라인의 시작점부터 마지막점을 결정 

            Lin1.StartPoint = Pt1;
            Lin1.EndPoint = Pt2;
            Lin2.StartPoint = Pt2;
            Lin2.EndPoint = Pt3;
            Lin3.StartPoint = Pt3;
            Lin3.EndPoint = Pt4;
            Lin4.StartPoint = Pt4;
            Lin4.EndPoint = Pt1;

            INFITF.Reference rline1 = Prt.CreateReferenceFromGeometry(Lin1);
            INFITF.Reference rline2 = Prt.CreateReferenceFromGeometry(Lin2);
            INFITF.Reference rline3 = Prt.CreateReferenceFromGeometry(Lin3);
            INFITF.Reference rline4 = Prt.CreateReferenceFromGeometry(Lin4);
            INFITF.Reference rlineH = Prt.CreateReferenceFromGeometry(Skt.AbsoluteAxis.HorizontalReference);
            INFITF.Reference rlineV = Prt.CreateReferenceFromGeometry(Skt.AbsoluteAxis.VerticalReference);

            MECMOD.Constraint d1 = Skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rline1, rline3);
            MECMOD.Constraint d2 = Skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rline2, rline4);
            MECMOD.Constraint d3 = Skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineH, rline4);
            MECMOD.Constraint d4 = Skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineV, rline1);

            Skt.CloseEdition();//Skech조건 끝 //

            //PAD를 하기 위해서 조건문을 만든다.//
            PARTITF.ShapeFactory ShpFac = (PARTITF.ShapeFactory)Prt.ShapeFactory;
            //그 shapeFactory를 돌출하기 위해서 AddNewPad를 사용해서 50만큼 돌출한다.//
            ShpFac.AddNewPad(Skt, 50);



            //새로운 Body2를 만들기 //
            MECMOD.Body Body2 = Bodis.Add();
            //planed을 기준으로 Skt2를 만든다.//
            MECMOD.Sketch Skt2 = (MECMOD.Sketch)Body2.Sketches.Add(plane);
            //스켓이 시작 //
            Fac2D = Skt2.OpenEdition();
            //  X=75,Y=75 를 중심으로 D=20의  원을  만들기//
            Circle2D Cir2D = Fac2D.CreateClosedCircle(75,75,20);
            //스켓을 끝내기//
            Skt2.CloseEdition();
            //PAD  80만큼 //
            ShpFac.AddNewPad(Skt2, 80);

            //Body3 만들기 //
            Body Body3 = Prt.Bodies.Add();
            //Skt3를 Body2 안에 plane면을 기준으로  Sketche를 한다.
            Sketch Skt3 = Body2.Sketches.Add(plane);
            //스킷을 시작//
            Fac2D = Skt3.OpenEdition();
            //  X=75,Y=75 를 중심으로 D=5의  원을  만들기//
            Fac2D.CreateClosedCircle(75,75,5);
            //스켓3을 끝내기//
            Skt3.CloseEdition();
            //스켓3을 80만큼 PAD를 한다.
            ShpFac.AddNewPad(Skt3, 80);
            /////////////////
            //PartBody에 Prat In Work Object를 사용해서 조건을 만든다.
            Prt.InWorkObject = PartBody;

            ShpFac.AddNewAdd(Body);//PartBody에 Body를추가
            ShpFac.AddNewAdd(Body2);//PartBody에 Body2를추가
            ShpFac.AddNewRemove(Body3);//PartBody에 Body3를 제거한다 


            Prt.Update();














        }
    }
}
