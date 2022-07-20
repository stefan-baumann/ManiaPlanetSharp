using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ManiaPlanetSharp.GameBox.Parsing.Utils
{
    public static class ParsedValueMappings
    {
        private static Regex timeOfDayMappingRegex = new Regex(@"^((?<Modifier>[A-Za-z]*)(\d+x\d+))?(?!$)(?<Mood>[Ss]unrise|[Ss]unset|[Nn]ight|[Dd]ay|$|^)(\d+x?\d*)?$");
        public static bool TryMapTimeOfDay(string timeOfDay, out string mappedTimeOfDay)
        {
            /* Real Mappings from TMUF:
             * Alpine, Rally:
             *     30x30Sunrise -> Sunrise
             *     30x30 -> Day
             *     30x30Sunset -> Sunset
             *     30x30Night -> Night
             * Speed:
             *     30x30sunrise -> Sunrise
             *     30x30 -> Day
             *     30x30sunset -> Sunset
             *     30x30Night -> Night
             * Alpine, Rally, Speed:
             *     32x32Sunrise -> Sunrise
             *     Simple -> Day
             *     32x32Sunset -> Sunset
             *     32x32Night -> Night
             * Alpine, Rally, Speed:
             *     20x60Sunrise -> Sunrise
             *     20x60 -> Day
             *     20x60Sunset -> Sunset
             *     20x60Night -> Night
             * Alpine, Rally, Speed:
             *     10x150Sunrise -> Sunrise
             *     10x150 -> Day
             *     10x150Sunset -> Sunset
             *     10x150Night -> Night
             * 
             * TMS has a trailing spaces after the regular mood name
             * 
             * Mappings from TM2 (arbritary):
             *      Day48 -> Day
             *      64x64Night -> Night
             *      Sunset48 -> Sunset
             *      Sunrise48 -> Sunrise
             *      64x64Night -> Night
             *      Day -> Day
             * 
             * Mappings from TM3 (arbritary):
             *      48x48Day -> Day
             *      Sunset16x12 -> Sunset
             *      Night16x12 -> Night
             *      NoStadium48x48Day -> Day
             *      NoStadium48x48Night -> Night
             *      etc.
             */

            if (string.IsNullOrWhiteSpace(timeOfDay))
            {
                mappedTimeOfDay = null;
                return false;
            }

            if (timeOfDay.ToLowerInvariant() == "simple")
            {
                mappedTimeOfDay = "Day";
                return true;
            }

            var match = ParsedValueMappings.timeOfDayMappingRegex.Match(timeOfDay);
            if (match.Success)
            {
                if (!match.Groups["Mood"].Success || match.Groups["Mood"].Value.Length == 0)
                {
                    mappedTimeOfDay = "Day";
                    return true;
                }
                mappedTimeOfDay = match.Groups["Mood"].Value;
                mappedTimeOfDay = char.ToUpperInvariant(mappedTimeOfDay[0]) + mappedTimeOfDay.Substring(1); // Make sure to capitalize the first character for consistency
                return true;
            }

            var timeOfDayTrimmed = timeOfDay.Trim();
            if (timeOfDayTrimmed != timeOfDay)
            {
                mappedTimeOfDay = timeOfDayTrimmed;
                return true;
            }

            mappedTimeOfDay = null;
            return false;
        }

        private static Regex mapVehicleStrippingRegex = new Regex(@"^(Vehicles\\)?(?<Vehicle>.*?)(\.Item\.Gbx)?$");
        private static Dictionary<string, string> mapVehicleToSourceEnvironmentMappings = new Dictionary<string, string>()
        {
            //TMUF Mappings
            ["BayCar"] = "Bay",
            ["SnowCar"] = "Snow",
            ["StadiumCar"] = "Stadium",
            //["Rally"] = "Rally",
            ["CoastCar"] = "Coast",
            ["American"] = "Desert",
            ["SportCar"] = "Island",

            //TM2 Default Mappings
            ["CanyonCar"] = "Canyon",
            ["StadiumCar"] = "Stadium",
            ["ValleyCar"] = "Valley",
            ["LagoonCar"] = "Lagoon",

            //TM2 Included TMUF Cars
            ["DesertCar"] = "Desert",
            ["RallyCar"] = "Rally",
            ["IslandCar"] = "Island",
        };
        public static bool TryMapVehicleToSourceEnvironment(string vehicle, out string mappedEnvironment)
        {
            if (string.IsNullOrWhiteSpace(vehicle))
            {
                mappedEnvironment = null;
                return false;
            }

            if (ParsedValueMappings.mapVehicleToSourceEnvironmentMappings.ContainsKey(vehicle))
            {
                mappedEnvironment = ParsedValueMappings.mapVehicleToSourceEnvironmentMappings[vehicle];
                return true;
            }

            //Only try to strip vehicle name if direct match was not successful
            vehicle = ParsedValueMappings.mapVehicleStrippingRegex.Match(vehicle).Groups["Vehicle"].Value;
            if (ParsedValueMappings.mapVehicleToSourceEnvironmentMappings.ContainsKey(vehicle))
            {
                mappedEnvironment = ParsedValueMappings.mapVehicleToSourceEnvironmentMappings[vehicle];
                return true;
            }

            mappedEnvironment = null;
            return false;
        }
    }
}
