using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> players;
    public List<int> playerDeaths;
    public List<int> playerKills;
    public List<int> playerOutPos;
    public int playersInPlay;
    public GameMenuManager gmm;
    // Start is called before the first frame update
    void Start()
    {
        playersInPlay = players.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(playersInPlay == 1)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(!players[i].GetComponent<PlayerStatsManager>().isDead)
                {
                    MarkPlayerOut(i + 1);
                    playersInPlay--;
                    gmm.DisplayResultsMenu();
                }
            }
        }
    }

    public void AddPlayerDeath(int playerNum)
    {
        playerDeaths[playerNum - 1]++;
    }

    public void AddPlayerKill(int playerNum)
    {
        playerOutPos[playerNum - 1]++;
    }

    public void MarkPlayerOut(int playerNum)
    {
        playerOutPos[playerNum-1] = playersInPlay;
        playersInPlay--;
    }
    


}
