using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace MaterialMaker {
    static class RandomPlot{

        public static float[,] randomArray(int seed, int width, int height) {
            
            float[,] map = new float[width, height];
            Random rand = new Random(seed);

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map[x, y] = (float)rand.NextDouble();
                }
            }

            return map;
        }

        public static Bitmap toMap(float[,] inputArray) {
            int arrayLength = inputArray.Length;
            Bitmap map = new Bitmap(inputArray.GetLength(0), inputArray.GetLength(1));


            for (int i = 0; i < inputArray.GetLength(0); i++) {
                for (int j = 0; j < inputArray.GetLength(1); j++) {
                    int inputColor = (int)(inputArray[j, i] * 255);
                    Color color = Color.FromArgb(inputColor,inputColor,inputColor);
                    map.SetPixel(i, j, color);
                }
            }
            return map;
        }

    }
}
