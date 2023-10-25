using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CsvFileWalker
{
    public List<string> GetCsvFiles(string folderPath)
    {
        var csvFiles = new List<string>();
        Walk(folderPath, csvFiles);
        return csvFiles;
    }

    private void Walk(string path, List<string> csvFiles)
    {
        try
        {
            foreach (string file in Directory.GetFiles(path, "*.csv"))
            {
                csvFiles.Add(file);
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                Walk(directory, csvFiles);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while walking through the folder: " + e.Message);
           
        }
    }
}



