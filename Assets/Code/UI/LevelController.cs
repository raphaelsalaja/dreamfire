using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class LevelControllers : MonoBehaviour
{
    public GameController gc;
    public Animator transisition;
    public Menu menu;
    public LevelGenerator levelGenerator;

    [Header("LEVEL STATS")]
    [Space]

    public static int deaths;
    public static int deathsGoal;
    public static int stars;
    public static int starsGoal;
    public static int time;
    public static int timeRemaining;

    [Header("WORLD")]
    [Space]
    public Text WORLD_LEVEL_TEXT;
    public int last_stage = 1;
    public int last_world = 1;
    public static int stage = 1;
    public static int world = 1;

    [Header("ENEMIES")]
    public int enemiesCount;
    public Text enemies_left;
    public Text enemies_left_shadow;

    [Header("WEAPONS")]
    [Space]
    public Text AMMO_TEXT, AMMO_TEXT_SHADOW;
    public static int ammoMax = 32;
    public int ammo = 32;
    public static int reloadTime = 1;

    [Header("LEVEL TYPE")]
    [Space]
    public bool endless;
    void Start()
    {

        gc = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        if (endless)
        {

        }
        else
        {

        }
    }
    private void Update()
    {
        enemiesCount = levelGenerator.EnemyCount;
        WORLD_LEVEL_TEXT.text = world + " - " + stage;

        Ammo();
        Enemies();
        AMMO_TEXT_SHADOW.text = AMMO_TEXT.text;




    }

    public void Enemies()
    {
        enemies_left.text = enemiesCount.ToString();
        enemies_left_shadow.text = enemies_left.text;
    }

    public void World()
    {
        stage++;
        if (stage == 3)
        {
            stage = 1;
            world++;
        }

    }

    public void Ammo()
    {
        AMMO_TEXT.text = ammo.ToString();
        AMMO_TEXT_SHADOW.text = AMMO_TEXT.text;
        if (ammo <= 0)
        {
            StartCoroutine(Reload(reloadTime));

        }

    }

    private IEnumerator Reload(int reloadTime)
    {

        yield return new WaitForSeconds(reloadTime);
        ammo = ammoMax;


    }

    public void ReduceAmmo()
    {
        ammo--;
    }



    public void Die()
    {
        GetStages();
        ResetVariables();
        StartCoroutine(LoadLevel(3));
    }

    private void GetStages()
    {

    }

    public void Win()
    {
        ResetVariables();
        StartCoroutine(LoadLevel(4));
    }

    private void ResetVariables()
    {
        stage = 1;
        world = 1;
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(1);

        transisition.SetTrigger("Start");
    }
}
