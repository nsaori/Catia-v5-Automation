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
using MECMOD;
using HybridShapeTypeLib;

namespace ex._05_saori_
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        PartDocument prtDoc = null;
        INFITF.Selection sel = null;

        public Form1()
        {
            InitializeComponent();
        }
        //래그 실행과 동시에 catia실행한다------------------------------
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
            //partDOcument 실행하기-----------------
            try
            {
                prtDoc = (PartDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("활성된 문서가 없습니다.");
                //PartDocument prtDoc = (PartDocument)catia.Documents.Add("Part");
            }
              }
        //line 선택하기---------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            sel = null; //action 시작할때 초기화할것!

            if (catia == null)
            {
                MessageBox.Show("Pleace run the CATIA");
                return; //이게 없으면 오류난다.
            }
            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("활성된 문서가 없습니다.");
                return; //이게 없으면 오류난다.
            }

            //line 선택
            sel = catia.ActiveDocument.Selection; /////???

            object[] inputObTyp = { "Line" };
            sel.Clear();
            string status = sel.SelectElement2(inputObTyp, "Select a line", true);

            if (status != "Normal")
            {
                MessageBox.Show("선택한 Line이 없습니다. ");
                return;
            }
            if (sel.Count < 1)
            {
                MessageBox.Show("line를 선택해주세요");
                return;
            }
            //line 이름을 표시한다
            textBox1.Text = ((INFITF.AnyObject)sel.Item(1).Value).get_Name();

        }
        //change the line color------------------------------------
        //line -> red
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(255, 0, 0);
        }

        //색을 지정하는 method-------------------------------
        private void SetColor(int red, int green, int blue)
        {
            if (sel==null||sel.Count<1)
            {
                MessageBox.Show("line를 선택해주세요.");
                return;
            }
            //색을 변경한다----
            sel.VisProperties.SetRealColor(red,green,blue,0);

            //catia.RefreshDisplay = false;
            catia.RefreshDisplay = true;

            return; 
        }
        //line -> green
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(0, 255, 0);
        }
        //line -> blue
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(0, 0, 255);
        }
        //line -> whilte
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SetColor(255, 255, 255);
        }
        //line 폭을 변경한다------------------------------
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (sel == null || sel.Count < 1)
            {
                MessageBox.Show("line를 선택해주세요.");
                return;
            }

            int width = (int)numericUpDown1.Value;

            sel.VisProperties.SetRealWidth(width,0);

            catia.RefreshDisplay = true;

            return;
        }
    }
}
