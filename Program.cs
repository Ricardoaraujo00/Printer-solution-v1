using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterUtility;
using ESC_POS_USB_NET.Printer;
using System.Drawing;

namespace printer1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Prepare for test.. Press one key");
            Console.ReadKey();
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            Image newImage = Image.FromFile(@"c:\photo\logo.bmp");
            //var BytesValue = ImageToByteArray(newImage);
            //var BytesValue = ReadAllBytes(@"c:\photo\logo.bmp");
            byte[] BytesValue = Encoding.ASCII.GetBytes("Sier Group 1 22 333 4444");
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Sier Group 1 22 333 4444\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontC());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Sier Group 1 22 333 4444\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());
            PrinterUtility.PrintExtensions.Print(BytesValue, "COM7");
            Console.WriteLine("Hope it went well.. Bye");
            Console.ReadKey();

            //Printer printer = new Printer("Thermal Printer");
            //printer.TestPrinter();
            //printer.FullPaperCut();
            //printer.PrintDocument();
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static byte[] CutPage()
        {
            List<byte> oby = new List<byte>();
            oby.Add(Convert.ToByte(Convert.ToChar(0x1D)));
            oby.Add(Convert.ToByte('V'));
            oby.Add((byte)66);
            oby.Add((byte)3);
            return oby.ToArray();
        }
    }
}
