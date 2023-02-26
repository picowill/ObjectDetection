using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectChecker
{
    public partial class DeveloperModeForm : Form
    {
        public DeveloperModeForm()
        {
            InitializeComponent();
        }

        private void DeveloperModeForm_Load(object sender, EventArgs e)
        {
            picBox.AllowDrop = true;
            developerModeGrid.Hide();
            Setup();
        }

        private void DeveloperModeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StartScreen start = new StartScreen();
            start.Show();
        }

        private void picBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void picBox_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                {
                    try
                    {
                        picBox.Image = Image.FromFile(fileNames[0]);
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file type.");
                    }
                }
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            double[,] ImageArray;
            switch (devlopermodeComboBox.SelectedIndex)
            {
                case 1:
                    developerModeGrid.Hide();
                    picBox.Image = ResizeImage((Bitmap)picBox.Image, new Size(24,24));
                    break;
                case 2:
                    developerModeGrid.Hide();
                    picBox.Image = ConvertToGreyscale((Bitmap)picBox.Image);
                    break;
                case 3:
                    picBox.Image = ResizeImage((Bitmap)picBox.Image, new Size(24, 24));
                    picBox.Image = ConvertToGreyscale((Bitmap)picBox.Image);
                    ImageArray = GenerateImageArray((Bitmap)picBox.Image);
                    DisplayArrayInGrid(ImageArray);
                    break;
                case 4:
                    picBox.Image = ResizeImage((Bitmap)picBox.Image, new Size(24, 24));
                    picBox.Image = ConvertToGreyscale((Bitmap)picBox.Image);
                    ImageArray = GenerateImageArray((Bitmap)picBox.Image);
                    double[,] IntegralImageArray = GenerateIntegralImageArray(ImageArray, (Bitmap)picBox.Image);
                    DisplayArrayInGrid(IntegralImageArray);
                    break;
            }
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            return (Bitmap)(new Bitmap(imgToResize, size));
        }

        private Bitmap ConvertToGreyscale(Bitmap originalImage)
        {
            Bitmap Image = new Bitmap(originalImage);
            // Loop through the images pixels and changes to greyscale
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    Color pixelColour = originalImage.GetPixel(x, y);
                    int r = pixelColour.R;
                    int g = pixelColour.G;
                    int b = pixelColour.B;
                    Color newColour = Color.FromArgb(pixelColour.A, (r + g + b) / 3, (r + g + b) / 3, (r + g + b) / 3);
                    Image.SetPixel(x, y, newColour); // Now greyscale
                }
            }
            return Image;
        }
        private void DisplayArrayInGrid(double[,] imageArray)
        {
            developerModeGrid.Show();

            Random rnd = new Random();
            for (int i = 0; i < 24; i++)
            {
                DataGridViewColumn column = developerModeGrid.Columns[i];
                column.Width = 40;
            }

            for (int i = 0; i < imageArray.GetLength(0); i++)
            {
                string[] imagearrayrow = new string[imageArray.GetLength(1)];

                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    imagearrayrow[j] = Convert.ToString(imageArray[i, j]);
                }
                developerModeGrid.Rows.Add(imagearrayrow);
            }
        }
        private static double[,] GenerateImageArray(Bitmap image)
        {
            double[,] ImageArray = new double[image.Width, image.Height];
            // Adds the intensity of each greyscale pixel to the array
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    double R = pixel.R;
                    double pixelIntensity = Convert.ToDouble(R / 255); //only uses R because R, G and B are the same - simplified, fixes rounding error
                    ImageArray[x, y] = pixelIntensity;
                }
            }
            return ImageArray;
        }
        private double[,] GenerateIntegralImageArray(double[,] ImageArray, Bitmap image)
        {
            double[,] IntegralImageArray = new double[image.Width, image.Height];
            string newLine = Environment.NewLine;
            // VERTICAL ADDITION OF INTENSITIES IN IMAGE ARRAY
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
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
            for (int y = 0; y < image.Width; y++)
            {
                for (int x = 0; x < image.Height; x++)
                {
                    if (x != 0)
                    {
                        IntegralImageArray[x, y] = IntegralImageArray[x, y] + IntegralImageArray[x - 1, y];
                    }
                }
            }
            return IntegralImageArray;
        }

        private void UploadBtn_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();

            open.Multiselect = false;

            // image filters  
            open.Filter = "Image Files(*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                picBox.Image = new Bitmap(open.FileName);

                // Makes the run button clickable
                startBtn.Enabled = true;
            }
        }
        public void Setup()
        {
            startBtn.Enabled = false;
            devlopermodeComboBox.SelectedIndex = 0;
        }

        
    }
}
