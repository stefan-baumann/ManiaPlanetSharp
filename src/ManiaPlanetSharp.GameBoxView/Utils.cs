using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBoxView
{
    public static class Utils
    {
        public static IEnumerable<T> CatchExceptions<T>(this IEnumerable<T> src, Action<Exception> handler = null)
        {
            using (var enumerator = src.GetEnumerator())
            {
                bool next = true;

                while (next)
                {
                    try
                    {
                        next = enumerator.MoveNext();
                    }
                    catch (Exception ex)
                    {
                        handler?.Invoke(ex);
                        continue;
                    }

                    if (next)
                        yield return enumerator.Current;
                }
            }

            yield break;
        }
    }
}
