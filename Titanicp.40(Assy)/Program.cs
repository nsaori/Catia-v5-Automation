//20161102 p.40
//product assembly 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using PARTITF;
using System.Runtime.InteropServices;
using ProductStructureTypeLib;

namespace Titanicp._40_Assy_
{
    class Program
    {
        static void Main(string[] args)
        {
            INFITF.Application catia;
            try
            {
                catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch (Exception)
            {
                catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
            }
            catia.Visible = true;

            ProductDocument pdctDoc = (ProductDocument)catia.Documents.Add("Product");
            Product pdct = pdctDoc.Product;
            Products pdcts = pdct.Products;

            object[] tpath =
            {
                @"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\Titanicp.40(Assy)\Assembly\Hull.CATPart"
             };
            pdcts.AddComponentsFromFiles(tpath,"*");

            Product pdct1 = pdcts.AddNewProduct("ass1");
            //Products ass1 = pdct1.Products;
            object[] tpath1 = 
            {
                @"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\Titanicp.40(Assy)\Assembly\Castle.CATPart",
                @"C:\Users\517-11\Desktop\saori\Automation\Catia-v5-Automation\Titanicp.40(Assy)\Assembly\Funnel.CATPart"
            };
            pdct1.Products.AddComponentsFromFiles(tpath1, "*");
            //ass1.AddComponentsFromFiles(tpath1,"*");

            //    Product pdctRef = pdcts.AddNewProduct("p");
            // pdctRef = pdct1.ReferenceProduct;
            Product pdctRef = pdct1.ReferenceProduct;
            Product pdct2 = pdcts.AddComponent(pdctRef);
            //Product pdct2 = pdcts.AddNewProduct("p2");
            //pdct1 = pdcts.AddComponent(pdctRef);
            //Product pdct3 = pdcts.AddNewProduct("p3");
            object[] aDistance = {1,0,0,0,1,0,0,0,1, 60, 0, 0 };
            pdct2.Move.Apply(aDistance);

            aDistance[9] = 120;
            Product pdct3 = pdcts.AddComponent(pdctRef);
            pdct3.Move.Apply(aDistance);
        }
    }
}
