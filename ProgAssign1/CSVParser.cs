using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class CSVValidator
{
    private string inputFilePath;
    private string outputFilePath;
    private string logFilePath;
    private int counter;
    private int valid_record_count;
    private int invalid_record_count;

    public CSVValidator(string inputFilePath, string outputFilePath, string logFilePath, int counter)
    {
        this.inputFilePath = inputFilePath;
        this.outputFilePath = outputFilePath;
        this.logFilePath = logFilePath;
        this.counter = counter;
        this.valid_record_count = valid_record_count;
        this.invalid_record_count = invalid_record_count;
    }

    public (int valid_record_count, int invalid_record_count) ValidateAndWriteValidRecords()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);
        config.BadDataFound = context =>
        {
            LogException(context);
        };

        string formatted_date = ParseFilePathToDate(inputFilePath);
        
        using (var reader = new StreamReader(inputFilePath))
        using (var csv = new CsvReader(reader, config))
        using (var writer = new StreamWriter(outputFilePath, append: true))
        using (var csvWriter = new CsvWriter(writer, config))


        {
            csv.Context.RegisterClassMap<CustomerInfoMap>();

            if (counter < 1)
            {
                csvWriter.WriteHeader<CustomerInfo>();
                csvWriter.NextRecord();
                
            }
                       
            
            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<CustomerInfo>();
                    record.Date = formatted_date;

                    if (IsValidRecord(record))
                    {
                        csvWriter.WriteRecord(record);
                        csvWriter.NextRecord();
                        csvWriter.Flush();
                        valid_record_count ++;
                        
                    }
                    else 
                    {
                        invalid_record_count++;
                        Logger.LogInfo("Invalid record is skipped");

                        Console.WriteLine("Invalid record is skipped");
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }
            
        }
        return (valid_record_count, invalid_record_count);
    }



    private void LogException(BadDataFoundArgs context)
    {
        Logger.LogError(context.ToString());
    }

    private void LogException(Exception exception)
    {
        Logger.LogError(exception.Message);
        //File.AppendAllText(logFilePath, $"{DateTime.Now} ERROR: {exception.Message}{Environment.NewLine}");
    }

    private bool IsValidRecord(CustomerInfo record)
    {
        if (record == null)
            return false;

        foreach (var property in record.GetType().GetProperties())
        {
            var value = (string)property.GetValue(record);
            if (string.IsNullOrWhiteSpace(value))
                return false;
        }

        return true;
    }

    static string ParseFilePathToDate(string filePath)
    {
        string[] pathParts = filePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        string year = pathParts[pathParts.Length - 4];
        string month = pathParts[pathParts.Length - 3];
        string day = pathParts[pathParts.Length - 2];

        string formattedDate = $"{year}/{month.PadLeft(2, '0')}/{day.PadLeft(2, '0')}";

        return formattedDate;
    }

}
