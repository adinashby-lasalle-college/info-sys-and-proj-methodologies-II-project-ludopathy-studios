
using System.Linq;
using UnityEngine.UIElements;


public static class BingoCardGenerator
{
    static System.Random random = new System.Random();


    public static int[,] GenerateBingoCard()
    {
        int[,] bingoCard = new int[5, 5];

        int[] b = FisherYateShuffle(1);
        int[] i = FisherYateShuffle(16);
        int[] n = FisherYateShuffle(31);
        int[] g = FisherYateShuffle(46);
        int[] o = FisherYateShuffle(61);

        int[][] sourceArray = { b, i, n, g, o };

        for (int h = 0; h < 5; h++)
        {
            for (int j = 0; j < 5; j++)
            {
                bingoCard[h, j] = sourceArray[h][j];
            }
        }

        return bingoCard;
    }


    static int[] FisherYateShuffle(int min)
    {
        int[] arr = new int[15];

        int n = arr.Length;

        // Populate the array with 15 numbers starting by the min
        for (int i = 0; i < n; i++)
        {
            arr[i] = min;
            min++;
        }


        // Fisher Yates Shuffle
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }

        int[] chosenNumbers = arr.Take(5).ToArray();


        return arr;
    }

}
