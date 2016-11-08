//20161108 노다 사오리
//EX-03

using MECMOD;
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

namespace Ex_03_saori_
{
    public partial class Form1 : Form
    {
        INFITF.Application catia =null;
        INFITF.Selection sel = null;
        PartDocument prtDoc = null;
        HybridBody hbdy = null;
        object[] intype;

        public Form1()
        {
            InitializeComponent();
        }
        //form실행과 동시에 카티아 실행 & dpcument 실행한다---------------------
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
            //partDOcument 실행하기-----------------
            try
            {
                PartDocument prtDoc = (PartDocument)catia.ActiveDocument;
            }
            catch (Exception ex)
            {
                PartDocument prtDoc = (PartDocument)catia.Documents.Item(1);
            }
        }
        //1.Gs 선택-----------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            sel = null;
            try
            {
                object[] intype = { "HybridBody" };
                catia.ActiveDocument.Selection.SelectElement2(intype, "Select a Geometrical set", true);

                sel = catia.ActiveDocument.Selection;
                // 여기부터~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //hbdy = 
            }
            catch (Exception)
            {
                MessageBox.Show("선택된 정보가 없습니다.");
                return; 
            }
        }

    }
}
