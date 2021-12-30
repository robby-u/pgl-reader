#pragma warning disable IDE0060 // remove unused parameter
using System;
using System.IO;
using System.Net;
using System.Text;

namespace PGL_Reader
{
    public class Program
    {
        private static string pglibraries = "http://url.here";

        static void Main(string[] args)
        {
            while (true)
            {
                Title();

                Console.WriteLine("Enter a URL / sublink to begin scan: ");
                Console.Write(" + ");

                string url = Console.ReadLine();
                if (url.Length > 2)
                    try
                    {
                        URLToFind(url);
                    } 
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught! {0}\n{1}", ex.Message, ex.StackTrace);
                    }
                else Console.WriteLine("No URL entered!");

                Console.WriteLine("Press ENTER to start another scan.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void Title()
        {
            Console.WriteLine(@"    ___      _            _       ");
            Console.WriteLine(@"   / _ \_ __(_)_   ____ _| |_ ___ ");
            Console.WriteLine(@"  / /_)/ '__| \ \ / / _` | __/ _ \");
            Console.WriteLine(@" / ___/| |  | |\ V / (_| | ||  __/");
            Console.WriteLine(@" \/ ___|_|  |_| \_/ \__,_|\__\___|");
            Console.WriteLine(@"   / _ \__ _ _ __ __| | ___ _ __  ");
            Console.WriteLine(@"  / /_\/ _` | '__/ _` |/ _ \ '_ \ ");
            Console.WriteLine(@" / /_\\ (_| | | | (_| |  __/ | | |");
            Console.WriteLine(@" \____/\__,_|_|  \__,_|\___|_| |_|");
            Console.WriteLine("");
            Console.WriteLine("           Library Reader");
            Console.WriteLine("");
        }

        private static void URLToFind(string URL)
        {
            string activeLibrary = string.Empty;
            int count = 0;

            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(pglibraries))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true))
            {
                string line;

                // read until it gets to the end of file
                while ((line = reader.ReadLine()) != null)
                {
                    switch(line.Substring(0, 1))
                    {
                        case "@":
                            activeLibrary = line.Substring(1);
                            continue;

                        case "+":
                            if(line.Contains(URL))
                            {
                                count++;
                                Console.WriteLine("{0}. {1}", count, activeLibrary);
                            }
                            continue;

                        default:
                            break;
                    }
                }

                Console.WriteLine("Total of {0} users with sublink: {1}", count, URL);
            }
        }
    }
}
#pragma warning restore IDE0060 // remove unused parameter
