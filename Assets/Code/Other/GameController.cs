using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level;
    public int level_s;
    public int deaths;
    public int deathsRemaining = 5;
    public int starsCollected;
    public int totalStars;
    public int AmountOfStars;
    public int time;
    public int timeRemaining;
    public int grade;
    private static GameController instance;
    private Transform teleport;
    public Animator transisition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
                totalStars = GameObject.FindGameObjectsWithTag("Star").Length;

    }

    private void Start()
    {
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
    }

    void Update()
    {
        DontDestroyOnLoad(this);
        transisition = GameObject.Find("LevelLoader").GetComponentInChildren<Animator>();

        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        Stars();
        WhichLevel();
        level_s = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(level_s);
        if (deaths == 10)
        {
            StartCoroutine(LoadLevel(6));
            deaths = 0;
            starsCollected = 0;
        }
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        deaths = 0;
        starsCollected = 0;
        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(1);

        transisition.SetTrigger("Start");
        deaths = 0;
        starsCollected = 0;
    }

    private void WhichLevel()
    {
        switch (level_s)
        {
            case 3:
                level = 3;
                break;
            case 4:
                level = 4;
                break;
            case 5:
                level = 5;
                break;
        }
    }

    private void Stars()
    {
        int half = AmountOfStars / 2;

        if (totalStars == 0)
        {
            grade = 3;
        }
        else if (starsCollected >= totalStars)
        {
            grade = 2;
        }
        else if ((starsCollected <= totalStars) && starsCollected != 0)
        {
            grade = 1;
        }
        else if (starsCollected == 0)
        {
            grade = 0;
        }
    }
}
