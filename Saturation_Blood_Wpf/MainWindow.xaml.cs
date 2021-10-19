using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Saturation_Blood_Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public View_Labels Livecharts_data;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = null;
            int Numb = 150;

            Livecharts_data = new View_Labels();
            DataContext = Livecharts_data;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            string adres = "q";
            string adres2 = "q";
            string datapath = "w";
            int da5 = 0;
            StringBuilder buffer2 = new StringBuilder();

            string image_name = "Chart.bmp";

            SaveToPng(cart, image_name);

            SaveFileDialog qqqq = new SaveFileDialog {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
            };

            if (qqqq.ShowDialog() == true)
            {               
                adres = qqqq.FileName;               
                buffer2.Insert(0, qqqq.FileName);

                da5 = buffer2.Length;
                buffer2.Remove(da5 - 9, 9);
                adres2 = buffer2.ToString();
                datapath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                System.IO.File.Copy(System.IO.Path.Combine(datapath, image_name), System.IO.Path.Combine(qqqq.InitialDirectory, qqqq.FileName),  true);
               
            }

        }

        public void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.Height+40, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

    }
}
