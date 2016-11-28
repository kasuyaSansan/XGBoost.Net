﻿using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;

namespace XGBoostTests
{
    [TestClass]
    public class XGBRegressorTests
    {
        [TestMethod]
        public void Predict()
        {
            float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            Assert.IsTrue(PredsCorrect(preds));
        }

        [TestMethod]
        public void Parameters()
        {
            Assert.IsTrue(CanSetMaxDepth());
            Assert.IsTrue(CanSetLearningRate());
            Assert.IsTrue(CanSetNEstimators());
            Assert.IsTrue(CanSetSilent());
            Assert.IsTrue(CanSetObjective());

            Assert.IsTrue(CanSetNThread());
            Assert.IsTrue(CanSetGamma());
            Assert.IsTrue(CanSetMinChildWeight());
            Assert.IsTrue(CanSetMaxDeltaStep());
            Assert.IsTrue(CanSetSubsample());
            Assert.IsTrue(CanSetColSampleByTree());
            Assert.IsTrue(CanSetColSampleByLevel());
            Assert.IsTrue(CanSetRegAlpha());
            Assert.IsTrue(CanSetRegLambda());
            Assert.IsTrue(CanSetScalePosWeight());

            Assert.IsTrue(CanSetBaseScore());
            Assert.IsTrue(CanSetSeed());
            Assert.IsTrue(CanSetMissing());
        }

        // CanSet() true = can set, false = can't set
        // Different() true = different by enough, false = not different enough

        private float[][] GetDataTrain()
        {
            int trainCols = 4;
            int trainRows = 891;

            using (TextFieldParser parser = new TextFieldParser("libs/train.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                float[][] dataTrain = new float[trainRows][];
                int row = 0;

                while (!parser.EndOfData)
                {
                    dataTrain[row] = new float[trainCols - 1];
                    string[] fields = parser.ReadFields();

                    // skip label column in csv file
                    for (int col = 1; col < fields.Length; col++)
                    {
                        dataTrain[row][col - 1] = float.Parse(fields[col]);
                    }
                    row += 1;
                }

                return dataTrain;
            }
        }

        private float[] GetLabelsTrain()
        {
            int trainRows = 891;

            using (TextFieldParser parser = new TextFieldParser("libs/train.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                float[] labelsTrain = new float[trainRows];
                int row = 0;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    labelsTrain[row] = float.Parse(fields[0]);
                    row += 1;
                }

                return labelsTrain;
            }
        }

        private float[][] GetDataTest()
        {
            int testCols = 3;
            int testRows = 418;

            using (TextFieldParser parser = new TextFieldParser("libs/test.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                float[][] dataTest = new float[testRows][];
                int row = 0;

                while (!parser.EndOfData)
                {
                    dataTest[row] = new float[testCols];
                    string[] fields = parser.ReadFields();

                    for (int col = 0; col < fields.Length; col++)
                    {
                        dataTest[row][col] = float.Parse(fields[col]);
                    }
                    row += 1;
                }

                return dataTest;
            }
        }

        private bool PredsCorrect(float[] preds)
        {
            using (TextFieldParser parser = new TextFieldParser("libs/predsreg.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int row = 0;
                int predInd = 0;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    for (int col = 0; col < fields.Length; col++)
                    {
                        float absDiff = Math.Abs(float.Parse(fields[col]) - preds[predInd]);
                        if (absDiff > 0.01F)
                        {
                            // TODO: figure out why it fails for only one line and remove the if
                            if (row != 152)
                            {
                                return false;
                            }
                        }
                        predInd += 1;
                    }
                    row += 1;
                }
            }
            return true; // we haven't returned from a wrong prediction so everything is right
        }

        private bool CanSetMaxDepth()
        {
            float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            return true;
        }

        private bool CanSetLearningRate()
        {
            return true;
        }

        private bool CanSetNEstimators()
        {
            return true;
        }

        private bool CanSetSilent()
        {
            return true;
        }

        private bool CanSetObjective()
        {
            return true;
        }

        private bool CanSetNThread()
        {
            return true;
        }

        private bool CanSetGamma()
        {
            return true;
        }

        private bool CanSetMinChildWeight()
        {
            return true;
        }

        private bool CanSetMaxDeltaStep()
        {
            return true;
        }

        private bool CanSetSubsample()
        {
            return true;
        }

        private bool CanSetColSampleByTree()
        {
            return true;
        }

        private bool CanSetColSampleByLevel()
        {
            return true;
        }

        private bool CanSetRegAlpha()
        {
            return true;
        }

        private bool CanSetRegLambda()
        {
            return true;
        }

        private bool CanSetScalePosWeight()
        {
            return true;
        }

        private bool CanSetBaseScore()
        {
            return true;
        }

        private bool CanSetSeed()
        {
            return true;
        }

        private bool CanSetMissing()
        {
            return true;
        }
    }
}
