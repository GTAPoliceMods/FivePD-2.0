// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

namespace FivePD.Common.Models
{
    /// <summary>
    /// A class to hold a date's year, month and day.
    /// </summary>
    public struct Date
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Date"/> struct.
        /// </summary>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        public Date(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
        }

        /// <summary>
        /// Gets the year of the date.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Gets the month of the date.
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// Gets the day of the date.
        /// </summary>
        public int Day { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Year}/{this.Month}/{this.Day}(YMD)";
        }
    }
}