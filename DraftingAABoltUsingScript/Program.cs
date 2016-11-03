//20161103 p.42

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using System.Runtime.InteropServices;
using DRAFTINGITF;

namespace DraftingAABoltUsingScript
{
    class Program
    {
        static void Main(string[] args)
        {
            INFITF.Application catia;
            try
            {
                catia =(INFITF.Application) Marshal.GetActiveObject("CATIA.Appication");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
            }
            catia.Visible = true;
            //open the file
            PartDocument prtDoc = (PartDocument)catia.Documents.Open(@"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\DraftingAABoltUsingScript\7_Drafting_the_bolt\Bolt.CATPart"); 

            DrawingDocument drwDoc = (DrawingDocument)catia.Documents.Open(@"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\DraftingAABoltUsingScript\7_Drafting_the_bolt\TitleBlock.CATDrawing");
            //3.get the active sheet
            DrawingSheet drwSheet = drwDoc.DrawingRoot.ActiveSheet;

            ////4. add a new view------------------------------------------------
            DrawingView FrntView = drwSheet.Views.Add("Front View");
            //5.Translate the view x;400 y;150
            FrntView.x = 400;
            FrntView.y = 150;
            //6.get the generativeBehavior 
            DrawingViewGenerativeBehavior FrntVwGB = FrntView.GenerativeBehavior;
                // define a front view with the xy plan 1,0,0,0,1,0
            FrntVwGB.DefineFrontView(1, 0, 0, 0, 1, 0);
            //7.assouciated the 3d docuemnt(bolt) with this view. 
            //ref; DrawingViewGenerativeBehavior (Object)
            //PartDocument prtToDoc = (PartDocument)catia.Documents.Item("CATPart1");
            //FrntView.GenerativeBehavior.Document = prtToDoc;
            FrntView.GenerativeBehavior.Document = prtDoc;

            //8.Add a top view x;400 y;600------------------------------------
            DrawingView Topvw = drwSheet.Views.Add("TopView");
            Topvw.x = 400;
            Topvw.y = 600;
            DrawingViewGenerativeBehavior TopVwGB = Topvw.GenerativeBehavior;
            TopVwGB.DefineProjectionView(TopVwGB,CatProjViewType.catTopView);
            //9. assouciated the 3d docuemnt(bolt) with this view. 
            Topvw.GenerativeBehavior.Document =prtDoc;

            //10.  Activate the view -----------------------------------
            //& get its Factory2D to create a circle int each view. 
            //C1(30,30,10) c2(30,,)
            FrntView.Activate();
            Factory2D facF = FrntView.Factory2D;
            Circle2D c1 = facF.CreateClosedCircle(30,30,10);

            Topvw.Activate();
            Factory2D facT = Topvw.Factory2D;
            Circle2D c2 = facT.CreateClosedCircle(30,30,20);

            //11. update the drawing document.
            drwDoc.Update();
        }
    }
}
