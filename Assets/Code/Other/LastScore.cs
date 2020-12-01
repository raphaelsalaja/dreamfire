using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastScore : MonoBehaviour
{
    public GameController gc;
    [Header("Stats")]
    [Space]
    public Text Results_Text;
    public int stars = 0;
    public int grade = 0;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        stars = gc.starsCollected;
        grade = gc.grade;
    }
    private void Update()
    {
        Results_Text.text = "Stars collected: " + stars + "\nGrade: " + grade;
    }
}
