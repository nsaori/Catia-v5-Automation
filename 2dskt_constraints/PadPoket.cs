//p.30

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
using PARTITF;
using MECMOD;

namespace _2dskt_constraints
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            INFITF.Application catia; //add the reference - catia v5 infiit interface...

            try
            {
                //열려 있는 catia가져오기
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                //catia 실행하기
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible = true;
            }

            MECMOD.PartDocument prtDoc = (MECMOD.PartDocument)catia.Documents.Add("Part");
            MECMOD.Part prt = prtDoc.Part;
            MECMOD.Bodies bdys = prt.Bodies;
            MECMOD.Body bdy = bdys.Add();
            MECMOD.Sketches skts = bdy.Sketches;

            INFITF.Reference xypln = (INFITF.Reference)prt.OriginElements.PlaneXY;
            Sketch skt1 = skts.Add(xypln);

            Factory2D fac2d = skt1.OpenEdition();   //fac2d안에서 sketch가 이루어진다. fac2d cketch의 기능을 쓸수있다

            Point2D p1 = fac2d.CreatePoint(10, 10);
            Point2D p2 = fac2d.CreatePoint(10, 30);
            Point2D p3 = fac2d.CreatePoint(40, 30);
            Point2D p4 = fac2d.CreatePoint(40, 10);

            //method를 만들고 line생성
            Line2D lin1 = CreateLine(fac2d, p1, p2);
            Line2D lin2 = CreateLine(fac2d, p2, p3);
            Line2D lin3 = CreateLine(fac2d, p3, p4);
            Line2D lin4 = CreateLine(fac2d, p4, p1);

            CatConstraintType cnstDis = CatConstraintType.catCstTypeDistance;
            
             
            MECMOD.Constraint d1 = createCnst(prt, skt1, cnstDis, lin1, lin3);
            MECMOD.Constraint d2 = createCnst(prt, skt1, cnstDis, lin2, lin4);
            MECMOD.Constraint d3 = createCnst(prt, skt1, cnstDis, skt1.AbsoluteAxis.HorizontalReference, lin4);
            MECMOD.Constraint d4 = createCnst(prt, skt1, cnstDis, skt1.AbsoluteAxis.VerticalReference, lin1);
            /*
            INFITF.Reference rline1 = prt.CreateReferenceFromGeometry(lin1);
            INFITF.Reference rline2 = prt.CreateReferenceFromGeometry(lin2);
            INFITF.Reference rline3 = prt.CreateReferenceFromGeometry(lin3);
            INFITF.Reference rline4 = prt.CreateReferenceFromGeometry(lin4);
            INFITF.Reference rlineH = prt.CreateReferenceFromGeometry(skt1.AbsoluteAxis.HorizontalReference);
            INFITF.Reference rlineV = prt.CreateReferenceFromGeometry(skt1.AbsoluteAxis.VerticalReference);

            MECMOD.Constraint d1 = skt1.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rline1, rline3);
            MECMOD.Constraint d2 = skt1.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rline2, rline4);
            MECMOD.Constraint d3 = skt1.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineH, rline4);
            MECMOD.Constraint d4 = skt1.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineV, rline1);
            */
            skt1.CloseEdition();

           //create a circlr
            Sketch skt2 = skts.Add(xypln);
            Factory2D fac2d1 = skt2.OpenEdition();
           //fac2d = skt2.OpenEdition();   //이렇게 함녀 된다.

            INFITF.Reference H = prt.CreateReferenceFromGeometry(skt2.AbsoluteAxis.HorizontalReference);
            INFITF.Reference V = prt.CreateReferenceFromGeometry(skt2.AbsoluteAxis.VerticalReference);

            Circle2D c = fac2d1.CreateClosedCircle(40,30,10);
            c.CenterPoint = p3;
            // MECMOD.Constraint orPtH = skt2.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance,H,(INFITF.Reference)c);
            //MECMOD.Constraint orPtV = skt2.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, V, (INFITF.Reference)c);
            MECMOD.Constraint r = skt2.Constraints.AddMonoEltCst(CatConstraintType.catCstTypeRadius,(INFITF.Reference)c);

            skt2.CloseEdition();

            ShapeFactory ShpFac = (ShapeFactory)prt.ShapeFactory;
            //pad
            Pad pad = ShpFac.AddNewPad(skt1, 20);

            //poket
            Pocket pock = ShpFac.AddNewPocket(skt2,20);
            pock.DirectionOrientation = CatPrismOrientation.catRegularOrientation;

            prt.Update();
        }

        private MECMOD.Constraint createCnst(Part prt, Sketch skt, CatConstraintType cnstType, INFITF.AnyObject ob1, INFITF.AnyObject ob2)
        {
            INFITF.Reference ref1 = prt.CreateReferenceFromGeometry(ob1);
            INFITF.Reference ref2 = prt.CreateReferenceFromGeometry(ob2);
            MECMOD.Constraint cnst = skt.Constraints.AddBiEltCst(cnstType, ref1, ref2);
            return cnst;
        }

        private static Line2D CreateLine(Factory2D fac, Point2D p1, Point2D p2)
        {
            //point 두개를 받아서 
            object[] ob1 = new object[2];
            p1.GetCoordinates(ob1);

            object[] ob2 = new object[2];
            p2.GetCoordinates(ob2);

            Line2D line = fac.CreateLine((double)ob1[0], (double)ob1[1], (double)ob2[0], (double)ob2[1]);

            line.StartPoint = p1;
            line.EndPoint = p2;

            return line;
        }
    }
}
