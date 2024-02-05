// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

// Find current working directory
// Console.WriteLine("Current working Directory:\t" + Directory.GetCurrentDirectory());

// store current directory in a variable
var currentDirectory = Directory.GetCurrentDirectory();
// create a variable of the combined directory of the current directory and the stores directory
var storesDirectory = Path.Combine(currentDirectory, "stores");

// create variable of the combined current directory and a new salesTotalDirectory
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
// Create new directory for the salesTotal
Directory.CreateDirectory(salesTotalDir);

// store the files in the stores directories into a salesFiles variable
var salesFiles = FindFiles(storesDirectory);

// calculate sales total from sales files
var salesTotal = CalculateSalesTotal(salesFiles);

// create a file in the salesTotalDir called toatals.txt
// File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);

// append all text to totals.txt and add new line after each sales total
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");


// ignore
        // write file store file names into console 
        // foreach (var file in salesFiles)
        // {
        //     Console.WriteLine(file);
        // }


// IEnumerable: FindFiles
// return: list of salesFiles
//      type: String
//      contains: file names
IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        // The file name will contain the full path, so only check the end of it
        // if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

//  create a new function that will calculate the sales total. This method should take an IEnumerable<string> of file paths that it can iterate over.
double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record SalesData(double Total);