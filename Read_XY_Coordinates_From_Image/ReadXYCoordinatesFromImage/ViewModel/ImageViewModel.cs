using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadXYCoordinatesFromImage.View;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ReadXYCoordinatesFromImage.Model;
using ReadXYCoordinatesFromImage.Utilities;
using System.Windows.Input;
using System.IO;

namespace ReadXYCoordinatesFromImage.ViewModel
{
    public class ImageViewModel
    {
        public ImageProcessingModel ImageModel { get; set; }
        public ICommand LoadImageCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand WarningCommand { get; set; }
        public Func<Object, Boolean> TrueFunc = new Func<object, bool>(o => true);

        private MainWindow m_MainWindow;
        public StringBuilder sb = new StringBuilder();

        public ImageViewModel(MainWindow _mainWindow)
        {
            ImageModel = new ImageProcessingModel();
            m_MainWindow = _mainWindow;
            this.LoadImageCommand = new CommandBase(BrowseAndLoadAction, TrueFunc);
            this.SaveCommand = new CommandBase(SaveAction, TrueFunc);
            this.ExitCommand = new CommandBase(ExitAction, TrueFunc);
            this.WarningCommand = new CommandBase(WarningAction, TrueFunc);
            ImageModel.IsSaveButtonEnabled = false;
        }

        private void WarningAction(object _parameter)
        {
            MessageBox.Show("Image file must contain POINTS painted ONLY WITH BLACK", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ExitAction(object _parameter)
        {
            m_MainWindow.Close();
        }

        private void BrowseAndLoadAction(object _parameter)
        {
            sb.Clear();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";

            //UWAGA:
            //*.JPG KOMPRESUJE OBRAZEK, WIEC NIE PRZECHOWUJE PIXEL PO PIXELU.

            //dlg.Filter = "Image files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg";
            dlg.Filter = "Image files (*.bmp)|*.bmp";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog().Value.Equals(true))
            {
                ImageModel.IsSaveButtonEnabled = false;

                ImageModel.SelectedFileName = dlg.FileName;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.DecodeFailed += bitmapImage_DecodeFailed;

                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(ImageModel.SelectedFileName);
                bitmapImage.EndInit();

                m_MainWindow.ImageViewer1.Stretch = Stretch.None;

                if (bitmapImage.PixelWidth < m_MainWindow.ActualWidth || bitmapImage.PixelWidth < m_MainWindow.ActualHeight)
                {
                    //m_MainWindow.ImageViewer1.Stretch = Stretch.UniformToFill;
                }
                else // source file to big, maybe decrease it
                {
                }
                m_MainWindow.ImageViewer1.Source = bitmapImage;

                long heightOfImage = bitmapImage.PixelHeight;
                long widthOfImage = bitmapImage.PixelWidth;
                int nStride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
                byte[] pixelByteArray = new byte[bitmapImage.PixelHeight * nStride];
                bitmapImage.CopyPixels(pixelByteArray, nStride, 0);

                if (m_MainWindow.Width <= widthOfImage) { m_MainWindow.Width = widthOfImage + 250; }

                long offset = 0;
                byte blue = 0;
                byte green = 0;
                byte red = 0;
                int color = 0;
                long founded = 0;

                string x_str;
                string y_str;

                if (bitmapImage.Format.BitsPerPixel == 32)
                {
                    for (long y = 0; y < heightOfImage; y++)
                    {
                        if (y < 10) { y_str = "0" + y; } else { y_str = y.ToString(); }
                        for (long x = 0; x < widthOfImage; x++)
                        {
                            offset = y * widthOfImage * 4 + x * 4;
                            blue = pixelByteArray[offset + 0];
                            green = pixelByteArray[offset + 1];
                            red = pixelByteArray[offset + 2];
                            if (blue == 0 && green == 0 && red == 0)//color black
                            {
                                founded++;
                                if (x < 10) { x_str = "0" + x; } else { x_str = x.ToString(); }
                                sb.AppendLine("(" + x_str + "," + y_str + ")");
                                continue;
                            }
                        }
                    }//y
                }//if
                else
                    if (bitmapImage.Format.BitsPerPixel == 8)
                    {
                        for (long y = 0; y < heightOfImage; y++)
                        {
                            if (y < 10) { y_str = "0" + y; } else { y_str = y.ToString(); }
                            for (long x = 0; x < widthOfImage; x++)
                            {
                                offset = y * widthOfImage * 1 + x * 1;
                                color = pixelByteArray[offset];
                                if (color == 0)
                                {
                                    founded++;
                                    if (x < 10) { x_str = "0" + x; } else { x_str = x.ToString(); }
                                    sb.AppendLine("(" + x_str + "," + y_str + ")");
                                    continue;
                                }
                            }
                        }//y
                    }
                    else { MessageBox.Show("File must be:  256-color bitmap OR  24-bits *.bmp bitmap"); }

                ImageModel.NumberOfTxtRow = String.Format("Lines: {0:0}", founded);

                if (founded<Math.Round(((double)widthOfImage)*0.15))
                {
                    MessageBox.Show("Are U sure the image file contains points painted ONLY WITH BLACK?", "Are You Sure?", MessageBoxButton.OK, MessageBoxImage.Question);
                    ImageModel.IsSaveButtonEnabled = false;
                }

                m_MainWindow.textBox1.Text = sb.ToString();
                ImageModel.IsSaveButtonEnabled = true;
            }
        }

        private void bitmapImage_DecodeFailed(object sender, ExceptionEventArgs e)
        {
            ImageModel.IsSaveButtonEnabled = false;
        }

        private void SaveAction(Object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "XY_Coordinates_" + DateTime.Now.Ticks;
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text Files (.txt)|*.txt";
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }    
        }

    }
}
