﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ManiaPlanetSharp.GameBox.Parsing.Utils
{
    public static class ParsedValueMappings
    {
        private static Regex timeOfDayMappingRegex = new Regex(@"^\d+x\d+(?<Mood>[Ss]unrise|[Ss]unset|[Nn]ight)?$");
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
             */

            if (timeOfDay == null)
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

            mappedTimeOfDay = null;
            return false;
        }
    }
}
