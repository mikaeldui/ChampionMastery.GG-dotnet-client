using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    public sealed class ChampionMasteryGgException : Exception
    {
        internal ChampionMasteryGgException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
