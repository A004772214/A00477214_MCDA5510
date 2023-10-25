MainClass.cs is the main entry for this program, it calls DirWalker.cs class to generate csv file path list, 
calls CSVParser.cs class to parse every csv file, keep the numbers of valid land invalid records and total running time.

DirWalker.cs class walks through whole folder sturcture and returns all csv file paths.

CSVParser.cs class receive csv file path and parse csv file, selects valid records and ignores invalid records, then write
valid records into a new csv file named output.csv with a new column Date that parsed from csv file directory.

CustomerInfo.cs is model class, since column names in csv file and CustomerInfo class defintion are not excatly the same,
ClassMap is used here to define column name with corrsponding class attribute.

Logger.cs class is used to keep log information.