using EightQueens;
using System.Diagnostics;

namespace EightQueensProblem
{
    internal class Program
    {
        /// <summary>
        /// Try walks over the tree of valid board states without physically
        /// constructing the tree.
        /// </summary>
        /// <param name="all">The number of queens, initially.</param>
        /// <param name="ld">Contains a one for each position attacked along a
        /// left going diagonal.</param>
        /// <param name="cols">A bit pattern containing a one for each column
        /// that is already occupied.</param>
        /// <param name="rd">Contains a one for each position attacked along a
        /// right going diagonal.</param>
        static int Try(int all, int ld, int cols, int rd)
        {
            int count = 0;
            all = ld != 0 ? all : (1 << all) - 1;
            int poss = ~(ld | cols | rd) & all;

            while (poss != 0)
            {
                int bit = poss & -poss;
                poss -= bit;
                count += Try(all, (ld | bit) << 1, cols | bit, (rd | bit) >> 1);
            }

            return count + (cols == all ? 1 : 0);
        }

        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //var solutionCount = Try(15, 0, 0, 0);
            var solutions = ChessBoard.Solve(13);
            var solutionCount = solutions.Count;
            stopwatch.Stop();
            Console.WriteLine($"Found {solutionCount} in solutions in {stopwatch.ElapsedMilliseconds} milliseconds.");
            Console.ReadLine();
            if (solutions.Any())
            {
                foreach (var solution in solutions)
                {
                    Console.WriteLine(solution.ToString());
                }
            }
            else
            {
                Console.WriteLine("No Solutions");
            }
            Console.ReadLine();
        }
    }
}