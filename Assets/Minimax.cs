using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimax : MonoBehaviour
{
    int[] allowedSteps = {1, 2, 3};
    List<string> positions = new List<string>();
    int currentPosition = 0;
    MainGame game;
    float lastTime = 0;
    char opposite(char p) { return (p == 'N') ? 'X' : 'N'; }
    List<string> availableMoves(string stage) { 
        List<string> answers = new List<string>();
        //char player = stage[stage.Length-1];
        char player = stage[^1];
        int place=int.Parse(stage.Substring(0, stage.Length - 1));
        char newplayer=opposite(player);
        foreach(int stepLength in allowedSteps){
            answers.Add((place + stepLength).ToString() + newplayer);
        }
        return answers;
    }
    void Start()
    {
        //Debug.Log(string.Join(",", availableMoves("2X")));
        positions = availableMoves("2X");
        game = GameObject.Find("GameObject").GetComponent<MainGame>();
    }

    void Update()
    {

        game.DisplayStage(positions[currentPosition]);
        if (Time.time >= lastTime + 1)
        {
            currentPosition++;
            if (currentPosition == positions.Count) { currentPosition = 0; }
            lastTime=Time.time;
        }
    }
}
