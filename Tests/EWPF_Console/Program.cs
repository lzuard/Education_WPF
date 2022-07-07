
using System;
using System.Globalization;

namespace EWPF_Console // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient(); 
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                yield return line;
            }
        }

        private static DateTime[] GetDatas() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();



        static void Main(string[] args)
        {
            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDatas();
            
            Console.WriteLine(String.Join("\r\n", dates));

            Console.ReadLine();
        }
    }
}