using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReset : MonoBehaviour
{
    [HideInInspector]
    public GameController gc;
    



    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        gc.starsCollected = 0;
        gc.deaths = 0;
    }
   
}
