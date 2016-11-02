//20161102 txt를 읽어오고 점 찍기

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using HybridShapeTypeLib;   //GSMInterface
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace WireframeSurface
{
    /*
    class Pt{
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Pt(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }
        public Pt() {
            X = 0;
            Y = 0;
            Z = 0;
        }
    }*/

    class Point_fileIo
    {
        static void Main(string[] args)
        {
            /*
            StreamReader sr = new StreamReader("point.txt");
            String line = "";
            List<Pt> ptlist = new List<Pt>();
            
            while ((line = sr.ReadLine()) != null)
            {
                string[] token = line.Split(',');
                int x = int.Parse(token[0]);
                int y = int.Parse(token[1]);
                int z = int.Parse(token[2]);
                ptlist.Add(new Pt(x,y,z));
            }
            */
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

            try
            {
                StreamReader sr  = new StreamReader("C:/Users/517-11/Desktop/saori/Automation/Catia-v5-Automation/RecapEx/point.txt");
            
                 String line = "";
                //List<Pt> ptlist = new List<Pt>();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] token = line.Split(',');
                    int x = int.Parse(token[0]);
                    int y = int.Parse(token[1]);
                    int z = int.Parse(token[2]);
                    // ptlist.Add(new Pt(x, y, z));
                    Point p = hsFac.AddNewPointCoord(x,y,z);
                    hbdy.AppendHybridShape(p);
                }
                //Point p1 = hsFac.AddNewPointCoord(10, 60, 30);
                sr.Close();

                prt.Update();
            }
            catch (Exception e)
            {
                Console.WriteLine("파일io오류:"+e.Message);
            }
            
        }
    }
}
