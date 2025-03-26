using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Andres_Scene_Scripts
{
    public class BingoSelectionHandler : Singleton<BingoSelectionHandler>
    {
        public BingoCard bingoCard;
        private Color defaultColor = Color.white;
        private Color highlightColor = Color.blue;
        private Color powerUpColor = Color.red;

        private HashSet<int> powerUpNumbers = new HashSet<int>(); // Store power-up numbers

        private void Start()
        {
            StartCoroutine(WaitAndAssignPowerUps());
        }

        private IEnumerator WaitAndAssignPowerUps()
        {
            yield return new WaitForSeconds(0.5f);
            AssignRandomPowerUps(5);
        }

        public void OnSelectCell(int selectedIndex)
        {
            if (bingoCard == null)
            {
                Debug.LogError("BingoCard instance not found!");
                return;
            }

            ResetHighlights(); // Clear previous highlights

            int selectedNumber = int.Parse(bingoCard.TxtBox[selectedIndex].text);
            List<int> rowIndexes = GetRowIndexes(selectedIndex);
            List<int> colIndexes = GetColumnIndexes(selectedIndex);

            // Highlight row and column
            foreach (int index in rowIndexes)
            {
                //bingoCard.MarkNumber(bingoCard.Numbers[index]);
            }
            foreach (int index in colIndexes)
            {
                //bingoCard.MarkNumber(bingoCard.Numbers[index]);
            }

            // Check if selected number has a power-up
            if (powerUpNumbers.Contains(selectedNumber))
            {
                bingoCard.BingoCardBtns[selectedIndex].GetComponent<Image>().color = powerUpColor;
                ActivatePowerUp(selectedNumber);
            }
        }

        private void AssignRandomPowerUps(int count)
        {
            List<int> availableNumbers = new List<int>();

            foreach (TMP_Text txt in bingoCard.TxtBox)
            {
                string cleanedText = txt.text.Trim();

                if (string.IsNullOrEmpty(cleanedText))
                {
                    Debug.LogWarning("Skipping empty cell in BingoCard.");
                    continue;
                }

                int number;
                if (int.TryParse(cleanedText, out number))
                {
                    availableNumbers.Add(number);
                }
                else
                {
                    Debug.LogWarning($"Invalid number in BingoCard: '{cleanedText}'");
                }
            }

            if (availableNumbers.Count < count)
            {
                Debug.LogError("Not enough valid numbers to assign power-ups!");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, availableNumbers.Count);
                powerUpNumbers.Add(availableNumbers[randomIndex]);
                availableNumbers.RemoveAt(randomIndex);
            }

            Debug.Log("Power-up numbers: " + string.Join(", ", powerUpNumbers));
        }

        public List<int> GetRowIndexes(int selectedIndex)
        {
            int row = selectedIndex / 5;
            List<int> rowIndexes = new List<int>();
            for (int col = 0; col < 5; col++)
            {
                rowIndexes.Add((row * 5) + col);
            }
            return rowIndexes;
        }

        void DisplayRowNumbers(List<int> rowIndexes)
        {
            for (int i = 0; i < rowIndexes.Count; i++)
            {
                Debug.Log("Row numbers " + bingoCard.TxtBox[rowIndexes[i]].text);

            }
        }

        void OnClickTest(int selectedNumber)
        {
            List<int> indexes = GetRowIndexes(selectedNumber);
            DisplayRowNumbers(indexes);
        }

        private List<int> GetColumnIndexes(int selectedIndex)
        {
            int col = selectedIndex % 5;
            List<int> colIndexes = new List<int>();
            for (int row = 0; row < 5; row++)
            {
                colIndexes.Add((row * 5) + col);
            }
            return colIndexes;
        }

        private void ResetHighlights()
        {
            foreach (Button btn in bingoCard.BingoCardBtns)
            {
                btn.GetComponent<Image>().color = defaultColor;
            }
        }

        private void ActivatePowerUp(int number)
        {
            Debug.Log("Power-up activated on number: " + number);
            // Example power-up: Clear entire row
            foreach (TMP_Text txt in bingoCard.TxtBox)
            {
                if (txt.text == number.ToString())
                {
                    txt.color = powerUpColor; // Change color for effect
                    txt.text = "X"; // Mark as used power-up
                }
            }
        }
    }
}