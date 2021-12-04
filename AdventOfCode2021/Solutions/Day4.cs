using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    internal class Day4
    {
        internal static void Problem1()
        {
            (List<int> numbers, List<int[][]> boards, List<bool[][]> called) = Load();

            foreach (int call in numbers)
            {
                for (int game = 0; game < boards.Count; game++)
                {
                    int[][] board = boards[game];

                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int col = 0; col < board[row].Length; col++)
                        {
                            if (board[row][col] == call)
                            {
                                called[game][row][col] = true;
                            }
                        }
                    }
                }

                for (int game = 0; game < boards.Count; game++)
                {
                    if (CheckWin(boards[game], called[game], game))
                    {
                        CalculateScore(boards[game], called[game], call);
                        return;
                    }
                }
            }
        }

        internal static void Problem2()
        {
            (List<int> numbers, List<int[][]> boards, List<bool[][]> called) = Load();
            HashSet<int> winnable = new HashSet<int>(Enumerable.Range(0, boards.Count));

            foreach (int call in numbers)
            {
                for (int game = 0; game < boards.Count; game++)
                {
                    int[][] board = boards[game];

                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int col = 0; col < board[row].Length; col++)
                        {
                            if (board[row][col] == call)
                            {
                                called[game][row][col] = true;
                            }
                        }
                    }
                }

                for (int game = 0; game < boards.Count; game++)
                {
                    if (!winnable.Contains(game))
                    {
                        continue;
                    }

                    if (CheckWin(boards[game], called[game], game))
                    {
                        winnable.Remove(game);

                        if (winnable.Count == 0)
                        {
                            CalculateScore(boards[game], called[game], call);
                            return;
                        }
                    }
                }
            }
        }

        private static (List<int> Numbers, List<int[][]> Boards, List<bool[][]> Called) Load()
        {
            List<int> numbers = null;
            List<int[][]> boards = new List<int[][]>();
            int[][] curBoard = null;
            int loadRow = 0;

            foreach (string line in Data.Enumerate())
            {
                if (numbers is null)
                {
                    numbers = new List<int>(line.Split(',').Select(int.Parse));
                    continue;
                }

                if (string.IsNullOrEmpty(line))
                {
                    if (curBoard is not null)
                    {
                        boards.Add(curBoard);
                        curBoard = null;
                    }

                    continue;
                }

                if (curBoard is null)
                {
                    curBoard = new int[5][];
                    loadRow = 0;
                }

                curBoard[loadRow] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Debug.Assert(curBoard[loadRow].Length == 5);
                loadRow++;
            }

            List<bool[][]> called = new List<bool[][]>();

            foreach (int[][] board in boards)
            {
                bool[][] calledBoard = new bool[board.Length][];

                for (int i = 0; i < board.Length; i++)
                {
                    calledBoard[i] = new bool[board[i].Length];
                }

                called.Add(calledBoard);
            }

            return (numbers, boards, called);
        }

        private static void CalculateScore(int[][] board, bool[][] called, int call)
        {
            long sum = 0;

            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board.Length; col++)
                {
                    if (!called[row][col])
                    {
                        sum += board[row][col];
                    }
                }
            }

            Console.WriteLine($"Winner won with call {call}");
            Console.WriteLine($"Answer is {call * sum}");
        }

        private static bool CheckWin(int[][] board, bool[][] called, int game)
        {
            for (int row = 0; row < board.Length; row++)
            {
                if (called[row].All(v => v))
                {
                    Console.WriteLine($"Winner in game {game} by row {row}");
                    return true;
                }
            }

            for (int col = 0; col < board.Length; col++)
            {
                if (called.All(row => row[col]))
                {
                    Console.WriteLine($"Winner in game {game} by col {col}");
                    return true;
                }
            }

            return false;
        }
    }
}
