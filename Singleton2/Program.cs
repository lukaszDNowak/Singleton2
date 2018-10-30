using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Zapis info do pliku");
                SimpleLogger.Instance(@"D:/Logs").Info("informacja");
                Console.WriteLine("Rzucamy wyj�tkiem");
                throw new Exception("Straszny b��d");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Zapis b��du do pliku");
                SimpleLogger.Instance(@"D:/Logs").Error(ex.Message);
            }
            Console.Read();
        }

    }
}

    internal class SimpleLogger
    {
    private static SimpleLogger instance;
    public static string filePath;
    private static List<string> strings = null;
    private SimpleLogger() { }
    public static SimpleLogger Instance(string _filePath)
    {
        filePath = _filePath;
        return instance == null ? instance = new SimpleLogger() : instance;
    }
    internal void Info(string messege)
    {
        SaveToFile(messege);
    }
    internal void Error(string error)
    {
        SaveToFile(error);
    }
    public virtual void SaveToFile(string messege)
    {
        if (File.Exists(filePath))
        {
            strings = File.ReadAllLines(filePath).ToList();
        }
        else
            strings = new List<string>();

        strings.Add(Convert.ToString(DateTime.Now) + "_" + messege);
        File.WriteAllLines(filePath, strings.ToArray());
    }
}

