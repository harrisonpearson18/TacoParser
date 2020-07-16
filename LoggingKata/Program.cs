using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            ITrackable tacoBell1 = new TacoBell();
            ITrackable tacoBell2 = new TacoBell();
            double distance = 0;
            var parser = new TacoParser();

            var locations = lines.Select(line => parser.Parse(line)).ToArray();

            for (int i = 0; i < locations.Length; i++)
            {
                var locationA = locations[i];
                var coordA = new GeoCoordinate(locationA.Location.Latitude, locationA.Location.Longitude);

                for (int k = 0; k < locations.Length; k++)
                {
                    var locationB = locations[k];
                    var coordB = new GeoCoordinate(locationB.Location.Latitude, locationB.Location.Longitude);

                    if (coordA.GetDistanceTo(coordB) > distance)
                    {
                        distance = coordA.GetDistanceTo(coordB);
                        tacoBell1 = locationA;
                        tacoBell2 = locationB;

                    }
                }
            }
            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart!");
        }
    }
}