using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class CsvFileCombiner
{
    public void CombineCsvFiles(string[] inputFilePaths, string outputFilePath)
    {
       
        {
            var writer = new StreamWriter(outputFilePath);
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            
            csvWriter.WriteHeader<MyCsvRecord>(); // Replace MyCsvRecord with your record class
            csvWriter.NextRecord();

            foreach (var inputFilePath in inputFilePaths)
            {
                
                {
                    var reader = new StreamReader(inputFilePath);
                    var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                    csv.Context.RegisterClassMap<CustomerInfoMap>();
                    //csv.Configuration.HeaderValidated = null; // Ignore header validation

                    while (csv.Read())
                    {
                        var record = csv.GetRecord<CustomerInfo>(); // Replace MyCsvRecord with your record class
                        csvWriter.WriteRecord(record);
                        csvWriter.NextRecord();
                    }
                }
            }
        }
    }
}

public class MyCsvRecord
{
    public string Field1 { get; set; }
    public string Field2 { get; set; }
    // Add more properties to match your CSV file structure
}
