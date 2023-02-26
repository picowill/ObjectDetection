using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public partial class ObjectChecker : Form
    {
        public Bitmap ConvertToGreyscale(Bitmap originalImage)
        {
            Bitmap Image = new Bitmap(originalImage);
            // Loop through the images pixels and changes it to greyscale
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    Color pixelColour = originalImage.GetPixel(x, y);
                    int r = pixelColour.R;
                    int g = pixelColour.G;
                    int b = pixelColour.B;
                    Color newColour = Color.FromArgb(pixelColour.A, (r + g + b) / 3, (r + g + b) / 3, (r + g + b) / 3);
                    Image.SetPixel(x, y, newColour);
                }
            }
            return Image;
        }
        public Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            // Resize the sample image to given size
            return (Bitmap)(new Bitmap(imgToResize, size));
        }

        private static double[,] GenerateImageArray(Bitmap image)
        {
            double[,] ImageArray = new double[image.Width, image.Height];
            // ADDS THE INTENSITY OF EACH GREYSCALE PIXEL TO THE ARRAY
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;
                    double pixelIntensity = Convert.ToDouble(R / 255); //only uses R because R, G and B are the same - simplified, fixes rounding error
                    ImageArray[x, y] = pixelIntensity;
                }
            }
            return ImageArray;
        }
        private static double[,] GenerateIntegralImageArray(double[,] ImageArray, Size size)
        {
            double[,] IntegralImageArray = new double[size.Width, size.Height];
            string newLine = Environment.NewLine;
            // VERTICAL ADDITION OF INTENSITIES IN IMAGE ARRAY
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    if (y == 0)
                    {
                        IntegralImageArray[x, y] = ImageArray[x, y] + IntegralImageArray[x, y];
                    }
                    else
                    {
                        IntegralImageArray[x, y] = ImageArray[x, y] + IntegralImageArray[x, y - 1];
                    }
                }
            }
            // HORIZONTAL ADDITION OF INTENSITIES IN IMAGE ARRAY
            for (int y = 0; y < size.Width; y++)
            {
                for (int x = 0; x < size.Height; x++)
                {
                    if (x != 0)
                    {
                        IntegralImageArray[x, y] = IntegralImageArray[x, y] + IntegralImageArray[x - 1, y];
                    }
                }
            }
            return IntegralImageArray;
        }
    }
}
