using System.Drawing;
using System.Drawing.Printing;

namespace Shopstock
{
    class PrintController
    {
        private Font pFont; //Font used for printing
        private string[] data; //Data to print

        public PrintController() //Constructor
        {
            pFont = new Font("Arial", 16);
        }

        public string[] Data { get => data; set => data = value; } 

        public void PrintReceipt(object sender, PrintPageEventArgs e) //prints data
                                                                      //Based on: https://docs.microsoft.com/en-us/dotnet/api/system.drawing.printing.printdocument.print?view=netframework-4.7.2
        {
            float linesPerPage = 50; //Max lines per page
            int count = 0; //for loop
            //Automatic marigns
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;


            // Iterate over the data array, printing each line.
            while (count < linesPerPage && count < Data.Length)
            {
                float yPos = topMargin + (count * pFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(Data[count], pFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }
        }



    }
}
