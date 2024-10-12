using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    // Based on: #90 — Floyd-Steinberg Dithering https://thecodingtrain.com/challenges/90-dithering
    static class Dethering
    {

        public static void GetDethering(WriteableBitmap wb, int factor)
        {
            int wb_width = (int)wb.Width;
            int wb_height = (int)wb.Height;

            for (int y = 0; y < wb_height - 1; y++)
            {
                for (int x = 1; x < wb_width - 1; x++)
                {
                    Color clr = wb.GetPixel(x, y);

                    float oldR = clr.R;
                    float oldG = clr.G;
                    float oldB = clr.B;

                    byte newR = ClosestStep(factor, oldR);
                    byte newG = ClosestStep(factor, oldG);
                    byte newB = ClosestStep(factor, oldB);

                    wb.SetPixel(x, y, Color.FromRgb(newR, newG, newB));

                    var errR = oldR - newR;
                    var errG = oldG - newG;
                    var errB = oldB - newB;

                    DistributeError(wb, x, y, errR, errG, errB);
                }
            }
        }

        private static byte ClosestStep(int factor, float value) => (byte)(Math.Round(factor * value / 255) * (255 / factor));

        private static void DistributeError(WriteableBitmap wb, int x, int y, float errR, float errG, float errB)
        {
            AddError(wb, 7f / 16f, x + 1, y, errR, errG, errB);
            AddError(wb, 3f / 16f, x - 1, y + 1, errR, errG, errB);
            AddError(wb, 5f / 16f, x, y + 1, errR, errG, errB);
            AddError(wb, 1f / 16f, x + 1, y + 1, errR, errG, errB);
        }

        private static void AddError(WriteableBitmap wb, float factor, int x, int y, float errR, float errG, float errB)
        {
            int wb_width = (int)wb.Width;
            int wb_height = (int)wb.Height;

            if (x < 0 || x >= wb_width || y < 0 || y >= wb_height) return;

            var clr = wb.GetPixel(x, y);

            float r = clr.R;
            float g = clr.G;
            float b = clr.B;

            r = r + errR * factor;
            g = g + errG * factor;
            b = b + errB * factor;

            if (r < 0)
            {
                r = 0;
            }
            else if (r > 255)
            {
                r = 255;
            }
            if (g < 0)
            {
                g = 0;
            }
            else if (g > 255)
            {
                g = 255;
            }
            if (b < 0)
            {
                b = 0;
            }
            else if (b > 255)
            {
                b = 255;
            }

            var color = Color.FromRgb((byte)r, (byte)g, (byte)b);

            wb.SetPixel(x, y, color);
        }
    }
}
