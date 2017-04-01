using ManiaPlanetSharp.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.ManiaExchange
{
    /// <summary>
    /// Provides methods for accessing data stored on ManiaExchange (http://mania-exchange.com).
    /// </summary>
    public static class ManiaExchange
    {
        #region Official API Methods

        /// <summary>
        /// Fetches information about a track specified by it's MX ID.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        /// <returns>A <see cref="TrackInfo"/> instance with all of the information that is provided for the track by ManiaExchange.</returns>
        /// <exception cref="Exception">Internal error while trying to retrieve the track information.</exception>
        public static async Task<TrackInfo> GetTrackInfo(int trackId)
        {
            try
            {
                return await WebUtils.FetchJsonObject<TrackInfo>($"https://api.mania-exchange.com/tm/maps/{trackId}");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error while trying to retrieve the track information.", ex);
            }
        }

        /// <summary>
        /// Fetches information about a track's world record replay.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        /// <returns>A <see cref="ReplayInfo"/> instance with all of the information that is provided about the world record replay by ManiaExchange.</returns>
        /// <exception cref="Exception">Internal error while trying to retrieve the track information.</exception>
        public static async Task<ReplayInfo> GetWorldRecord(int trackId)
        {
            try
            {
                return await WebUtils.FetchJsonObject<ReplayInfo>($"https://api.mania-exchange.com/tm/tracks/worldrecord/{trackId}");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error while trying to retrieve the track information.", ex);
            }
        }

        /// <summary>
        /// Fetches a list with information about all of the replays submitted on the specified track.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        /// <returns>A <see cref="List<ReplayInfo>"/> with information about all of the replays submitted on ManiaExchange.</returns>
        /// <exception cref="Exception">Internal error while trying to retrieve the track information.</exception>
        public static async Task<List<ReplayInfo>> GetReplays(int trackId)
        {
            try
            {
                return await WebUtils.FetchJsonObject<List<ReplayInfo>>($"https://api.mania-exchange.com/tm/replays/{trackId}");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error while trying to retrieve the track information.", ex);
            }
        }

        /// <summary>
        /// Fetches a list with information about all of the objects embedded in the specified track.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        /// <returns>A <see cref="List<ObjectInfo>"/> with information about all of the objects embedded in the specified track.</returns>
        /// <exception cref="Exception">Internal error while trying to retrieve the track information.</exception>
        public static async Task<List<ObjectInfo>> GetEmbeddedObjects(int trackId)
        {
            try
            {
                return await WebUtils.FetchJsonObject<List<ObjectInfo>>($"https://api.mania-exchange.com/tm/tracks/embeddedobjects/{trackId}");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error while trying to retrieve the track information.", ex);
            }
        }

        /// <summary>
        /// Returns the url of the ManiaExchange screenshot of the specified track.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetScreenshotUrl(int trackId)
        {
            return $"https://tm.mania-exchange.com/tracks/screenshot/normal/{trackId}";
        }

        /// <summary>
        /// Returns the url of the thumbnail of the specified track.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetIngameThumbnailUrl(int trackId)
        {
            return $"https://tm.mania-exchange.com/tracks/thumbnail/{trackId}";
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Returns the url of the ManiaExchange track page for the specified track.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetTrackPageUrl(int trackId)
        {
            return $"https://tm.mania-exchange.com/tracks/{trackId}";
        }

        /// <summary>
        /// Returns the url which can be used to download the specified track from ManiaExchange.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetDownloadUrl(int trackId)
        {
            return $"https://tm.mania-exchange.com/tracks/download/{trackId}";
        }

        /// <summary>
        /// Returns the url which can be used to download the specified track from ManiaExchange via the ingame map downloader.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetInstallUrl(int trackId)
        {
            return $"maniaplanet:///:mx:download?id={trackId}";
        }

        /// <summary>
        /// Returns the url which can be used to download a specific replay from ManiaExchange.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetReplayUrl(int replayId)
        {
            return $"https://tm.mania-exchange.com/replays/download/{replayId}";
        }

        /// <summary>
        /// Returns the url to the profile of a specific user from ManiaExchange.
        /// </summary>
        /// <param name="trackId">The ManiaExchange ID.</param>
        public static string GetUserProfileUrl(int userId)
        {
            return $"https://tm.mania-exchange.com/user/profile/{userId}";
        }

        #endregion
    }
}
