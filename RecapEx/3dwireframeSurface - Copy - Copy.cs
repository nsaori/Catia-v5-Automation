//20161102 p.38 다시풀기
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using HybridShapeTypeLib;   //GSMInterface

namespace WireframeSurface
{
    class Serface
    {
        static void Main(string[] args)
        {
            INFITF.Application catia;
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
            }
            catia.Visible = true;

            PartDocument prtdoc = (PartDocument)catia.Documents.Add("Part");
            Part prt = prtdoc.Part;

            HybridBodies hbdys = prt.HybridBodies;
            HybridBody hbdy = hbdys.Add();
            HybridShapeFactory hsFac = (HybridShapeFactory)prt.HybridShapeFactory;

            Point p1 = hsFac.AddNewPointCoord(10, 60, 30);
            Point p2 = hsFac.AddNewPointCoord(70, 75, 35);
            Point p3 = hsFac.AddNewPointCoord(100, 80, 30);
            Point p4 = hsFac.AddNewPointCoord(100, 80, 40);
            Point p5 = hsFac.AddNewPointCoord(95, 20, 45);
            Point p6 = hsFac.AddNewPointCoord(100, 10, 50);

            INFITF.Reference r1 = prt.CreateReferenceFromGeometry(p1);
            INFITF.Reference r2 = prt.CreateReferenceFromGeometry(p2);
            INFITF.Reference r3 = prt.CreateReferenceFromGeometry(p3);
            INFITF.Reference r4 = prt.CreateReferenceFromGeometry(p4);
            INFITF.Reference r5 = prt.CreateReferenceFromGeometry(p5);
            INFITF.Reference r6 = prt.CreateReferenceFromGeometry(p6);

            HybridShapeSpline hspln1 = hsFac.AddNewSpline();
            HybridShapeSpline hspln2 = hsFac.AddNewSpline();

            hspln1.AddPoint(r1);
            hspln1.AddPoint(r2);
            hspln1.AddPoint(r3);

            hspln2.AddPoint(r4);
            hspln2.AddPoint(r5);
            hspln2.AddPoint(r6);

            hbdy.AppendHybridShape(hspln1);
            hbdy.AppendHybridShape(hspln2);

            INFITF.Reference rspl1 = prt.CreateReferenceFromGeometry(hspln1);
            INFITF.Reference rspl2 = prt.CreateReferenceFromGeometry(hspln2);

            HybridShapeSweepExplicit swp = hsFac.AddNewSweepExplicit(rspl1,rspl2);
            hbdy.AppendHybridShape(swp);

            Point p = hsFac.AddNewPointCoord(50,30,100);
            hbdy.AppendHybridShape(p);
            HybridShapeProject prjct = hsFac.AddNewProject((INFITF.Reference)p,(INFITF.Reference)swp);
            hbdy.AppendHybridShape(prjct);

            prt.Update();
        }
    }
}
