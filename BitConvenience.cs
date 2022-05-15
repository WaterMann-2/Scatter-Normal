using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace MaterialMaker {
    public static class BitConvenience {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap) {
            using (var memory = new System.IO.MemoryStream()) {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public static Bitmap toNormalMap(Bitmap image) {
            Bitmap bitImage = image;
            #region Global Variables
            int w = bitImage.Width - 1;
            int h = bitImage.Height - 1;
            float sample_l;
            float sample_r;
            float sample_u;
            float sample_d;
            float x_vector;
            float y_vector;
            Bitmap normal = new Bitmap(bitImage.Width, bitImage.Height);
            #endregion
            for (int y = 0; y < w + 1; y++) {
                for (int x = 0; x < h + 1; x++) {
                    if (x > 0) { sample_l = bitImage.GetPixel(x - 1, y).GetBrightness(); } else { sample_l = bitImage.GetPixel(x, y).GetBrightness(); }
                    if (x < w) { sample_r = bitImage.GetPixel(x + 1, y).GetBrightness(); } else { sample_r = bitImage.GetPixel(x, y).GetBrightness(); }
                    if (y > 1) { sample_u = bitImage.GetPixel(x, y - 1).GetBrightness(); } else { sample_u = bitImage.GetPixel(x, y).GetBrightness(); }
                    if (y < h) { sample_d = bitImage.GetPixel(x, y + 1).GetBrightness(); } else { sample_d = bitImage.GetPixel(x, y).GetBrightness(); }
                    x_vector = (((sample_l - sample_r) + 1) * .5f) * 255;
                    y_vector = (((sample_u - sample_d) + 1) * .5f) * 255;
                    Color col = Color.FromArgb(255, (int)x_vector, (int)y_vector, 255);
                    normal.SetPixel(x, y, col);
                }
            }
            return normal;
        }

        public static void saveBitmap(Bitmap map, String fileName, String filePath) {
            map.Save(filePath+"\\"+fileName+".png");
        }
    }
}
