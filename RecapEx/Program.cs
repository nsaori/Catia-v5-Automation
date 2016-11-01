//내모 사각형 원 사각형만 뺴기

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MECMOD;

namespace Taitanic_p39
{
    class Program
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

            PartDocument prtDoc = (PartDocument)catia.Documents.Add("Part");
            Part prt1 = prtDoc.Part;
            //Part prt2 = prtDoc.Part;
            //Part prt3 = prtDoc.Part;

         //prt1 rectangre
            Bodies bdys1 = prt1.Bodies;
            Body bdy1 = bdys1.Add();
            Sketches skts1 = bdy1.Sketches;
            INFITF.Reference xyPln = (INFITF.Reference)prt1.OriginElements.PlaneXY;
            Sketch skt1 = skts1.Add(xyPln);
            Factory2D fac1 = skt1.OpenEdition();

                Point2D p1 = fac1.CreatePoint(0, 0);
                Point2D p2 = fac1.CreatePoint(10, 0);
                Point2D p3 = fac1.CreatePoint(10, 10);
                Point2D p4 = fac1.CreatePoint(0, 10);

                Line2D lin1 = CreateLine(fac1, p1, p2);
                Line2D lin2 = CreateLine(fac1, p2, p3);
                Line2D lin3 = CreateLine(fac1, p3, p4);
                Line2D lin4 = CreateLine(fac1, p4, p1);

                

            skt1.CloseEdition();
         //part2 circle
            //Bodies bdys2 = prt1.Bodies;
            Body bdy2 = bdys1.Add();
            //Sketches skts2 = bdy2.Sketches;
            //INFITF.Reference xyPln = (INFITF.Reference)prt2.OriginElements.PlaneXY;
            Sketch skt2 = skts1.Add(xyPln);
            fac1 = skt2.OpenEdition();



            skt2.CloseEdition();
         //part3 triangle
            //Bodies bdys3 = prt1.Bodies;
            Body bdy3 = bdys1.Add();
            //Sketches skts1 = bdy1.Sketches;
            //INFITF.Reference xyPln = (INFITF.Reference)prt1.OriginElements.PlaneXY;
            Sketch skt3 = skts1.Add(xyPln);
            fac1 = skt3.OpenEdition();



            skt3.CloseEdition();
        }

        private static Line2D CreateLine(Factory2D fac, Point2D p1, Point2D p2)
        {
            object[] ob1 = new object[2];
            p1.GetCoordinates(ob1);

            object[] ob2 = new object[2];
            p1.GetCoordinates(ob2);

            Line2D line = fac.CreateLine((double)ob1[0], (double)ob1[1], (double)ob2[0], (double)ob2[1]);

            line.StartPoint = p1;
            line.EndPoint = p2;

            return line;
        }
    }
}
