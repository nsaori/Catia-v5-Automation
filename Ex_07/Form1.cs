using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex_07
{
    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;
        INFITF.Document doc = null;
        INFITF.Selection Sel = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch
            {
                Catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                Catia.Visible = true;
            }

            try
            {
                doc = Catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("활성 문서가 없습니다");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                label1.Text = "Please run CATIA";
                return;
            }

            /*
            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label1.Text = "활성 문서가 없습니다";
            }
            */
            try
            {
                doc = Catia.ActiveDocument;
            }
            catch (Exception)
            {
                doc = Catia.Documents.Add("Part");
            }

            Sel = Catia.ActiveDocument.Selection;   //복수 선택 가능...?
                                                    // MessageBox.Show(Sel.Count +" , " + Sel.Count2 +" , " + Sel.Item(1).get_Name() + " , " + Sel.Item(3).get_Name() + " , " + Sel.Item(3).get_Name()/* + " , " + Sel.Item(4).get_Name()*/);
                                                    //INFITF.Selection sels = null;
                                                    //sels.Add

            //List<INFITF.AnyObject> list = new List<INFITF.AnyObject>();
           /* for (int i = 1; i <= Sel.Count; i++)
            {
                //list.Add((INFITF.AnyObject)Sel.Item(i).Value);
                INFITF.Selection s = null;
                INFITF.AnyObject a =(INFITF.AnyObject)Sel.Item(i).Value;
                s.Add(a);
                s.Copy();
                s.Paste();
            }
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Catia.ActiveDocument.Selection.Paste();
            Sel.Paste();


        }
    }
}
