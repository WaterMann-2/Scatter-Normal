using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;

namespace MaterialMaker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Bitmap map;
        Bitmap normalMap;
        string path = "c:\\matOutput";
        int seed;
        bool reloaded = false;

        public MainWindow() {
            InitializeComponent();
        }


        //Reloads images on click
        private void Reload_Click(object sender, RoutedEventArgs e) {

            int size;

            try {
                seed = (int)Convert.ToInt32(SeedSpace.Text);
            } catch {
                seed = 0;
                MessageBox.Show("invalid seed. Seed set to 0.");
            }

            try {
                size = (int)Convert.ToInt32(imgSize.Text);
            } catch {
                size = 250;
                MessageBox.Show("Invalid size. Size set to 250.");
            }

            float[,] randomInts = RandomPlot.randomArray(seed, size, size);

            //Creates random plot and turns into displayed image
            map = RandomPlot.toMap(randomInts);
            MapImage.Source = BitConvenience.ToBitmapImage(map);

            //Creates normalized plot and displays image
            normalMap = BitConvenience.toNormalMap(map);
            NormalImage.Source = BitConvenience.ToBitmapImage(normalMap);

            //Allows to save
            reloaded = true;
        }

        //Integer only for seed input
        private void SeedSpace_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]-");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Saves Standard map
        private void Save_Click(object sender, RoutedEventArgs e) {
            if (!reloaded) { 
                MessageBox.Show("Must be reloaded at least once"); 
                return ; 
            }

            System.IO.Directory.CreateDirectory(path);

            try {
                BitConvenience.saveBitmap(map, "" + seed as string + "_Standard", path);
                MessageBox.Show("Successfully Saved Standard Map!");
            } catch {
                MessageBox.Show("Could not save");
            }
        }

        //Saves Normal map
        private void SaveNormal_Click(object sender, RoutedEventArgs e) {
            if(!reloaded) { 
                MessageBox.Show("Must be reloaded at least once"); 
                return ; 
            }
            try {
                System.IO.Directory.CreateDirectory(path);
            } catch {
                MessageBox.Show("Must be a valid file path (Make sure you have two \\ )");
            }

            try {
                BitConvenience.saveBitmap(normalMap, "" + seed as string + "_Normal" , path);
                MessageBox.Show("Successfully Saved Normal Map!");
            } catch {
                MessageBox.Show("Could not save");
            }
        }

        //Number only for image size
        private void imgSize_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Updates file path
        private void filePath_TextChanged(object sender, TextChangedEventArgs e) {
            path = filePath.Text;
        }
    }
}
