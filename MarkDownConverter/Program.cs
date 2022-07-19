namespace MarkDownConverter;

public class Program
{
    //Main program taking input file in --path and write result to path in --out
    public static void Main(string[] args)
    {
        //Checking basic arugment length. Assuming proper file name and argument names provided
        if(args.Length == 0)
        {
            Console.Error.WriteLine("File path is not provided! Provide full file path argument with '--path fileName --out outPutPathName' format");
        }
        else
        {
            var inputFile = args[1];
            var outputFile = args[3];
            if (args[0].ToLower() == "--path" && !string.IsNullOrEmpty(args[1]))
            {
                if (File.Exists(inputFile))
                {
                    //Enter main program
                    MarkdownConverter converter = new MarkdownConverter();

                    var lines = File.ReadLines(inputFile);
                    var convertedHtml = converter.Parse(lines);

                    File.WriteAllText(outputFile, convertedHtml);
                }
                else
                {
                    Console.Error.WriteLine("Path is not correct!");
                }
            }
        }
    }
}

