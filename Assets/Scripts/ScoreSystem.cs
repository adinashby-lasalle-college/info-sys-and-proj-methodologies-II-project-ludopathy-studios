// using UnityEngine;
// using System;
// using System.Collections.Generic;
// using Random=UnityEngine.Random;

// public class ScoreSystem : MonoBehaviour
// {
// class BingoGame
// {
//     private static Random random = new Random();
//     private static int[,] bingoCard = new int[5, 5];
//     private static HashSet<int> calledNumbers = new HashSet<int>();
//     private static int score = 0;

//     static void Main()
//     {
//         GenerateBingoCard();
//         DisplayBingoCard();
//         Console.WriteLine("\nStarting the game! Press Enter to draw numbers.");

//         while (score < 12) // Adjust score threshold for difficulty
//         {
//             Console.ReadLine();
//             int number = DrawNumber();
//             Console.WriteLine("Number drawn: " + number);
//             UpdateScore(number);
//             DisplayBingoCard();
//             Console.WriteLine("Current Score: " + score);
//         }

//         Console.WriteLine("Bingo! You won the game!");
//     }

//     static void GenerateBingoCard()
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             HashSet<int> columnNumbers = new HashSet<int>();
//             for (int j = 0; j < 5; j++)
//             {
//                 int min = i * 15 + 1;
//                 int max = i * 15 + 15;
//                 int number;
//                 do
//                 {
//                     number = random.Next(min, max + 1);
//                 } while (columnNumbers.Contains(number));
//                 columnNumbers.Add(number);
//                 bingoCard[j, i] = number;
//             }
//         }
//         bingoCard[2, 2] = 0; // Free space in the center
//     }

//     static void DisplayBingoCard()
//     {
//         Console.WriteLine("\n B   I   N   G   O");
//         for (int i = 0; i < 5; i++)
//         {
//             for (int j = 0; j < 5; j++)
//             {
//                 if (bingoCard[i, j] == 0 || calledNumbers.Contains(bingoCard[i, j]))
//                 {
//                     Console.Write(" X  ");
//                 }
//                 else
//                 {
//                     Console.Write(string.Format("{0,2}  ", bingoCard[i, j]));
//                 }
//             }
//             Console.WriteLine();
//         }
//     }

//     static int DrawNumber()
//     {
//         int number;
//         do
//         {
//             number = random.Next(1, 76);
//         } while (calledNumbers.Contains(number));
//         calledNumbers.Add(number);
//         return number;
//     }

//     static void UpdateScore(int number)
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             for (int j = 0; j < 5; j++)
//             {
//                 if (bingoCard[i, j] == number)
//                 {
//                     bingoCard[i, j] = 0; // Mark as called
//                     score += 10; // Increase score based on matches
//                 }
//             }
//         }
//     }
// }
// }

