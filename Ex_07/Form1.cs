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
        MECMOD.PartDocument doc = null;
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
                doc = (MECMOD.PartDocument)Catia.ActiveDocument;
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
                MessageBox.Show("Please run CATIA");
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
                doc = (MECMOD.PartDocument)Catia.ActiveDocument;
            }
            catch (Exception)
            {
                doc = (MECMOD.PartDocument)Catia.Documents.Add("Part");
            }

            try
            {
                Sel = Catia.ActiveDocument.Selection;
            }
            catch (Exception)
            {
                MessageBox.Show("copy할 요소를 선택해주세요");
                return;
            }  
               // MessageBox.Show(Sel.Count +" , " + Sel.Count2 +" , " + Sel.Item(1).get_Name() + " , " + Sel.Item(3).get_Name() + " , " + Sel.Item(3).get_Name()/* + " , " + Sel.Item(4).get_Name()*/);

            try
            {
                Sel.Copy();
                MessageBox.Show(Sel.Count + "개 복사 되엇습니다.");
            }
            catch (Exception)
            {
                MessageBox.Show("copy할 요소를 선택해주세요");
                return;
            }  
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Sel.Paste();
            }
            catch (Exception)
            {
                MessageBox.Show("paste할 요소가 없습니다");
                return;
            }

            doc.Part.Update();
        }
    }
}
