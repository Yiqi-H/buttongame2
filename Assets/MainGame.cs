using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int N = 7;
    public int place = 1;
    public Color player = Color.red;
    public Color player2 = Color.green;
    GameObject sphere;

    void Start()
    {
      for(int i=1; i<=N; i++) { 
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(i, 0, 0);
        cube.transform.localScale = Vector3.one*0.7f;
      }
      sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphere.transform.position = new Vector3(place, 1, 0);
      sphere.GetComponent<Renderer>().material.color = player;
      DisplayStage("2X");
    }

    public void DisplayStage(string stage) {
        char playerStr = stage[^1];
        sphere.GetComponent<Renderer>().material.color = 
             (playerStr=='X') ? player : player2;
        place = int.Parse(stage.Substring(0, stage.Length - 1));
    }

    void Update()
    {
        int oldPlace= place;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            place += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            place += 2;
        }
        //Change color in each step
        if (place > N) { place = oldPlace; }
        else if(place>oldPlace) {
           Color newColor = player;
            if (sphere.GetComponent<Renderer>().material.color == player) { 
              newColor = player2;
            }
            sphere.GetComponent<Renderer>().material.color= newColor;
        }
        sphere.transform.position = new Vector3(place, 1, 0);
    }
}
