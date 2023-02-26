using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ObjectChecker
{
    public partial class ObjectChecker : Form
    {
        public List<List<Classifier>> ClassifierLists;

        private void TrainBtn_Click(object sender, EventArgs e)
        {
            ClassifierLists = new List<List<Classifier>>();

            // Checks if the user has entered an erronious value for the number of iterations (i.e. not an integer)
            // The iteration count is defaulted to 10 if this is the case
            try
            {
                int iterationCount = int.Parse(iterationCountTxtBox.Text);
            }
            catch
            {
                iterationCountTxtBox.Text = "10";
                MessageBox.Show("Invalid iteration count. Defaulted to 10.");
            }

            try
            {
                // there may be 1 or 2 samplelists in totalSamples depending on the mode selected
                foreach (var sampleList in totalSamples)
                {
                    List<Classifier> Classifiers = new List<Classifier>();
                    List<Sample> Samples = new List<Sample>();

                    if (sampleList != null)
                    {
                        // Loops through each sample image
                        for (int i = 0; i < sampleList.Count(); i++)
                        {
                            // Create a Bitmap of the sample image
                            Bitmap sampleImage = new Bitmap(sampleList[i]);

                            sampleImage = ConvertToGreyscale(sampleImage);
                            sampleImage = ResizeImage(sampleImage, new Size(Globals.sampleWidth, Globals.sampleHeight));
                            double[,] ImageArray = GenerateImageArray(sampleImage);
                            double[,] IntegralImageArray = GenerateIntegralImageArray(ImageArray, new Size(Globals.sampleWidth, Globals.sampleHeight));

                            // The AID values are extracted from the image
                            List<Attribute> sampleAttributes = ExtractSampleData(IntegralImageArray, Samples);

                            // Determines whether the sample was a positive sample or a negative sample, and then converts the image to numeric values
                            if (Path.GetFileName(sampleList[i])[0] == 'P')
                            {
                                Sample s = new Sample(sampleAttributes, true, 0);
                                Samples.Add(s);
                            }
                            else
                            {
                                Sample s = new Sample(sampleAttributes, false, 0);
                                Samples.Add(s);
                            }
                        }

                        // Now that all the image samples have been converted to numerical values, AdaBoost can be used
                        // adaboost will output 1 classifier for each iteration of the algorithm

                        AdaBoost(Samples, Classifiers);

                        // Adds the new list of classifiers to a list of lists of classifiers (there will only be a max of 2 in the list because the user can only upload 2 sets of samples)
                        ClassifierLists.Add(Classifiers);
                    }
                    else
                    {
                        MessageBox.Show("Please upload samples");
                    }
                }

                // An error message will be shown if no samples exist. (if the user hasn't uploaded any)
                if(totalSamples.Count() == 0)
                {
                    MessageBox.Show("Cannot train. No samples present.");
                }
                else
                {
                    UploadBtn.Enabled = true;
                    displayAccuracyChkBox.Enabled = true;
                    outputLogChkBox.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Invalid images");
                SamplesLocationTxtBoxA.Text = "";
                SamplesLocationTxtBoxB.Text = "";
                totalSamples.Clear();
                ClassifierLists.Clear();
            }
        }

        private void AdaBoost(List<Sample> Samples, List<Classifier> Classifiers)
        {
            AssignWeights(Samples);

            // This is preparation for the 'actual learning' stage
            // ===================================================

            /* This is the list that will hold the intensities that will be used to classify
             * [the attributes are numeric values, rather than boolean, this must be done]
             * They are stored in the same order as the attributes.
             * [E.g. if attribute 1 was the age of a person, then the first value in the array
             * would be the age to use for the check [ Age < attribute[0] ]]
            */
            List<double> comparisonIntensities = new List<double>();
            List<double> adjacentAverageIntensities = new List<double>();


            for (int count = 0; count < Samples[0].Attributes.Count(); count++) // Each sample has the same number of attributes, so it doesn't matter what the check is here
            {
                // Sort the samples in order of the size of the AID in question, smallest to largest
                Samples = MergeSort(Samples, count);

                //MessageBox.Show("1st: " + Samples[0].Attributes[count].AverageIntensityDifference + ", 2nd: " + Samples[1].Attributes[count].AverageIntensityDifference + ", 3rd: " + Samples[2].Attributes[count].AverageIntensityDifference);

                // Calculate the average AID for all adjacent pairs of samples
                adjacentAverageIntensities.Clear();
                CalculateAdjacentAID(Samples, count, adjacentAverageIntensities);

                // Find the intensity to use by calculating the Gini Impurity for the attribute in question, using the corresponding average adjacent AID's
                double Intensity = FindIntensityToUse(Samples, count, adjacentAverageIntensities);

                // Add the intensity to the list
                comparisonIntensities.Add(Intensity);

                /* It doesn't matter that the samples list will be resorted
                *  as the order of this list only needs to match the order of the attributes in each sample,
                *  not the order of the samples, which dont change
                */
            }
            adjacentAverageIntensities.Clear();


            // This is where the 'actual learning' is done
            
            int iterationCount = int.Parse(iterationCountTxtBox.Text);

            IterationCountLbl.Font = new Font(IterationCountLbl.Font, FontStyle.Bold);

            // This is a list that will contain the positions of the samples in the Samples list that were incorrectly classified
            List<int> incorrectlyClassifiedPositions = new List<int>();
            for (int iteration = 0; iteration < iterationCount; iteration++) // The number of iterations may vary [the number of iterations should be the number of classifiers created]
            {
                // Find out which attribute results in the lowest impurity, as this will be the attribute used in the classifier created
                incorrectlyClassifiedPositions.Clear();

                // Calculate the total error by testing each attribute. The 'winner' will be used for the classifier.
                Classifier newClassifier = CreateClassifier(comparisonIntensities, Samples, incorrectlyClassifiedPositions, Classifiers);

                Classifiers.Add(newClassifier);

                AdjustSampleWeights(Samples, incorrectlyClassifiedPositions, newClassifier);

                NormaliseSampleWeights(Samples);

                // The set of samples then needs to be bootstrapped to make use of the new weights. This bootstrapped dataset replaces the original dataset
                Samples = BootstrapDataset(Samples);
            }

            comparisonIntensities.Clear();
            incorrectlyClassifiedPositions.Clear();
        }
        private void AssignWeights(List<Sample> Samples)
        {
            // Assign equal weights to each sample
            foreach (var sample in Samples)
            {
                sample.SampleWeight = (1 / (double)Samples.Count());
            }
        }
        private void CalculateAdjacentAID(List<Sample> Samples, int count, List<double> adjacentAverageIntensities)
        {
            for (int i = 0; i < Samples.Count() - 1; i++)
            {
                adjacentAverageIntensities.Add((Samples[i].Attributes[count].AverageIntensityDifference + Samples[i + 1].Attributes[count].AverageIntensityDifference) / 2);
            }
        }
        private double FindIntensityToUse(List<Sample> Samples, int count, List<double> adjacentAverageIntensities)
        {
            double lowestGiniImpurity = 2;
            double intensityToUse = 0; // this is the intensity that will be used to classify for this attribute

            foreach (var avg in adjacentAverageIntensities)
            {
                double leftT = 0;
                double leftF = 0;
                double rightT = 0;
                double rightF = 0;

                foreach (var sample in Samples)
                {
                    if ((sample.Attributes[count].AverageIntensityDifference < avg) && (sample.IsObject))
                    {
                        leftT++;
                    }
                    else if ((sample.Attributes[count].AverageIntensityDifference < avg) && (!sample.IsObject))
                    {
                        leftF++;
                    }
                    else if ((sample.Attributes[count].AverageIntensityDifference >= avg) && (sample.IsObject))
                    {
                        rightT++;
                    }
                    else if ((sample.Attributes[count].AverageIntensityDifference >= avg) && (!sample.IsObject))
                    {
                        rightF++;
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }

                double giniImpurityLeft = 1 - Math.Pow(leftT / (leftT + leftF), 2) - Math.Pow(leftF / (leftT + leftF), 2);
                double giniImpurityRight = 1 - Math.Pow(rightT / (rightT + rightF), 2) - Math.Pow(rightF / (rightT + rightF), 2);
                double giniImpurity = (((leftT + leftF) / (leftT + leftF + rightT + rightF)) * giniImpurityLeft) + (((rightT + rightF) / (leftT + leftF + rightT + rightF)) * giniImpurityRight);

                // Check if a purer one has been found
                if (giniImpurity < lowestGiniImpurity)
                {
                    lowestGiniImpurity = giniImpurity;
                    intensityToUse = avg;
                }
            }
            return intensityToUse;
        }
        private Classifier CreateClassifier(List<double> comparisonIntensities, List<Sample> Samples, List<int> incorrectlyClassifiedPositions, List<Classifier> Classifiers)
        {
            double lowestGiniImpurity = 2;
            double intensityToUse = 0; // this is the intensity that will be used to classify for this attribute
            Classifier bestClassifier = new Classifier(23, 23, 24, 24, HaarFilterType.HSA, 0, 0);
            Classifier latestClassifier = bestClassifier;
            double totalError = 0;

            for (int i = 0; i < comparisonIntensities.Count(); i++)
            {
                double leftT = 0;
                double leftF = 0;
                double rightT = 0;
                double rightF = 0;
                totalError = 0;
                intensityToUse = comparisonIntensities[i];
                incorrectlyClassifiedPositions.Clear();

                for (int k = 0; k < Samples.Count(); k++)
                {
                    if ((Samples[k].Attributes[i].AverageIntensityDifference < comparisonIntensities[i]) && (Samples[k].IsObject))
                    {
                        leftT++;
                    }
                    else if ((Samples[k].Attributes[i].AverageIntensityDifference < comparisonIntensities[i]) && (!Samples[k].IsObject))
                    {
                        leftF++;
                        // this means it got it wrong, so need to add to the total error
                        totalError = totalError + Samples[k].SampleWeight;
                        incorrectlyClassifiedPositions.Add(k);
                    }
                    else if ((Samples[k].Attributes[i].AverageIntensityDifference >= comparisonIntensities[i]) && (Samples[k].IsObject))
                    {
                        rightT++;
                        // this means it got it wrong, so need to add to the total error
                        totalError = totalError + Samples[k].SampleWeight;
                        incorrectlyClassifiedPositions.Add(k);
                    }
                    else if ((Samples[k].Attributes[i].AverageIntensityDifference >= comparisonIntensities[i]) && (!Samples[k].IsObject))
                    {
                        rightF++;
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }

                    latestClassifier = new Classifier(Samples[k].Attributes[i].XPos, Samples[k].Attributes[i].YPos, Samples[k].Attributes[i].Width, Samples[k].Attributes[i].Height, Samples[k].Attributes[i].Type, intensityToUse, 0);
                }

                double giniImpurityLeft = 1 - Math.Pow(leftT / (leftT + leftF), 2) - Math.Pow(leftF / (leftT + leftF), 2);
                double giniImpurityRight = 1 - Math.Pow(rightT / (rightT + rightF), 2) - Math.Pow(rightF / (rightT + rightF), 2);
                double giniImpurity = (((leftT + leftF) / (leftT + leftF + rightT + rightF)) * giniImpurityLeft) + (((rightT + rightF) / (leftT + leftF + rightT + rightF)) * giniImpurityRight);

                // check if it has found a purer one
                if (giniImpurity < lowestGiniImpurity)
                {
                    if (CheckIfAlreadyClassifier(latestClassifier, Classifiers))
                    {
                        bestClassifier = latestClassifier;
                        lowestGiniImpurity = giniImpurity;
                    }
                }
            }

            // Calculate the amount of say which will be used to determine which classifiers are the 'most trusted'
            CalculateAmountOfSay(totalError, bestClassifier);

            return bestClassifier;
        }
        private bool CheckIfAlreadyClassifier(Classifier latestClassifier, List<Classifier> Classifiers)
        {
            bool validity = true;
            foreach(var c in Classifiers)
            {
                if ((latestClassifier.XPos == c.XPos)&&(latestClassifier.YPos == c.YPos)&&(latestClassifier.Width == c.Width)&&(latestClassifier.Height == c.Height))
                {
                    validity = false;
                    break;
                }
            }
            return validity;
        }
        private void CalculateAmountOfSay(double totalError, Classifier classifier)
        {
            double amountOfSay = 0.5 * Math.Log((1 - totalError) / totalError);
            classifier.AmountOfSay = amountOfSay;
        }

        private void AdjustSampleWeights(List<Sample> Samples, List<int> incorrectlyClassifiedPositions, Classifier newClassifier)
        {
            // Adjust the weight of each sample to make the next classification better

            List<Sample> temp = Samples;
            // For simplicity, it first adjusts the weight of ALL samples as if they ALL classified incorrectly
            foreach (var sample in Samples)
            {
                sample.SampleWeight = sample.SampleWeight * Math.Pow((Math.E), (newClassifier.AmountOfSay) * -1);
            }
            // Then it adjusts just the correctly classified samples
            for (int i = 0; i < incorrectlyClassifiedPositions.Count(); i++)
            {
                Samples[incorrectlyClassifiedPositions[i]].SampleWeight = temp[incorrectlyClassifiedPositions[i]].SampleWeight * Math.Pow((Math.E), (newClassifier.AmountOfSay));
            }
        }
        private void NormaliseSampleWeights(List<Sample> Samples)
        {
            // Normalise the weights so that they all sum up to 1

            double total = 0;
            // Find the current total
            foreach (var sample in Samples)
            {
                total = total + sample.SampleWeight;
            }
            // Divide each by the total to make the total sum to 1
            foreach (var sample in Samples)
            {
                sample.SampleWeight = sample.SampleWeight / total;
            }
        }
        private List<Sample> BootstrapDataset(List<Sample> Samples)
        {
            Random rnd = new Random();

            List<Sample> BootstrapSamples = new List<Sample>();

            // This will take random samples from the original samples dataset, using the weight to determine which sample should be added
            while (BootstrapSamples.Count < Samples.Count)
            {
                double randomDouble = rnd.NextDouble();
                double lower = 0;
                double higher = 0;
                foreach (var sample in Samples)
                {
                    higher = higher + sample.SampleWeight;
                    if ((randomDouble <= higher) && (randomDouble > lower))
                    {
                        BootstrapSamples.Add(sample);
                    }
                    lower = higher;
                }
            }

            // Deletes all of the samples that were added to the bootstrapped samples list from the original samples list
            foreach (var sample in BootstrapSamples)
            {
                Samples.Remove(sample);
            }

            // Deletes duplicates
            for (int i = 0; i < BootstrapSamples.Count(); i++)
            {
                for (int j = 0; j < BootstrapSamples.Count(); j++)
                {
                    if((BootstrapSamples[i] == BootstrapSamples[j]) && (i != j))
                    {
                        BootstrapSamples.Remove(BootstrapSamples[j]);
                        i = 0;
                        j = 0;
                    }
                }
            }

            AssignWeights(BootstrapSamples);

            // Adds the missing samples back again
            foreach (var sample in Samples)
            {
                BootstrapSamples.Add(sample);
            }

            NormaliseSampleWeights(BootstrapSamples);

            return BootstrapSamples;
        }
    }
}
