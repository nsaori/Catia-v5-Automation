//20161108 saori
//ex04
//textboxdml의 좌표를 읽어오고 1.점표시 2.spline생성하기~~
//최종 목표부터 보고 그리자--(그걸 그리기 위해 뭐가 필요한지 먼저 아는 것)

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
using HybridShapeTypeLib;

namespace Ex04
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        PartDocument prtDoc = null;

        public Form1()
        {
            InitializeComponent();
        }

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
           /* try
            {
                prtDoc = (PartDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                prtDoc = (PartDocument)catia.Documents.Add("Part");
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<Pnt> pList = new List<Pnt>();
            List <HybridShapeTypeLib.Point> pList = new List<HybridShapeTypeLib.Point>();

            //prtDoc = (PartDocument)catia.Documents.Item(1);
            prtDoc = (PartDocument)catia.Documents.Add("Part");

            Part prt = prtDoc.Part;
            HybridBodies hbdys = prt.HybridBodies;
            HybridBody hbdy = hbdys.Add();
            hbdy.set_Name("saori");
            HybridShapeFactory hsFac = (HybridShapeFactory)prt.HybridShapeFactory;

            //string Line = textBox1.Text;
            string lin = "";

            if (textBox1.Text == "") {
                MessageBox.Show("좌표를 입력해주새요");
                return;
            }

            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                lin = textBox1.Lines[i];
                string[] token = lin.Split(',');
                int x = int.Parse(token[0]);
                int y = int.Parse(token[1]);
                int z = int.Parse(token[2]);
                // pList.Add(new Pnt(x, y, z));
                pList.Add(hsFac.AddNewPointCoord(x,y,z));
                HybridShapeTypeLib.Point p = hsFac.AddNewPointCoord(x, y, z); //create a point
                hbdy.AppendHybridShape(p);
            }
            //create points and spline
            HybridShapeSpline splin = hsFac.AddNewSpline();
            foreach (HybridShapeTypeLib.Point p in pList)
            {
                INFITF.Reference r = prt.CreateReferenceFromGeometry(p);
               // hsFac.AddNewPointDatum(r);
                //hbdy.AppendHybridShape(p);
                splin.AddPoint(r);
            }
            hbdy.AppendHybridShape(splin);
            prt.Update();
        }
    }
}
