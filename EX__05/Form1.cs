//2161108 saori
//ex05
//1.line 선택. 2.색 변경, 3.line width변경

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

namespace EX__05
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        INFITF.Selection sel = null;

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
            catch
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                catia.Visible = true;
            }
        }

        private void txtLine_MouseClick(object sender, MouseEventArgs e)
        {
            Sel = null;

            //카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                lblMsg.Text = "Please run CATIA ";
                return;
            }
            //활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다.");
                lblMsg.Text = "활성 문서가 없습니다.";
                return;
            }
            //Line 선택 기능실행
            //INFITF.Selection Sel = null;
            Sel = Catia.ActiveDocument.Selection;

            object[] InputObjectType = { "Line" };
            string Status;

            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select an line", false);

            if (Status != "Normal")
            {
                //MessageBox.Show("선택한 ....없습니다. ");
                lblMsg.Text = "선택한 ....없습니다. ";
                return;
            }
            if (Sel.Count < 1)
            {
                lblMsg.Text = "선택한 ....없습니다. ";
                return;
            }
            
            txtLine.Text = ((INFITF.AnyObject)Sel.Item(1).Value).get_Name();

        }

        private void rbRed_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(255, 0, 0);
        }

        private void rbGreen_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(0, 255, 0);
        }

        //private void SetColor(string Color)
        private void SetColor(int Red, int Green, int Blue)
        {
            //선택내역 확인
            if (Sel == null || Sel.Count < 1)
            {
                lblMsg.Text = "dfadsfsad";
                return;
            }

            //Color 변경
            Sel.VisProperties.SetRealColor(Red, Green, Blue, 0);

            Catia.RefreshDisplay = false;
            Catia.RefreshDisplay = true;

            return;
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            //선택내역 확인
            if (Sel == null || Sel.Count < 1)
            {
                lblMsg.Text = "dfadsfsad";
                return;
            }

            int Width = (int)nudWidth.Value;

            Sel.VisProperties.SetRealWidth(Width, 0);

            Catia.RefreshDisplay = false;
            Catia.RefreshDisplay = true;

            return;

        }
    }
}
