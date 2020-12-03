using System;

namespace AdventOfCode.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdventOfCodeAttribute : Attribute
    {
        public int Year { get; }
        public int Day { get; }
        public string Description { get; }
        public object SolutionPart1 { get; }
        public object SolutionPart2 { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year">The Advent Of Code year</param>
        /// <param name="day">The Advent Of Code day</param>
        /// <param name="number">The Advent Of Code Number of the day</param>
        /// <param name="description">Short description of the problem to solve.</param>
        /// <param name="solution">The expected result.</param>
        public AdventOfCodeAttribute(int year, int day, string description, object solution1, object solution2 = null)
        {
            Year = year;
            Day = day;
            Description = description;
            SolutionPart1 = solution1;
            SolutionPart2 = solution2;
        }
    }
}
