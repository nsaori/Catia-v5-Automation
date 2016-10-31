using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MECMOD;

namespace sktconsPractice
{
    class Program
    {
        static void Main(string[] args)
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

            Point2D p1 = fac2d.CreatePoint(50,50);
            Point2D p2 = fac2d.CreatePoint(50, 100);
            Point2D p3 = fac2d.CreatePoint(100, 100);
            Point2D p4 = fac2d.CreatePoint(100, 50);

            Line2D lin1 = fac2d.CreateLine(50,50,50,100);
            Line2D lin2 = fac2d.CreateLine(50, 100, 100, 100);
            Line2D lin3 = fac2d.CreateLine(100, 100, 100, 50);
            Line2D lin4 = fac2d.CreateLine(100, 50, 50, 50);

            lin1.StartPoint = p1;
            lin1.EndPoint = p2;

            lin2.StartPoint = p2;
            lin2.EndPoint = p3;

            lin3.StartPoint = p3;
            lin3.EndPoint = p4;

            lin4.StartPoint = p4;
            lin4.EndPoint = p1;

            INFITF.Reference rline1 = prt.CreateReferenceFromGeometry(lin1);
            INFITF.Reference rline2 = prt.CreateReferenceFromGeometry(lin2);
            INFITF.Reference rline3 = prt.CreateReferenceFromGeometry(lin3);
            INFITF.Reference rline4 = prt.CreateReferenceFromGeometry(lin4);
            INFITF.Reference rlineH = prt.CreateReferenceFromGeometry(skt.AbsoluteAxis.HorizontalReference);
            INFITF.Reference rlineV = prt.CreateReferenceFromGeometry(skt.AbsoluteAxis.VerticalReference);

            Constraint d1 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance,rline1,rline3);
            Constraint d2 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rline2, rline4);
            Constraint d3 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineH, rline4);
            Constraint d4 = skt.Constraints.AddBiEltCst(CatConstraintType.catCstTypeDistance, rlineV, rline1);

            skt.CloseEdition();

        }
    }
}
