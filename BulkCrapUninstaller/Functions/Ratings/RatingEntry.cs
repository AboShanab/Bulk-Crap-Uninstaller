using System;
using BulkCrapUninstaller.Properties;

namespace BulkCrapUninstaller.Functions.Ratings
{
    public struct RatingEntry : IEquatable<RatingEntry>
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (AverageRating.GetHashCode()*397) ^ MyRating.GetHashCode();
            }
        }

        public string ApplicationName { get; set; }
        public int? AverageRating { get; set; }
        public int? MyRating { get; set; }
        public bool IsEmpty => ApplicationName == null && !AverageRating.HasValue && !MyRating.HasValue;
        public static RatingEntry Empty { get; } = default(RatingEntry);

        public static RatingEntry NotAvailable { get; } = new RatingEntry
        {
            AverageRating = null,
            MyRating = null,
            ApplicationName = Localisable.NotAvailable
        };

        public bool Equals(RatingEntry other)
        {
            return AverageRating == other.AverageRating && MyRating == other.MyRating;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RatingEntry && Equals((RatingEntry) obj);
        }

        public static UninstallerRating ToRating(int val)
        {
            if (val <= ((int) UninstallerRating.Bad + (int) UninstallerRating.Neutral)/2)
                return UninstallerRating.Bad;
            if (val >= ((int) UninstallerRating.Good + (int) UninstallerRating.Neutral)/2)
                return UninstallerRating.Good;

            return UninstallerRating.Neutral;
        }

        public override string ToString()
        {
            return string.Format(Localisable.RatingEntry_ToString,
                AverageRating.HasValue ? ToRating(AverageRating.Value) : UninstallerRating.Unknown,
                MyRating.HasValue ? ToRating(MyRating.Value) : UninstallerRating.Unknown);
        }
    }
}