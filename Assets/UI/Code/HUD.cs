using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [HideInInspector]
    public GameController gc;
    private int stars = 0;
    private int deaths = 0;
    [Header("Texts")]
    public Text StarsText;
    public Text DeathText;



    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        
    }
    private void Update()
    {
        stars = gc.starsCollected;
        deaths = gc.deaths;
        StarsText.text = "Stars: "  + gc.starsCollected;
        DeathText.text = "Deaths: " + deaths + " / " + gc.deathsRemaining;
    }
}
