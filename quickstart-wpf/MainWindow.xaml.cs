using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DrawingQuickstartWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void myCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }

        private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Render();
        }

        void Render()
        {
            using (var bmp = new Bitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight))
            using (var canvas = Graphics.FromImage(bmp))
            {
                using (var pen = new Pen(Color.White))
                using (var blackBrush = new SolidBrush(Color.AntiqueWhite))
                using (var whiteBrush = new SolidBrush(Color.DarkGray))
                {
                    int w = bmp.Width / 8;
                    int h = bmp.Height / 8;
                    canvas.Clear(Color.White);
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if ((x + y) % 2 == 0)
                                canvas.FillRectangle(blackBrush, new Rectangle(x * w, y * h, w, h));
                            else
                                canvas.FillRectangle(whiteBrush, new Rectangle(x * w, y * h, w, h));
                        }
                    }
                }

				myImage.Source = BmpImageFromBmp(bmp);
            }
        }

        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
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
    }
}
