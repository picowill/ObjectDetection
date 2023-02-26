using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public partial class ObjectChecker : Form
    {
        // This list contains the location and file name of all samples; the positive ones begin with p, the negative ones begin with n
        List<string[]> totalSamples = new List<string[]> { };
        protected string objectNameA = "";
        protected string objectNameB = "";

        private void SamplesLocationTxtBoxA_Click(object sender, EventArgs e)
        {
            // Hides the train and upload button, for if the user changes samples
            iterationCountTxtBox.Text = "";
            TrainBtn.Enabled = false;
            UploadBtn.Enabled = false;
            IterationCountLbl.Font = new Font(IterationCountLbl.Font, FontStyle.Regular);

            string[] samplesFilesA = new string[] { };
            loadSamples(samplesFilesA, SamplesLocationTxtBoxA, ref objectNameA);
        }
        private void SamplesLocationTxtBoxB_Click(object sender, EventArgs e)
        {
            // Hides the train and upload button, for if the user changes samples
            iterationCountTxtBox.Text = "";
            TrainBtn.Enabled = false;
            UploadBtn.Enabled = false;
            IterationCountLbl.Font = new Font(IterationCountLbl.Font, FontStyle.Regular);
            
            string[] samplesFilesB = new string[] { };
            loadSamples(samplesFilesB, SamplesLocationTxtBoxB, ref objectNameB);
        }

        private string[] loadSamples(string[] samples, TextBox txtBox, ref string objectname)
        {
            if (samples.Count() > 0)
            {
                Array.Clear(samples);
            }
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    samples = Directory.GetFiles(fbd.SelectedPath);
                    txtBox.Text = fbd.SelectedPath;
                }
                objectname = Path.GetFileName(fbd.SelectedPath);
            }

            // Checks if samples were properly loaded in and if so, adds to the total samples
            if (samples.Length > 0)
            {
                totalSamples.Add(samples);
                CheckSamplesDone();
                TrainBtnVisibility();
            }            

            return samples;
        }

        private void CheckSamplesDone()
        {
            // Checks whether all samples have been uploaded
            // If so, the label will go bold
            switch (currentMode)
            {
                case Mode.Checker:
                    samplesLbl.Font = new Font(samplesLbl.Font, FontStyle.Bold);
                    break;
                case Mode.Recognition:
                    if(totalSamples.Count == 2)
                    {
                        samplesLbl.Font = new Font(samplesLbl.Font, FontStyle.Bold);
                    }
                    break;
            }
        }

        private void TrainBtnVisibility()
        {
            // Determines whether both lots of samples have been uploaded and if so, makes the train button clickable

            if (currentMode == Mode.Checker)
            {
                TrainBtn.Enabled = true;
            }
            else if (currentMode == Mode.Recognition)
            {
                if((SamplesLocationTxtBoxA.Text.Length > 0)&&(SamplesLocationTxtBoxB.Text.Length > 0))
                {
                    TrainBtn.Enabled = true;
                }
                else
                {
                    TrainBtn.Enabled = false;
                }
            }
            else
            {
                TrainBtn.Enabled = false;
            }
        }

        private List<Attribute> ExtractSampleData(double[,] IntegralImageArray, List<Sample> Samples)
        {
            // This is the list that will contain the position, dimensions, type of filter used, and the average intensity difference calculated for each region of the sample
            List<Attribute> sampleAttributes = new List<Attribute>();

            // Create each type of Haar Filter, and a list of all
            List<HaarFilter> HaarFilters = new List<HaarFilter>();
            HorizontalStepA horizontalStepA = new HorizontalStepA(0, 0, 6, 4);
            HaarFilters.Add(horizontalStepA);
            HorizontalStepB horizontalStepB = new HorizontalStepB(0, 0, 6, 4);
            HaarFilters.Add(horizontalStepB);

            // Loop through the sample image and calculate the average intensity difference for each region that is interrogated
            foreach (var filter in HaarFilters)
            {
                int defaultWidth = filter.Width;

                while ((filter.Width < Globals.sampleWidth) && (filter.Height < Globals.sampleHeight))
                {
                    for (int y = 0; y <= Globals.sampleHeight - (filter.Height); y++)
                    {
                        for (int x = 0; x <= Globals.sampleWidth - (filter.Width); x++)
                        {
                            // Setting the coordinates of the filter each time
                            filter.XPos = x + (filter.Width - 1);
                            filter.YPos = y + (filter.Height - 1);
                            double AID = 0;
                            HaarFilterType type = HaarFilterType.undetermined;

                            // Calculate intensity of that region:

                            // Converts the filter to be the right type (as the 'Apply' method is different)
                            if (filter is HorizontalStepA)
                            {
                                HorizontalStepA f = (HorizontalStepA)filter;
                                AID = f.Apply(IntegralImageArray);
                                type = HaarFilterType.HSA;
                            }
                            else if (filter is HorizontalStepB)
                            {
                                HorizontalStepB f = (HorizontalStepB)filter;
                                AID = f.Apply(IntegralImageArray);
                                type = HaarFilterType.HSB;
                            }

                            // Create the attribute and add it to the list of attributes for that sample
                            Attribute newAttribute = new Attribute(filter.XPos, filter.YPos, filter.Width, filter.Height, type, AID);
                            sampleAttributes.Add(newAttribute);
                        }
                    }
                    // sets width back to default once it reaches the image width
                    if ((filter is HorizontalStepA) || (filter is HorizontalStepB))
                    {
                        if (filter.Width >= Globals.sampleWidth - 3)
                        {
                            filter.Width = defaultWidth;
                            filter.Height = filter.Height + 2;
                        }
                        else
                        {
                            filter.Width = filter.Width + 3;
                        }
                    }
                }
            }
            return sampleAttributes;
        }
    }
}