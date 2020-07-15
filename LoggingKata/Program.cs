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
            ITrackable tB1 = new TacoBell();
            ITrackable tB2 = new TacoBell();
            var parser = new TacoParser();
            var locations = lines.Select(line => parser.Parse(line)).ToArray();

            double distance = 0;
            for (int i = 0; i < locations.Length; i++)
            {
                var locationA = locations[i];
                var coordinateA = new GeoCoordinate();
                coordinateA.Latitude = locationA.Location.Latitude;
                coordinateA.Longitude = locationA.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    var locationB = locations[j];
                    var coordinateB = new GeoCoordinate(locationB.Location.Latitude, locationB.Location.Longitude);

                    if (coordinateA.GetDistanceTo(coordinateB) > distance)
                    {
                        distance = coordinateA.GetDistanceTo(coordinateB);
                        tB1 = locationA;
                        tB2 = locationB;
                    }
                }
            }
            logger.LogInfo($"{tB1.Name} and {tB2.Name} are the farthest apart.");
        }
    }
}
