using System;

namespace AdventOfCode.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdventOfCodeAttribute : Attribute
    {
        public int Year { get; }
        public int Day { get; }
        public int Number { get; }
        public string Description { get; }
        public long Solution { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year">The Advent Of Code year</param>
        /// <param name="day">The Advent Of Code day</param>
        /// <param name="number">The Advent Of Code Number of the day</param>
        /// <param name="description">Short description of the problem to solve.</param>
        /// <param name="solution">The expected result.</param>
        public AdventOfCodeAttribute(int year, int day, int number, string description, long solution)
        {
            Year = year;
            Day = day;
            Number = number;
            Description = description;
            Solution = solution;
        }
    }
}
