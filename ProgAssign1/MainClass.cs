using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DIrectoryWalker
{
    class MainClass
    {
        static void Main()
        {
            try {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                CsvFileWalker cfw = new CsvFileWalker();
                List<string> csvFiles = cfw.GetCsvFiles("ProgAssign1\\Sample Data\\Sample Data\\");
                string outputFile = "ProgAssign1\\Output\\output.csv";

                string outputFolder = "ProgAssign1\\Output";
                string logFolder = "ProgAssign1\\logs";

                if (!System.IO.Directory.Exists(outputFolder))
                {
                    System.IO.Directory.CreateDirectory(outputFolder);
                }

                if (!System.IO.Directory.Exists(logFolder))
                {
                    System.IO.Directory.CreateDirectory(logFolder);
                }

                if (System.IO.File.Exists(outputFile))
                {
                    System.IO.File.Delete(outputFile);
                }

                string logFilePath = "ProgAssign1\\logs\\log.txt";
                Logger.Initialize(logFilePath);

                int counter = 0;
                int valid_record_count = 0;
                int invalid_record_count = 0;


                foreach (string csvFile in csvFiles)
                {
                    int valid_record_count_per_file = 0;
                    int invalid_record_count_per_file = 0;
                    Console.WriteLine(csvFile);
                    //Logger.LogInfo("Reading " + csvFile);
                    var csvValidator = new CSVValidator(csvFile, outputFile, logFilePath, counter);
                    (valid_record_count_per_file, invalid_record_count_per_file) = csvValidator.ValidateAndWriteValidRecords();

                    counter++;
                    valid_record_count += valid_record_count_per_file;
                    invalid_record_count += invalid_record_count_per_file;

                }

                stopwatch.Stop();

                Logger.LogInfo("Total number of valid records: " + valid_record_count.ToString());
                Logger.LogInfo("Total number of invalid records: " + invalid_record_count.ToString());

                Console.WriteLine("Total number of valid records: " + valid_record_count.ToString());
                Console.WriteLine("Total number of invalid records: " + invalid_record_count.ToString());

                double running_time_in_seconds = stopwatch.Elapsed.TotalSeconds;

                Logger.LogInfo("Total running time in seconds: " + running_time_in_seconds.ToString());
                Console.WriteLine("Total running time in seconds: " + running_time_in_seconds.ToString());

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            };



        }
    }
}
