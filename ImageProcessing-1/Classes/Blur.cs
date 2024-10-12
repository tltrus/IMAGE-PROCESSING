using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    static class Blur
    {
        struct Sum
        {
            public int R;
            public int G;
            public int B;
        }

        static Sum sum = new Sum();

        public static void GetBlur(WriteableBitmap wb1, WriteableBitmap wb2)
        {
            int wb1_width = (int)wb1.Width;
            int wb1_height = (int)wb1.Height;
            int wb2_width = (int)wb2.Width;
            int wb2_height = (int)wb2.Height;

            if (wb1_width != wb2_width || wb1_height != wb2_height) return;

            for (int y = 0; y < wb1_height; y++)
            {
                for (int x = 0; x < wb1_width; x++)
                {
                    if (x < 1 || y < 1 || x + 1 >= wb1_width || y + 1 >= wb1_height)
                        continue;

                    /* Set P to the average of 9 pixels:
                        X X X
                        X P X
                        X X X
                    */
                    sum.R = GetSumR(wb1, x, y);
                    sum.G = GetSumG(wb1, x, y);
                    sum.B = GetSumB(wb1, x, y);

                    // Calculate average.
                    byte R = (byte)(sum.R / 9);
                    byte G = (byte)(sum.G / 9);
                    byte B = (byte)(sum.B / 9);

                    wb2.SetPixel(x, y, R, G, B);
                }
            }
        }

        private static int GetSumR(WriteableBitmap wb1, int x, int y)
        {
            return wb1.GetPixel(x - 1, y + 1).R + // Top left 
                  wb1.GetPixel(x + 0, y + 1).R + // Top center
                  wb1.GetPixel(x + 1, y + 1).R + // Top right
                  wb1.GetPixel(x - 1, y + 0).R + // Mid left
                  wb1.GetPixel(x + 0, y + 0).R + // Current pixel
                  wb1.GetPixel(x + 1, y + 0).R + // Mid right
                  wb1.GetPixel(x - 1, y - 1).R + // Low left
                  wb1.GetPixel(x + 0, y - 1).R + // Low center
                  wb1.GetPixel(x + 1, y - 1).R;  // Low right
        }
        private static int GetSumG(WriteableBitmap wb1, int x, int y)
        {
            return wb1.GetPixel(x - 1, y + 1).G + // Top left 
                  wb1.GetPixel(x + 0, y + 1).G + // Top center
                  wb1.GetPixel(x + 1, y + 1).G + // Top right
                  wb1.GetPixel(x - 1, y + 0).G + // Mid left
                  wb1.GetPixel(x + 0, y + 0).G + // Current pixel
                  wb1.GetPixel(x + 1, y + 0).G + // Mid right
                  wb1.GetPixel(x - 1, y - 1).G + // Low left
                  wb1.GetPixel(x + 0, y - 1).G + // Low center
                  wb1.GetPixel(x + 1, y - 1).G;  // Low right
        }
        private static int GetSumB(WriteableBitmap wb1, int x, int y)
        {
            return wb1.GetPixel(x - 1, y + 1).B + // Top left 
                  wb1.GetPixel(x + 0, y + 1).B + // Top center
                  wb1.GetPixel(x + 1, y + 1).B + // Top right
                  wb1.GetPixel(x - 1, y + 0).B + // Mid left
                  wb1.GetPixel(x + 0, y + 0).B + // Current pixel
                  wb1.GetPixel(x + 1, y + 0).B + // Mid right
                  wb1.GetPixel(x - 1, y - 1).B + // Low left
                  wb1.GetPixel(x + 0, y - 1).B + // Low center
                  wb1.GetPixel(x + 1, y - 1).B;  // Low right
        }
    }
}
