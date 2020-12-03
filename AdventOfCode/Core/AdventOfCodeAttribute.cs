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
        /// <param name="description">Short description of the problem to solve.</param>
        /// <param name="solution1">The expected result for part 1.</param>
        /// <param name="solution2">The expected result for part 2.</param>
        public AdventOfCodeAttribute(int year, int day, string description, object solution1 = null, object solution2 = null)
        {
            Year = year;
            Day = day;
            Description = description;
            SolutionPart1 = solution1;
            SolutionPart2 = solution2;
        }
    }
}
