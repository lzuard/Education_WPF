﻿using System;

namespace EWPF_Console // Note: actual namespace depends on the project name.
{
    internal class Programm
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var response = client.GetAsync(data_url).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;




            Console.ReadLine();
        }
    }
}