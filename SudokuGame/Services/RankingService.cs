using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class RankingService
    {
        public static void Load()
        {
            DatabaseService.GetRecords();
        }
        public static void Save(string nickname, int score)
        {
            DatabaseService.CreateRecord(nickname, score);
        }
        public static int Calculate(int level, int time, bool hasUsedIndicator)
        {
            int score = 0;

            if (hasUsedIndicator)
            {
                switch (level)
                {
                    case 0:
                        score += 50; break;
                    case 1:
                        score += 100; break;
                    case 2:
                        score += 200; break;
                    case 3:
                        score += 300; break;
                    case 4:
                        score += 500; break;
                    default: break;
                }

                switch (time)
                {
                    case 0:
                        score += 25; break;
                    case 1:
                        score += 300; break;
                    case 2:
                        score += 200; break;
                    case 3:
                        score += 100; break;
                    case 4:
                        score += 50; break;
                    default: break;
                }

                score -= 250;
            }
            else
            {
                switch (level)
                {
                    case 0:
                        score += 100; break;
                    case 1:
                        score += 200; break;
                    case 2:
                        score += 250; break;
                    case 3:
                        score += 500; break;
                    case 4:
                        score += 1000; break;
                    default: break;
                }

                switch (time)
                {
                    case 0:
                        score += 100; break;
                    case 1:
                        score += 500; break;
                    case 2:
                        score += 400; break;
                    case 3:
                        score += 300; break;
                    case 4:
                        score += 200; break;
                    default: break;
                }

                score += 250;
            }

            return score;
        }
    }
}
