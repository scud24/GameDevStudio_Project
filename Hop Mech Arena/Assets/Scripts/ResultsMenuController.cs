using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenuController : MonoBehaviour
{
    public Text firstPlaceText;
    public Text secondPlaceText;
    public Text thirdPlaceText;
    public Text fourthPlaceText;
    public ScoreManager sm;

    public void SetupResultsDisplay()
    {
        List<int> playerRankOrder = new List<int>();

        for (int i = 0; i < sm.playerOutPos.Count; i++)
        {
            for (int j = 0; j < sm.playerOutPos.Count; j++)
            {
                if (sm.playerOutPos[j] == i + 1)
                {
                    playerRankOrder.Add(j);
                }
            }
        }

        firstPlaceText.text = "Player " + playerRankOrder[0] + 1 + " Wins!";
        secondPlaceText.text = "Second Place: Player " + playerRankOrder[1] + 1;
        thirdPlaceText.text = "Third Place: Player " + playerRankOrder[2] + 1;
        fourthPlaceText.text = "Fourth Place: Player " + playerRankOrder[3] + 1;
    }
}
