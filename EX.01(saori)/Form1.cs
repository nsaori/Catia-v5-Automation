//2161109 saori
//ex.01  point,ine,plane 선택, rename

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

namespace EX._01_saori_
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        PartDocument prtDoc = null;
        INFITF.AnyObject selOb = null;
        INFITF.Selection sel = null;

        public Form1()
        {
            InitializeComponent();
        }
        //catia 실행한다--------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible=true;
            }

            try
            {
                prtDoc = (PartDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("pleae open a active document");
            }
        }
        //point select
        private void button1_Click(object sender, EventArgs e)
        {
            selOb = null; //기본의 초기화!!
            textBox1.Text = "";

            IsRunCatDoc(); // //catia 가 실행되어 있는지, 황성되어 있는 document가 있는지

            sel = catia.ActiveDocument.Selection;

            object[] inputObjectType = { "Point" };
            string Status;

            sel.Clear();
            Status = sel.SelectElement2(inputObjectType, "Select a Point", true);

            if (Status != "Normal" || sel.Count < 1)
            {
                MessageBox.Show("선택한 point가 없습니다.");
                return;
            }

            INFITF.SelectedElement selItem;
            selItem = sel.Item(1);

            textBox1.Text = ((INFITF.AnyObject)selItem.Value).get_Name();
            selOb = (INFITF.AnyObject)selItem.Value;
        }

        //catia 가 실행되어 있는지, 황성되어 있는 document가 있는지-------------
        private void IsRunCatDoc()
        {
            if (catia == null)
            {
               MessageBox.Show("Please run Catia");
                return;

            }

            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("활성 문서가 없습니다");
                return;
            }
        }

        //point remane-------------------------------------------------------
        private void brp_Click(object sender, EventArgs e)
        {
            if (selOb == null)
            {
                MessageBox.Show("선택된 Point가 없습니다.");
                return;
            }
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("변경할 이름을 입력하세요");
                return;
            }
            selOb.set_Name(textBox1.Text);
        }
        //select a line
        private void bselLine_Click(object sender, EventArgs e)
        {
            IsRunCatDoc();

            selOb = null; //기본의 초기화!!
            textBox2.Text = "";

            sel = catia.ActiveDocument.Selection;

            object[] inputObjectType = { "Line" };
            string Status;

            sel.Clear();
            Status = sel.SelectElement2(inputObjectType, "Select a Line", true);

            if (Status != "Normal" || sel.Count < 1)
            {
                MessageBox.Show("선택한 line가 없습니다.");
                return;
            }

            INFITF.SelectedElement selItem;
            selItem = sel.Item(1);

            textBox2.Text = ((INFITF.AnyObject)selItem.Value).get_Name();
            selOb = (INFITF.AnyObject)selItem.Value;

        }
        //remane a line
        private void brlin_Click(object sender, EventArgs e)
        {
            if (selOb == null)
            {
                MessageBox.Show("선택된 line가 없습니다.");
                return;
            }
            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("변경할 이름을 입력하세요");
                return;
            }
            selOb.set_Name(textBox2.Text);
        }
        //select a plan---------------------------------------
        private void bselPlt_Click(object sender, EventArgs e)
        {
            IsRunCatDoc();

            selOb = null; //기본의 초기화!!
            textBox3.Text = "";

            sel = catia.ActiveDocument.Selection;

            object[] inputObjectType = { "Plane" };
            string Status;

            sel.Clear();
            Status = sel.SelectElement2(inputObjectType, "Select a Plane", true);

            if (Status != "Normal" || sel.Count < 1)
            {
                MessageBox.Show("선택한 Plane가 없습니다.");
                return;
            }

            INFITF.SelectedElement selItem;
            selItem = sel.Item(1);

            textBox3.Text = ((INFITF.AnyObject)selItem.Value).get_Name();
            selOb = (INFITF.AnyObject)selItem.Value;

        }

        private void brplt_Click(object sender, EventArgs e)
        {
            if (selOb == null)
            {
                MessageBox.Show("선택된 plane가 없습니다.");
                return;
            }
            if (textBox3.Text.Length < 1)
            {
                MessageBox.Show("변경할 이름을 입력하세요");
                return;
            }
            selOb.set_Name(textBox3.Text);
        }
    }
}

//뭔가 만드는 기능 ; collection, factry
//change name ; anyObject -> Name (c#; get_Name,Set_Name())