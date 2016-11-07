//20161107 saori
//selection 

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
using HybridShapeTypeLib;

namespace Selection
{
    public partial class Form1 : Form
    {
        INFITF.Application catia;
        INFITF.Selection sel;

        public Form1()
        {
            InitializeComponent();
        }
        //catia 실행
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible =true;
            }
        }
        //point만 선택한다
        private void button1_Click(object sender, EventArgs e)
        {
            object[] intype = { "Point" };
            catia.ActiveDocument.Selection.SelectElement2(intype,"Select a Point",true);

            sel = catia.ActiveDocument.Selection;
            string name = textBox1.Text;
           // HybridShapeTypeLib.Point p = (HybridShapeTypeLib.Point)sel.FindObject("Point");
           // sel.set_Name(name);
           
        }
        //line만 선택
        private void button2_Click(object sender, EventArgs e)
        {
            object[] intype = { "Line" };
            catia.ActiveDocument.Selection.SelectElement2(intype, "Select a line", true);
        }
        //plane만 선택
        private void button3_Click(object sender, EventArgs e)
        {
            object[] intype = { "Plane" };
            catia.ActiveDocument.Selection.SelectElement2(intype, "Select a plane", true);
        }
    }
}
