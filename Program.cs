using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

public class CodingTask
{
    public static void Dijkstra(int[,] matrix, int start, int lenght, string input)
    {
        int[] distance          = new int[lenght];
        bool[] shortestPath     = new bool[lenght];

        for (int i = 0; i < lenght; ++i)
        {
            distance[i]         = int.MaxValue;
            shortestPath[i]     = false;
        }

        distance[start] = 0;

        for (int count = 0; count < lenght - 1; ++count)
        {
            int i               = MinimumDistance(distance, shortestPath, lenght);
            shortestPath[i]     = true;

            for (int j = 0; j < lenght; ++j)
            {
                if (!shortestPath[j] && Convert.ToBoolean(matrix[i, j]) && distance[i] != int.MaxValue && (distance[i] + matrix[i, j]) < distance[j])
                {
                    distance[j] = distance[i] + matrix[i, j];
                }
            }
        }
        WriteToFile(distance, lenght, input);
    }

    public static void WriteToFile(int[] distance, int lenght, string input)
    {
        string line;
        
        if (Reachable(distance, lenght))
        {
            line = input + "--> The last element is reachable" + "\n";
            Console.WriteLine("The last element is reachable");
        }
        else
        {
            line = input + "--> The last element isn't reachable" + "\n";
            Console.WriteLine("The last element isn't reachable");
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\WriteLines.txt", true))
        {
            file.WriteLine(line);
        }
    }

    public static void ReadFromFile()
    {
        string[] lines = System.IO.File.ReadAllLines(@".\WriteLines.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }

    public static void ReadFromFile(string numbers)
    {
        bool observed           = false;
        string observedLine     = "";

        string[] lines          = System.IO.File.ReadAllLines(@".\WriteLines.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Split("-->");

            if(line[0] == numbers)
            {
                observed        = true;
                observedLine    += "Result is already calculated. Result: " + line[1];
            }
        }

        char[] delimiterChars   = { ' ', ',', '.', ':' };
        string[] arr            = numbers.Split(delimiterChars);
        bool rightRegex         = false;

        if (observed) 
        { 
            Console.WriteLine(observedLine); 
        }
        else 
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (!Regex.IsMatch(arr[i], @"^\d"))
                {
                    rightRegex = true;
                }
            }
            if (rightRegex)
            {
                Console.WriteLine("Given array isn't numeric");
            }
            else
            {
                Converter(numbers);
                Console.WriteLine("Result was not calculated before, but now result is in the file.");
            }
        }
    }

public static int MinimumDistance(int[] distance, bool[] shortestPath, int lenght)
    {
        int min         = int.MaxValue;
        int minIndex    = 0;

        for (int i = 0; i < lenght; ++i)
        {
            if (shortestPath[i] == false && distance[i] <= min)
            {
                min         = distance[i];
                minIndex    = i;
            }
        }

        return minIndex;
    }

    public static bool Reachable(int[] distance, int lenght)
    {
        if (distance[lenght - 1] >= 2147483646)
        {
            return false;
        }
        else return true;
    }

    public static void InitiateMatrix(int[,] matrix, int lenght)
    {
        int i, j;

        for (i = 0; i < lenght; i++)
        {
            for (j = 0; j < lenght; j++)
            {
                matrix[i, j] = 0;
            }
            matrix[i, i] = 1;
        }
    }

    public static void ArrayToMatrix(int[,] matrix, int[] array, int lenght)
    {
        int i, j;

        for (i = 0; i < lenght; i++)
        {
            int x = array[i];
            if (x > 0)
            {
                for (j = i; (j <= i + x) && (j < lenght); j++)
                {
                    matrix[i, j] = 1;
                }
            }
            else
            {
                for (j = i; (j >= i + x) && (j >= 0); j--)
                {
                    matrix[i, j] = 1;
                }
            }
        }
    }

    public static void Converter(string input)
    {
        int i, lenght;

        char[] delimiterChars   = { ' ', ',', '.', ':' };
        string[] numbers        = input.Split(delimiterChars);

        lenght      = numbers.Length;
        int[] array = new int[lenght];

        for (i = 0; i < lenght; i++)
        {
            Int32.TryParse(numbers[i], out array[i]);
        }

        int[,] matrix = new int[lenght, lenght];

        InitiateMatrix(matrix, lenght);
        ArrayToMatrix(matrix, array, lenght);
        Dijkstra(matrix, 0, lenght, input);
    }

    public static void Main(string[] args)
    {
        bool condition = true;

        while(condition)
        {
            Console.Write("Menu:\n0 - finish command \n1 - check, if array is reachable \n2 - print calculated result \n3 - print all records\n");
            string input = Console.ReadLine();
            switch (input)
            {
                case ("0"):
                    condition = false;
                    break;
                case ("1"):
                    Console.Write("The numbers of array:\n");
                    input = Console.ReadLine();
                    ReadFromFile(input);
                    break;
                case ("2"):
                    Console.Write("The numbers of array:\n");
                    input = Console.ReadLine();
                    ReadFromFile(input);
                    break;
                case ("3"):
                    ReadFromFile();
                    break;
            }
        }
    }
}