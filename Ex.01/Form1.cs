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

namespace Ex._01
{
    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;
        INFITF.AnyObject SelObj = null;

        public Form1()
        {
            InitializeComponent();

            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch
            {
                Catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                Catia.Visible = true;
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

            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label1.Text = "활성 문서가 없습니다";
            }
            // Point 선택 기능 실행
            INFITF.Selection Sel = null;
            Sel = Catia.ActiveDocument.Selection;

            Object[] InputObjectType = { "Point" };
            string Status;
            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select a Point", false);

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택";
                return;
            }

            //INFITF.SelectedElement SelObj = null; // 아이템 생성
            SelObj = (INFITF.AnyObject)Sel.Item(1).Value;

            textBox1.Text = SelObj.get_Name();
        }

        private void Rename1_Click(object sender, EventArgs e)
        {
            // 선택 내역?
            if (SelObj == null)
            {
                textBox1.Text = "선택한 Point가 없습니다.";
                return;
            }
            // 변경할 이름?
            if (textBox1.Text.Length < 1)
            {
                textBox1.Text = "변경할 이름을 입력하세요.";
                return;
            }
            SelObj.set_Name(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                label1.Text = "Please run CATIA";
                return;
            }

            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label1.Text = "활성 문서가 없습니다";
            }
            // Point 선택 기능 실행
            INFITF.Selection Sel = null;
            Sel = Catia.ActiveDocument.Selection;

            Object[] InputObjectType = { "Line" };
            string Status;
            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select an Line", false);

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택했나";
                return;
            }

            //INFITF.SelectedElement SelObj = null; // 아이템 생성
            SelObj = (INFITF.AnyObject)Sel.Item(1).Value;

            textBox2.Text = SelObj.get_Name();
        }

        private void Rename2_Click(object sender, EventArgs e)
        {
            // 선택 내역?
            if (SelObj == null)
            {
                textBox2.Text = "선택한 Line이 없습니다.";
                return;
            }
            // 변경할 이름?
            if (textBox2.Text.Length < 1)
            {
                textBox2.Text = "변경할 이름을 입력하세요.";
                return;
            }
            SelObj.set_Name(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                label1.Text = "Please run CATIA";
                return;
            }

            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label1.Text = "활성 문서가 없습니다";
            }
            // Point 선택 기능 실행
            INFITF.Selection Sel = null;
            Sel = Catia.ActiveDocument.Selection;

            Object[] InputObjectType = { "Plane" };
            string Status;
            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select an Plane", false);

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택했나";
                return;
            }

            //INFITF.SelectedElement SelObj = null; // 아이템 생성
            SelObj = (INFITF.AnyObject)Sel.Item(1).Value;

            textBox3.Text = SelObj.get_Name();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 선택 내역?
            if (SelObj == null)
            {
                textBox3.Text = "선택한 Line이 없습니다.";
                return;
            }
            // 변경할 이름?
            if (textBox3.Text.Length < 1)
            {
                textBox3.Text = "변경할 이름을 입력하세요.";
                return;
            }
            SelObj.set_Name(textBox3.Text);
        }
    }
}
