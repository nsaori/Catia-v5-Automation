//20161109 saori
//ex06  재귀호출 -> excel에 저장(level,)

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
using ProductStructureTypeLib;
using SPATypeLib;

using EXCEL = Microsoft.Office.Interop.Excel;


namespace recall
{
    public partial class Form1 : Form
    {
        INFITF.Application catia = null;
        ProductDocument prdDoc = null;
        //PartDocument prtDoc = null;

        EXCEL.Application excel = null;
        EXCEL.Workbook wb = null;
        EXCEL.Worksheet ws = null;
        EXCEL._Worksheet wss = null;

        int level = 0;
        int cellrowNum = 0;

        public Form1()
        {
            InitializeComponent();
        }
        //run the catia
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
                prdDoc = (ProductDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("please open a document");
            }
        }
        //call  a excel-----------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                excel = (EXCEL.Application)Marshal.GetActiveObject("EXCEL.Application");
                excel.Visible = true;
            }
            catch (Exception)
            {
                excel = (EXCEL.Application)Activator.CreateInstance(Type.GetTypeFromProgID("EXCEL.Application"));
                excel.Visible = true;
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("저장 경로를 입력해주세요.");
                return;
            }

            wb = excel.Workbooks.Open(textBox1.Text);

            ws = excel.ActiveSheet;

        }
        //record------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (excel == null)
            {
                MessageBox.Show("excel가 열러 있지 안습니다.");
                return;
            }
            if (wb == null)
            {
                MessageBox.Show("excel를 열려주세요");
            }
            ws = excel.ActiveSheet;

            if (catia == null)
            {
                MessageBox.Show("pleace run a catia");
                return;
            }
            try
            {
                prdDoc = (ProductDocument)catia.ActiveDocument;
            }
            catch (Exception)
            {
                MessageBox.Show("please open a document");
            }

            //prdDoc = (ProductDocument)catia.ActiveDocument;
            Product prd = prdDoc.Product;
            re(prd);
        }
       

        private void re(Product prd)
        {
            //SPAWorkbench spawb = prdDoc.GetWorkbench;
            //EXCEL.PictureFormat
            INFITF.Document doc = (INFITF.Document)prd.ReferenceProduct.Parent;
            string fullPath = doc.FullName;
            Products pds = prd.Products;
            

            cellrowNum++;
            ws.Cells[cellrowNum, 1] = level; //level 
            ws.Cells[cellrowNum, 2] = prd.get_PartNumber();
            ws.Cells[cellrowNum, 3] = fullPath;//저장경로
            ws.Cells[cellrowNum, 4] = prd.Analyze.Volume;//구피;volume
                                                         //imige capture

            //product열고 캡처한다.----------------------
            INFITF.Document tDoc = null;

            if (level != 0) //level 0일땐 열지 않아도 된다.
            {
                tDoc = catia.Documents.Open(fullPath);
            }
            
            INFITF.Window w =catia.ActiveWindow;
            INFITF.Viewer vwr = w.ActiveViewer;

            object[] oldClr = new object[3];
            vwr.GetBackgroundColor(oldClr);
            
            object[] clrArry= { 1, 1, 1 };
            vwr.PutBackgroundColor(clrArry);

            vwr.FullScreen = true;
            //catia.StartCommand("Fit All In");
            vwr.Reframe();  // same as abave
            catia.StartCommand("specifications"); //catia의 명령어(view>command)는 거의 다할 수 있는데 내용을 건들 수 없다.(흿킹 x)
             /*
            //iso view로 변경하기---
            INFITF.Camera3D camera = (INFITF.Camera3D)doc.Cameras.Item("*iso");
            object[] invew = new object[3];
            camera.Viewpoint3D.GetOrigin(invew);
            //object[] iso = {1,1,1 };
            //camera.Viewpoint3D.PutOrigin(iso);
            //v3.PutOrigin(iso);
            /////
            INFITF.Viewer3D v3 = (INFITF.Viewer3D)vwr;
            //INFITF.Viewpoint3D v3 = v3.Viewpoint3D;
            //object[] iso = { 1, 1, 1 };
            v3.Viewpoint3D.PutOrigin(invew);
            */

            vwr.Activate();
            //vwr.CaptureToFile(INFITF.CatCaptureFormat.catCaptureFormatJPEG, @"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\Assembly\recall\capture\img.jpg");
            vwr.CaptureToFile(INFITF.CatCaptureFormat.catCaptureFormatJPEG, fullPath+ cellrowNum + ".jpg");
            
            //wss = ws;
            //wss.Cells[1,1].Pi  //.Pictures.Insert(@"C:\Windows\System32\@BackgroundAccessToastIcon.png");
            // AddPicture(string Filename, Office.Core.MsoTriState LinkToFile, Office.Core.MsoTriState SaveWithDocument, float Left, float Top, float Width, float Height);
            EXCEL.Range ImgRane = ws.Cells[cellrowNum, 5];
            // ws.Shapes.AddPicture(@"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\Assembly\recall\capture\img.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, ImgRane.Left + 1, ImgRane.Top + 1, ImgRane.Width - 2.5, ImgRane.Height - 3);
            ws.Shapes.AddPicture(fullPath + cellrowNum+".jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, ImgRane.Left + 1, ImgRane.Top + 1, ImgRane.Width - 2.5, ImgRane.Height - 3);

            catia.StartCommand("specifications");
            vwr.PutBackgroundColor(oldClr);  
            vwr.FullScreen = false;

            if (level != 0)
            {
                tDoc.Close();
            }

            //propaties.designer-----------
            // ws.Cells[cellrowNum, 6] =prd.UserRefProperties
            string designer = "";
            try
            {
                designer = prd.Parameters.Item("designer").ValueAsString();
                //NavigatorTypeLib.Para =  prd.Parameters.GetItem("designer").;
            }
            catch (Exception)
            {
                designer = "";
            }
            

            ws.Cells[cellrowNum, 6] = designer;

            for (int i = 1; i <= pds.Count; i++) //1부터이어야 된다. i <= pds.Count(=있어야 한다)
            {
                // re(pds.Item(i));
                Product tpdt = pds.Item(i);
                level++;
                re(tpdt);
                level--;        //빠져나갈 떄

            }
        }
    }
}
