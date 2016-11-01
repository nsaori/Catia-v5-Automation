using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using HybridShapeTypeLib;

namespace WireframeSurface
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
            
            HybridBodies hybdys = prt.HybridBodies;
            HybridBody hybdy = hybdys.Add();
            HybridShapes hyshps = hybdy.HybridShapes;

            HybridShapeFactory hyshfac = (HybridShapeFactory)prt.HybridShapeFactory;

            Point p1 = hyshfac.AddNewPointCoord(10, 60, 30);
            Point p2 = hyshfac.AddNewPointCoord(70, 75, 35);
            Point p3 = hyshfac.AddNewPointCoord(100, 80, 30);

            Point p4 = hyshfac.AddNewPointCoord(100, 80, 40);
            Point p5 = hyshfac.AddNewPointCoord(95, 20, 45);
            Point p6 = hyshfac.AddNewPointCoord(100, 10, 50);

            INFITF.Reference r1 = prt.CreateReferenceFromGeometry(p1);
            INFITF.Reference r2 = prt.CreateReferenceFromGeometry(p2);
            INFITF.Reference r3 = prt.CreateReferenceFromGeometry(p3);
            INFITF.Reference r4 = prt.CreateReferenceFromGeometry(p4);
            INFITF.Reference r5 = prt.CreateReferenceFromGeometry(p5);
            INFITF.Reference r6 = prt.CreateReferenceFromGeometry(p6);

            HybridShapeSpline hyspl1 = hyshfac.AddNewSpline();
            HybridShapeSpline hyspl2 = hyshfac.AddNewSpline();


            hyspl1.AddPoint(r1);
            hyspl1.AddPoint(r2);
            hyspl1.AddPoint(r3);

            hyspl2.AddPoint(r4);
            hyspl2.AddPoint(r5);
            hyspl2.AddPoint(r6);

            hybdy.AppendHybridShape(hyspl1);
            hybdy.AppendHybridShape(hyspl2); //AppendHybridShape 만들고 바로 해야 된다

            INFITF.Reference rspl1 = prt.CreateReferenceFromGeometry(hyspl1);
            INFITF.Reference rspl2 = prt.CreateReferenceFromGeometry(hyspl2);

            HybridShapeSweepExplicit swp = hyshfac.AddNewSweepExplicit(rspl1, rspl2);
            hybdy.AppendHybridShape(swp);

            Point p = hyshfac.AddNewPointCoord(50,30,100);
            hybdy.AppendHybridShape(p);

            HybridShapeProject hsprjct = hyshfac.AddNewProject((INFITF.Reference)p,(INFITF.Reference)swp);

            
            
            prt.Update();
        }
    }
}
