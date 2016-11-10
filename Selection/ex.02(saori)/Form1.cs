//20161109 saori
//ex.02 select a start Point & an end Point -> create a line

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MECMOD;
using HybridShapeTypeLib;
using System.Runtime.InteropServices;

namespace ex._02_saori_
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        PartDocument prtDoc = null;
        INFITF.Selection sel = null;
        //INFITF.AnyObject obj = null;
        INFITF.Reference ptStrt = null;
        INFITF.Reference ptEnd = null;

        public Form1()
        {
            InitializeComponent();
        }
        //run the farm & catia
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
            try
            {
                prtDoc = (PartDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("Please open a document.");
            }
        }
        //select a start point
        private void button1_Click(object sender, EventArgs e)
        {
            //obj = null;
            sel = catia.ActiveDocument.Selection;
            sel.Clear();
            object[] inputObjType = { "Point"};
            string status = sel.SelectElement2(inputObjType,"Select a start point", false);

            if (status != "Normal" || sel.Count <1) {
                MessageBox.Show("Select a start point.");
                return;
            }
            ptStrt = sel.Item(1).Reference;
        }
        //select an end point
        private void button2_Click(object sender, EventArgs e)
        {
            //obj = null;
            sel = catia.ActiveDocument.Selection;
            sel.Clear();
            object[] inputObjType = {"Point" };
            string status = sel.SelectElement2(inputObjType, "Select a end point", false);

            if (status != "Normal" || sel.Count < 1)
            {
                MessageBox.Show("Select a start point.");
                return;
            }
            ptEnd = sel.Item(1).Reference;
        }
        //create a line
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                prtDoc = (PartDocument)catia.ActiveDocument;
                HybridBody hbdy = (HybridBody)ptStrt.Parent;
                HybridShapeFactory hfac = (HybridShapeFactory)prtDoc.Part.HybridShapeFactory;

                Line lin = hfac.AddNewLinePtPt(ptStrt, ptEnd);
                hbdy.AppendHybridShape(lin);

                prtDoc.Part.Update();
            }
            catch (Exception)
            {
                MessageBox.Show("error");
                return;
            }
        }
    }
}

//programing - wireframe나 surface를 하는게 solid를 만드는 거보다 유리한다.


//product - prt,prt,prt,prt
//active ducument -> 최상위 documents