using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ReadXYCoordinatesFromImage.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Point currentPoint = new Point();

        List<Shape> m_ListOfShapes = new List<Shape>();

        Line tmpLine;
        public Window1()
        {
            InitializeComponent();
            paintSurface.MouseDown += paintSurface_MouseDown;
            paintSurface.MouseMove += paintSurface_MouseMove;
        }

        private void paintSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                if (m_ListOfShapes.Count == 0)
                { currentPoint = e.GetPosition(this); }
                else
                {
                    var l = (Line)m_ListOfShapes.Last(s => s.GetType() == typeof(Line));
                    currentPoint = new Point(l.X2, l.Y2);
                }
            }
            tmpLine = null;
        }

        private void paintSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (null == tmpLine)
                {
                    tmpLine = new Line();
                    tmpLine.Stroke = SystemColors.WindowFrameBrush;
                    tmpLine.X1 = currentPoint.X;
                    tmpLine.Y1 = currentPoint.Y;

                    m_ListOfShapes.Add(tmpLine);
                    paintSurface.Children.Add(tmpLine);
                }
                tmpLine.X2 = e.GetPosition(this).X;
                tmpLine.Y2 = e.GetPosition(this).Y;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)paintSurface.RenderSize.Width,
                (int)paintSurface.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(paintSurface);

            //var crop = new CroppedBitmap(rtb, new Int32Rect(50, 50, 250, 250));


            BitmapEncoder pngEncoder = new BmpBitmapEncoder();
            //pngEncoder.Frames.Add(BitmapFrame.Create(crop));
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            using (var fs = System.IO.File.OpenWrite(@"E:\Biezace10-2015\Read_XY_Coordinates_From_Image\ReadXYCoordinatesFromImage\kb.bmp"))
            {
                pngEncoder.Save(fs);
            }

        }
    }
}
