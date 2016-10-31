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
            MECMOD.Sketch skt = skts.Add(xypln);

            MECMOD.Factory2D fac2d = skt.OpenEdition();

            Point2D p1 = fac2d.CreatePoint(10,10);
            Point2D p2 = fac2d.CreatePoint(10, 40);
            Point2D p3 = fac2d.CreatePoint(40, 40);
            Point2D p4 = fac2d.CreatePoint(40, 10);
            Point2D p5 = fac2d.CreatePoint(30, 20);
            Point2D p6 = fac2d.CreatePoint(20, 5);

            Line2D linLft = fac2d.CreateLine(10,10,10,40);
            Line2D linTop = fac2d.CreateLine(10, 40, 40, 40);
            Line2D linRgt = fac2d.CreateLine(40, 40, 40, 10);

            Point2D[] parry = { p1, p6, p5, p4 };
            Spline2D spl = fac2d.CreateSpline(parry);

            linLft.StartPoint = p1;
            linLft.EndPoint = p2;
            linRgt.StartPoint = p3;
            linRgt.EndPoint = p4;
            linTop.StartPoint = p2;
            linTop.EndPoint = p3;

            MECMOD.Constraint cnst = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeAxisPerpendicularity,(INFITF.Reference)linLft,(INFITF.Reference)linTop);
            /*
            INFITF.Reference r1 = prt.CreateReferenceFromGeometry(linLft);
            INFITF.Reference r2 = prt.CreateReferenceFromGeometry(linTop);
            MECMOD.Constraint cnst1 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeAxisPerpendicularity, r1, r2);
            */
            skt.CloseEdition();
        }
    }
}
