using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeetingProject.SupportiveClasses
{
    internal class ImageConverter
    {
        public static byte[] ConvertToByteCollection(string uri)
        {
            BitmapImage image = new BitmapImage(new Uri($"{uri}", UriKind.Relative));
            MemoryStream memoryStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memoryStream);
            return memoryStream.ToArray();
        }

        public static byte[] OpenFileDialogSave()
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "All Files (*.*)|*.*"
            };

            if (openFile.ShowDialog().GetValueOrDefault())
            {
                BitmapFrame.Create(new MemoryStream(File.ReadAllBytes(openFile.FileName)), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return File.ReadAllBytes(openFile.FileName);
            }
            return null;
        }

        public static ImageSource ConvertToImageSourse(byte[] bytes)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(bytes);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource image = biImg as ImageSource;
            return image;
        }
    }
}
