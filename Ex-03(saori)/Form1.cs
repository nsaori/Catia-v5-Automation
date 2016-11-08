//20161108 노다 사오리
//EX-03
//1.Geomatorical Set 선택 2.point수 세우기 3.line구리기

using HybridShapeTypeLib;
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
        List<INFITF.AnyObject> Pts = new List<INFITF.AnyObject>(); // 여러개 선택가능해짐(배열이랑 비슷?)
        

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
            ///*
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
            //*/
        }
        //1.Gs 선택-----------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // 카티아가 실행중?
            if (catia == null)
            {
                MessageBox.Show("Please run the CATIA");
                return;
            }

            // 활성 문서가 있는가?
            if (catia.ActiveDocument == null)
            {
                MessageBox.Show("document를 열어주세요.");
                return;
            }
            
            try
            {
                // 선택 기능 실행
                sel = catia.ActiveDocument.Selection;
                //Gs만 선택해주라~~

                Object[] intype = { "HybridBody" };
                //sel.Clear();
                string status = sel.SelectElement2(intype, "Select a Geometrical set", true);

                /*The state of the selection command once SelectElement2 returns.
                 *  It can be either "Normal" (the selection has succeeded),
                 *   "Cancel" (the user wants to cancel the VB command,
                 *    which must exit immediately), "Undo" or "Redo".
                */
                if (status != "Normal")
                {
                    MessageBox.Show("Cancel");
                    return;
                }

                if (sel.Count < 1)
                {
                    MessageBox.Show("GS를 선택해주세요");
                    return;
                }
                
                // GS이름 표시한다---
                hbdy = (HybridBody)sel.Item(1).Value;
                textBox1.Text = hbdy.get_Name();

                //point 수 표시한다---
                int ptCount = 0;
                for (int i = 1; i <= hbdy.HybridShapes.Count; i++)  //i=1부타 시작~~
                {
                    ptCount++;
                    Pts.Add(hbdy.HybridShapes.Item(i));
                }
                textBox2.Text = ptCount + "";

            }
            catch (Exception)
            {
                MessageBox.Show("선택된 정보가 없습니다.");
                return; 
            }
        }

        // create lines
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 prtDoc = (PartDocument)catia.ActiveDocument;
                Part prt = prtDoc.Part;
                hbdy = prt.HybridBodies.Item(1);
                HybridShapeFactory hfac = (HybridShapeFactory)prt.HybridShapeFactory;
                //SelGS = (HybridBody)Sel.Item(1).Value;

                //INFITF.AnyObject aaa = Pts[0];
                for (int i = 0; i <= (Pts.Count - 1); i++)
                {
                    for (int j = i + 1; j < Pts.Count; j++)
                    {
                        HybridShapeTypeLib.Point pt1 = (HybridShapeTypeLib.Point)Pts.ElementAt(i);
                        HybridShapeTypeLib.Point pt2 = (HybridShapeTypeLib.Point)Pts.ElementAt(j);

                        INFITF.Reference ref1 = prt.CreateReferenceFromGeometry(pt1);
                        INFITF.Reference ref2 = prt.CreateReferenceFromGeometry(pt2);

                        HybridShapeTypeLib.Line lin = hfac.AddNewLinePtPt(ref1, ref2);
                        hbdy.AppendHybridShape(lin);
                    }
                }
                prt.Update();
            }
            catch (Exception)
            {
                MessageBox.Show("선택된 정보가 없습니다.");
                return;
            }
        }
    }
}
