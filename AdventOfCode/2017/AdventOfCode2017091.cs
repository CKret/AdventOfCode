using System.IO;
using AdventOfCode.Core;

namespace AdventOfCode._2017
{
    /// <summary>
    /// --- Day 9: Stream Processing ---
    /// 
    /// A large stream blocks your path. According to the locals, it's not safe to
    /// cross the stream at the moment because it's full of garbage. You look down
    /// at the stream; rather than water, you discover that it's a stream of
    /// characters.
    /// 
    /// You sit for a while and record part of the stream (your puzzle input). The
    /// characters represent groups - sequences that begin with { and end with }.
    /// Within a group, there are zero or more other things, separated by commas:
    /// either another group or garbage. Since groups can contain other groups, a }
    /// only closes the most-recently-opened unclosed group - that is, they are
    /// nestable. Your puzzle input represents a single, large group which itself
    /// contains many smaller ones.
    /// 
    /// Sometimes, instead of a group, you will find garbage. Garbage begins with &lt;
    /// and ends with &gt;. Between those angle brackets, almost any character can
    /// appear, including { and }. Within garbage, &lt; has no special meaning.
    /// 
    /// In a futile attempt to clean up the garbage, some program has canceled some
    /// of the characters within it using !: inside garbage, any character that
    /// comes after ! should be ignored, including &lt;, &gt;, and even another !.
    /// 
    /// You don't see any characters that deviate from these rules. Outside
    /// garbage, you only find well-formed groups, and garbage always terminates
    /// according to the rules above.
    /// 
    /// Here are some self-contained pieces of garbage:
    /// 
    /// 
    ///     - &lt;&gt;, empty garbage.
    ///     - &lt;random characters&gt;, garbage containing random characters.
    ///     - &lt;&lt;&lt;&lt;&gt;, because the extra &lt; are ignored.
    ///     - &lt;{!&gt;}&gt;, because the first &gt; is canceled.
    ///     - &lt;!!&gt;, because the second ! is canceled, allowing the &gt; to terminate
    ///       the garbage.
    ///     - &lt;!!!&gt;&gt;, because the second ! and the first &gt; are canceled.
    ///     - &lt;{o"i!a,&lt;{i&lt;a&gt;, which ends at the first &gt;.
    /// 
    /// Here are some examples of whole streams and the number of groups they
    /// contain:
    /// 
    ///     - {}, 1 group.
    ///     - {{{}}}, 3 groups.
    ///     - {{},{}}, also 3 groups.
    ///     - {{{},{},{{}}}}, 6 groups.
    ///     - {&lt;{},{},{{}}&gt;}, 1 group (which itself contains garbage).
    ///     - {&lt;a&gt;,&lt;a&gt;,&lt;a&gt;,&lt;a&gt;}, 1 group.
    ///     - {{&lt;a&gt;},{&lt;a&gt;},{&lt;a&gt;},{&lt;a&gt;}}, 5 groups.
    ///     - {{&lt;!&gt;},{&lt;!&gt;},{&lt;!&gt;},{&lt;a&gt;}}, 2 groups (since all but the last &gt; are
    ///       canceled).
    /// 
    /// Your goal is to find the total score for all groups in your input. Each
    /// group is assigned a score which is one more than the score of the group
    /// that immediately contains it. (The outermost group gets a score of 1.)
    /// 
    ///     - {}, score of 1.
    ///     - {{{}}}, score of 1 + 2 + 3 = 6.
    ///     - {{},{}}, score of 1 + 2 + 2 = 5.
    ///     - {{{},{},{{}}}}, score of 1 + 2 + 3 + 3 + 3 + 4 = 16.
    ///     - {&lt;a&gt;,&lt;a&gt;,&lt;a&gt;,&lt;a&gt;}, score of 1.
    ///     - {{&lt;ab&gt;},{&lt;ab&gt;},{&lt;ab&gt;},{&lt;ab&gt;}}, score of 1 + 2 + 2 + 2 + 2 = 9.
    ///     - {{&lt;!!&gt;},{&lt;!!&gt;},{&lt;!!&gt;},{&lt;!!&gt;}}, score of 1 + 2 + 2 + 2 + 2 = 9.
    ///     - {{&lt;a!&gt;},{&lt;a!&gt;},{&lt;a!&gt;},{&lt;ab&gt;}}, score of 1 + 2 = 3.
    /// 
    /// What is the total score for all groups in your input?
    /// 
    /// </summary>
    [AdventOfCode(2017, 9, "Stream Processing - Part One", 17537)]
    public class AdventOfCode2017091 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var totalScore = 0;
            var currentScore = 0;
            var isGarbage = false;
            var input = File.ReadAllText("2017\\AdventOfCode201709.txt").ToCharArray();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (c == '!')
                {
                    i++;
                    continue;
                }

                if (isGarbage && c == '>')
                {
                    isGarbage = false;
                    continue;
                }

                if (isGarbage) continue;

                if (c == '<')
                {
                    isGarbage = true;
                    continue;
                }

                if (c == '{')
                {
                    currentScore++;
                    continue;
                }

                if (c == '}')
                {
                    totalScore += currentScore;
                    currentScore--;
                }
            }

            Result = totalScore;
        }

        public AdventOfCode2017091(string sessionCookie) : base(sessionCookie) { }
    }
}
