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
            if (lines.Length == 0) 
            {
                logger.LogError("File has no input");
            
            }


            if (lines.Length == 1) 
            
            {
                logger.LogWarning("file only has one line of input");
            
            }
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            
            ITrackable[] locations = lines.Select(line => parser.Parse(line)).ToArray();

           
            ITrackable tacobell1 = null;
            ITrackable tacobell2 = null;
            double distance = 0;



          
            for (int i = 0; i < locations.Length; i++)


            {


                var locA = locations[i];
                var coar = new GeoCoordinate();
                coar.Latitude = locA.Location.Latitude;
                coar.Longitude = locA.Location.Longitude;

                
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var coarB = new GeoCoordinate();
                    coarB.Latitude = locB.Location.Latitude;
                    coarB.Longitude = locB.Location.Longitude;

                    if (coar.GetDistanceTo(coarB) > distance) 
                    { 
                      distance = coar.GetDistanceTo(coarB);
                      tacobell1 = locA;
                      tacobell2 = locB;
                    
                    }


                }
                


            }

            

            logger.LogInfo($"{tacobell1.Name} and {tacobell2.Name} are the farthest apart ");
        }
    }
}
