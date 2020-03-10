using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Helper
{
    /// <summary>
    /// This class stores a list of features following the csv (US) schema.
    /// Each feature must be represented using an array of data and a label describing the class of belonging.
    /// The file header could be specified or not.In case of lack of header specification, the class computes a random header.
    /// </summary>
    class CSVFeatureOutput
    {
        private const char IT_SEP = ';';
        private const char US_SEP = ',';
        private const char IT_DOUBLE_SEP = ',';
        private const char US_DOUBLE_SEP = '.';
        private string[] headers;
        private List<string> data;
        private readonly String file;
        private int dataCount;
        public CSVFeatureOutput(String outputFileName)
        {
            this.file = outputFileName;
            this.data = new List<string>();
            this.dataCount = 0;
        }
        /// <summary>
        /// removes all data and header stored in this object.
        /// </summary>
        public void Clear()
        {
            this.headers = null;
            this.data = new List<string>();
        }
        /// <summary>
        /// Add the feature into the feature list. Verify the header correctness.
        /// </summary>
        /// <typeparam name="T">the feature data type</typeparam>
        public void Append <T> (T[] features, int label)
        {
            if(headers != null && features.Length != headers.Length)
            {
                throw new ArgumentException("feature must be consistent to header");
            }
            dataCount = features.Length;
            string featureData = String.Join(IT_SEP + "", features);
            featureData = featureData.Replace(IT_DOUBLE_SEP, US_DOUBLE_SEP); //US rep
            featureData = featureData.Replace(IT_SEP, US_SEP);
            featureData = label + "" + US_SEP + featureData;
            data.Add(featureData);
        }
        /// <summary>
        /// add csv file header, verify the correctness if there is some data stored in the class
        /// </summary>
        /// <param name="headers">an array of label describe each feature dimension</param>
        public void SetHeader(string[] headers)
        {
            if(data.Count != 0 && data[0].Split(US_SEP).Length != headers.Length) 
            {
                throw new ArgumentException("header must be consistent to feature");
            }
            this.headers = headers;
        }
        /// <summary>
        /// Compute the CSV output string and writes it in the file.
        /// </summary>
        public void WriteOnFile()
        {
            if (data.Count == 0)
            {
                data.Add("none");
            }
            var lines = ComputeCSV();
            System.IO.File.WriteAllLines(file, lines);
        }
        /// <summary>
        /// Compute the csv strings from the data appended using Append method.
        /// </summary>
        /// <returns>The string representation of features.</returns>
        public string[] ComputeCSV()
        {
            var headerString = "";
            if (headers == null)
            {
                headerString = FreshHeader();
            }
            else
            {
                headerString = String.Join(US_SEP + "", headers);
            }
            data.Insert(0, headerString);
            return data.ToArray();
        }
        private string FreshHeader()
        {
            return "label," + String.Join(US_SEP + "", Enumerable.Range(0, dataCount));
        }
    }
}
