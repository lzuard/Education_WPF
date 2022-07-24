using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using EducationWPF.Models;

namespace EducationWPF.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_DataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if(line.Contains('\"'))
                {
                    int firstEnter = line.IndexOf('\"');
                    int lastEnter = line.LastIndexOf('\"');
                    line = line.Substring(0, firstEnter)
                           + line.Substring(firstEnter, lastEnter).Replace(',', '-')
                           + line.Substring(lastEnter);
                }

                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double Lat, double lon) Place, int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim() ?? String.Empty;
                var country_name = row[1].Trim(' ', '"') ?? String.Empty;
                var latitude = String.IsNullOrEmpty(row[2]) ? 0 : double.Parse(row[2], CultureInfo.InvariantCulture);
                var longitude = String.IsNullOrEmpty(row[2]) ? 0 : double.Parse(row[3], CultureInfo.InvariantCulture);
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                
                yield return (province,country_name,(latitude,longitude),counts);
            }

        }

        public IEnumerable<CountryInfo> GetData()
        {
            var datas = GetDates();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {
                var country = new CountryInfo()
                {
                    Name = country_info.Key,
                    Provinces = country_info.Select(c => new PlaceInfo()
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.lon),
                        Counts = datas.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}
