//20161107 saori
//selection 
//.ActiveDocument ; 현재 docunebt
//try ; errer가 날까말까할떄
//if ; 물어보는 것 확인하는 것

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
        INFITF.Selection sel = null;
        INFITF.AnyObject selob = null;

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
        //point만 선택한다--------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            selob = null;

            if (catia == null) {
                MessageBox.Show("catia를 실행 해주세요.");
                return;+
            }
            if (catia.ActiveDocument == null) {
                MessageBox.Show("활성 document가 없습니다.");
                return;
            }
            object[] intype = { "Point" };
            //catia.ActiveDocument.Selection.SelectElement2(intype,"Select a Point",true); //esc  -> cancel

            string status;
            sel.Clear();
            status = sel.SelectElement2(intype, "Select a Point", true);

            if (status != "Normal" || sel.) {

                MessageBox.Show("선택한 정보가 없습니다." );
                return;
            }

            //textBox1.Text = sel.get_Name();
            //sel = catia.ActiveDocument.Selection;
            // string name = textBox1.Text;
            // HybridShapeTypeLib.Point p = (HybridShapeTypeLib.Point)sel.FindObject("Point");
            // sel.set_Name(name);

        }
        //line만 선택-------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (catia == null)
            {
                MessageBox.Show("catia를 실행 해주세요.");
                return;
            }
            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("활성 document가 없습니다.");
                return;
            }
            object[] intype = { "Line" };
            catia.ActiveDocument.Selection.SelectElement2(intype, "Select a line", true);
        }
        //plane만 선택-------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            if (catia == null)
            {
                MessageBox.Show("catia를 실행 해주세요.");
                return;
            }
            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("활성 document가 없습니다.");
                return;
            }
            object[] intype = { "Plane" };
            catia.ActiveDocument.Selection.SelectElement2(intype, "Select a plane", true);
        }
        //point remane
        private void button4_Click(object sender, EventArgs e)
        {
            if (catia == null)
            {
                MessageBox.Show("catia를 실행 해주세요.");
                return;
            }
            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("활성 document가 없습니다.");
                return;
            }
            try
            {
                sel = catia.ActiveDocument.Selection;
                // HybridShapeTypeLib.Point p = (HybridShapeTypeLib.Point)sel.FindObject("Point");
                INFITF.AnyObject p = sel.FindObject("Point");

                textBox1.Text = p.get_Name();

                //string name = textBox1.Text;

    }
            catch (Exception ex)
            {
                MessageBox.Show("선택한 정보가 없습니다.\n" + ex.Message);
                return;
            }
            
           // p.set_Name();
        }
    }
}
