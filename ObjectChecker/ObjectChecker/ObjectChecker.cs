using System.IO;

namespace ObjectChecker
{
    public partial class ObjectChecker : Form
    {
        private Mode _currentmode;
        public Mode currentMode { get => _currentmode; set => _currentmode = value; }
        public ObjectChecker(Mode m)
        {
            InitializeComponent();
            currentMode = m;
        }

        struct result
        {
            public string name;
            public bool validity;
            public double accuracy;
        }

        string uploadedImagePath = "";

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
                imageLbl.Text = "Original Image";
                imageLbl.Show();
                uploadedImagePath = open.FileName;

                // Makes the run button clickable
                RunBtn.Enabled = true;

                // Hides the potential previous output
                resultLbl.Hide();
                accuracyLbl.Hide();
            }
        }

        private void picBox_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if(data != null)
            {
                var fileNames = data as string[];
                if(fileNames.Length > 0)
                {
                    try
                    {
                        if(UploadBtn.Enabled == true)
                        {
                            picBox.Image = Image.FromFile(fileNames[0]);
                            imageLbl.Text = "Original Image";
                            imageLbl.Show();
                            RunBtn.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Please upload samples.");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file type.");
                    }
                }
            }
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Bitmap originalUploadedImage = new Bitmap(picBoxChecker.Image);
                Bitmap originalUploadedImage = (Bitmap)picBox.Image;

                if(originalUploadedImage != null)
                {
                    // Convert the uploaded image to greyscale
                    Bitmap uploadedImage = ConvertToGreyscale(originalUploadedImage);
                    // Resize the uploaded image to match the size of the samples
                    uploadedImage = ResizeImage(uploadedImage, new Size(Globals.imageWidth, Globals.imageHeight));
                    imageLbl.Text = "Edited Image";

                    // Display the uploaded image after the transformations
                    picBox.Image = uploadedImage;

                    // Create an image array and integral image array of the uploaded image
                    double[,] ImageArray = GenerateImageArray(uploadedImage);
                    double[,] IntegralImageArray = GenerateIntegralImageArray(ImageArray, new Size(Globals.imageWidth, Globals.imageHeight));

                    List<result> results = new List<result>();
                    accuracyLbl.Hide();
                    resultLbl.Hide();

                    // Check if any classifiers were made in the training process (the user may not have even clicked train)
                    // If classifiers are present in the list, apply them to the uploaded image and output the results
                    if (ClassifierLists.Count() != 0)
                    {
                        // loop through each set of classifiers (for the different sample sets)
                        foreach (var classifierlist in ClassifierLists)
                        {
                            List<double> AIDresults = CalculateCorrespondingAID(IntegralImageArray, classifierlist);

                            double accuracyPercentage = 0;

                            if ((CheckIfObject(AIDresults, ref accuracyPercentage, classifierlist)) && (accuracyPercentage >= 50))
                            {
                                switch (currentMode)
                                {
                                    case Mode.Checker:
                                        resultLbl.Text = "Valid";
                                        resultLbl.Show();
                                        accuracyLbl.Text = "Accuracy: " + Math.Round(accuracyPercentage, 2).ToString() + "%";
                                        if (displayAccuracyChkBox.Checked)
                                        {
                                            accuracyLbl.Show();
                                        }
                                        break;
                                    case Mode.Recognition:
                                        if (classifierlist == ClassifierLists[0])
                                        {
                                            result newResult = new result();
                                            newResult.name = objectNameA;
                                            newResult.validity = true;
                                            newResult.accuracy = accuracyPercentage;
                                            results.Add(newResult);
                                        }
                                        else
                                        {
                                            result newResult = new result();
                                            newResult.name = objectNameB;
                                            newResult.validity = true;
                                            newResult.accuracy = accuracyPercentage;
                                            results.Add(newResult);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                switch (currentMode)
                                {
                                    case Mode.Checker:
                                        resultLbl.Text = "Invalid";
                                        resultLbl.Show();
                                        accuracyLbl.Text = "Accuracy: " + Math.Round(accuracyPercentage, 2).ToString() + "%";
                                        if (displayAccuracyChkBox.Checked)
                                        {
                                            accuracyLbl.Show();
                                        }
                                        break;
                                    case Mode.Recognition:
                                        if (classifierlist == ClassifierLists[0])
                                        {
                                            result newResult = new result();
                                            newResult.name = objectNameA;
                                            newResult.validity = false;
                                            newResult.accuracy = accuracyPercentage;
                                            results.Add(newResult);
                                        }
                                        else
                                        {
                                            result newResult = new result();
                                            newResult.name = objectNameB;
                                            newResult.validity = false;
                                            newResult.accuracy = accuracyPercentage;
                                            results.Add(newResult);
                                        }
                                        break;
                                }
                            }

                            result winnerResult = new result();
                            if (results.Count > 1) 
                            {
                                if ((results[0].validity) && (!results[1].validity))
                                {
                                    winnerResult = results[0];
                                }
                                else if ((!results[0].validity) && (results[1].validity))
                                {
                                    winnerResult = results[1];
                                }
                                else if (((results[0].validity) && (results[1].validity)) && (results[0].accuracy >= results[1].accuracy))
                                {
                                    winnerResult = results[0];
                                }
                                else if (((results[0].validity) && (results[1].validity)) && (results[0].accuracy <= results[1].accuracy))
                                {
                                    winnerResult = results[1];
                                }

                                if (winnerResult.name != null)
                                {
                                    resultLbl.Text = winnerResult.name;
                                }
                                else
                                {
                                    resultLbl.Text = "Unable to recognise.";
                                }

                                accuracyLbl.Text = "Accuracy: " + Math.Round(winnerResult.accuracy, 2).ToString() + "%";
                                if ((displayAccuracyChkBox.Checked)&&(resultLbl.Text != "Unable to recognise."))
                                {
                                    accuracyLbl.Show();
                                }
                            }
                            resultLbl.Show();
                            // If the checkbox is ticked, output the information to a text file
                            if (outputLogChkBox.Checked)
                            {
                                OutputFile();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry, there are no classifiers. Cannot analyse this image.");
                    }
                }
                else
                {
                    MessageBox.Show("Please upload an image.");
                }
            }
            catch
            {
                MessageBox.Show("Please upload an image.");
            }
        }

        private void OutputFile()
        {
            using (StreamWriter output = new StreamWriter("outputlog.txt"))
            {
                output.Flush();
                output.WriteLine("Object Detection\n================");
                output.WriteLine(DateTime.Now.ToString() + "\n");
                output.WriteLine("Mode: " + currentMode);
                output.WriteLine("\nUploaded image: " + uploadedImagePath);

                if (currentMode == Mode.Checker)
                {
                    output.WriteLine("Samples: " + SamplesLocationTxtBoxA.Text);
                }
                else if (currentMode == Mode.Recognition)
                {
                    output.WriteLine("Samples A: " + SamplesLocationTxtBoxA.Text);
                    output.WriteLine("Samples B: " + SamplesLocationTxtBoxB.Text);
                }

                output.WriteLine("\nResult: " + resultLbl.Text);
                output.WriteLine("\n" + accuracyLbl.Text + " (" + iterationCountTxtBox.Text + " iterations)");
            }
        }

        private List<double> CalculateCorrespondingAID(double[,] IntegralImageArray, List<Classifier> Classifiers)
        {
            // This is a list that will store, in order of the classifiers, the average intensity calculated in the uploaded image
            List<double> results = new List<double>();

            // Go through each classifier and calculate the AID in the uploaded image corresponding to the classifiers xpos, ypos, width, height, type
            foreach (var classifier in Classifiers)
            {

                //MessageBox.Show(classifier.XPos + ", " + classifier.YPos + ", " + classifier.Width + ", " + classifier.Height);

                double AID = 0;

                if (classifier.Type == HaarFilterType.HSA)
                {
                    HorizontalStepA filter = new HorizontalStepA(classifier.XPos, classifier.YPos, classifier.Width, classifier.Height);
                    AID = filter.Apply(IntegralImageArray);
                }
                else if (classifier.Type == HaarFilterType.HSB)
                {
                    HorizontalStepB filter = new HorizontalStepB(classifier.XPos, classifier.YPos, classifier.Width, classifier.Height);
                    AID = filter.Apply(IntegralImageArray);
                }

                results.Add(AID);
            }

            return results;
        }
        private bool CheckIfObject(List<double> AIDresults, ref double accuracyPercentage, List<Classifier> classifierlist)
        {
            // Sums up the amount of say for the classifiers that classify the image as 'valid object'
            // Sums up the amount of say for the classifiers that classify the image as 'invalid object'

            double amountOfSayValid = 0;
            double amountOfSayInValid = 0;

            for (int i = 0; i < AIDresults.Count(); i++)
            {
                if (AIDresults[i] > classifierlist[i].AverageIntensityDifference)
                {
                    amountOfSayValid = amountOfSayValid + classifierlist[i].AmountOfSay;
                }
                else
                {
                    amountOfSayInValid = amountOfSayInValid + classifierlist[i].AmountOfSay;
                }
            }

            // whichever vote has the highest amount of say wins overall
            if (amountOfSayValid > amountOfSayInValid)
            {
                accuracyPercentage = ((double)amountOfSayValid / (double)(amountOfSayValid + amountOfSayInValid)) * (double)100.0;
                return true;
            }
            else if (amountOfSayValid < amountOfSayInValid)
            {
                return false;
            }
            else
            {
                MessageBox.Show("Unable to determine validity.");
                return false;
            }
        }

        private void Setup()
        {
            accuracyLbl.Font = new Font(accuracyLbl.Font, FontStyle.Bold);
            resultLbl.Font = new Font(resultLbl.Font, FontStyle.Bold);

            ObjectCheckerGroupBox.Text = currentMode.ToString();

            TrainBtn.Enabled = false;
            RunBtn.Enabled = false;
            UploadBtn.Enabled = false;
            displayAccuracyChkBox.Enabled = false;
            outputLogChkBox.Enabled = false;

            if (currentMode == Mode.Checker)
            {
                samplesLbl.Location = new Point(8, 34);
                SamplesLocationTxtBoxA.Location = new Point(8, 56);

                SamplesLocationTxtBoxB.Hide();

                IterationCountLbl.Location = new Point(8, 104);
                iterationCountTxtBox.Location = new Point(8, 124);
                TrainBtn.Location = new Point(129, 124);
            }
            if(currentMode == Mode.Recognition)
            {
                samplesLbl.Location = new Point(6, 28);
                SamplesLocationTxtBoxA.Location = new Point(8, 50);
                
                SamplesLocationTxtBoxB.Show();
                SamplesLocationTxtBoxB.Location = new Point(8, 85);

                IterationCountLbl.Location = new Point(8, 116);
                iterationCountTxtBox.Location = new Point(8, 136);
                TrainBtn.Location = new Point(129, 136);
            }

        }

        private void ObjectChecker_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();

            StartScreen start = new StartScreen();
            start.Show();
        }

        private void ObjectChecker_Load(object sender, EventArgs e)
        {
            picBox.AllowDrop = true;
            Setup();
        }

        private void picBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }

    public class Globals
    {
        public const int imageWidth = 24;
        public const int imageHeight = 24;

        public const int sampleWidth = 24;
        public const int sampleHeight = 24;
    }
}