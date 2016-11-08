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

namespace Ex_03
{
    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;
        List<INFITF.AnyObject> Pts = new List<INFITF.AnyObject>(); // 여러개 선택가능해짐(배열이랑 비슷?)
        HybridBody SelGS = null;
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
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // 카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                label3.Text = "Please run CATIA";
                return;
            }

            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label3.Text = "활성 문서가 없습니다";
            }
            // 선택 기능 실행
            
            Sel = Catia.ActiveDocument.Selection;

            Object[] InputObjectType = { "HybridBody" };
            string Status;
            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select a Geometrical Set", false);

            if (Status != "Normal")
            {
                label3.Text = "취소";
                return;
            }

            if (Sel.Count < 1)
            {
                label3.Text = "선택";
                return;
            }

            SelGS = (HybridBody)Sel.Item(1).Value;
            textBox1.Text = SelGS.get_Name();

            int ptCount =0;
            for (int i = 1; i <= SelGS.HybridShapes.Count; i++)
            {
                ptCount++;
                Pts.Add(SelGS.HybridShapes.Item(i));                
            }
            label4.Text = ptCount + "";
        }

        private void button1_Click(object sender, EventArgs e)
        { 
           try {
                PartDocument prtDoc = (PartDocument)Catia.ActiveDocument;
                Part prt = prtDoc.Part;
                HybridBody hbdy = prt.HybridBodies.Item(1);
                HybridShapeFactory hfac = (HybridShapeFactory)prt.HybridShapeFactory;
                //SelGS = (HybridBody)Sel.Item(1).Value;

                //INFITF.AnyObject aaa = Pts[0];
                    for (int i = 0; i <= (Pts.Count-1); i ++)
                    {
                        for (int j = i+1; j < Pts.Count; j++)
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
