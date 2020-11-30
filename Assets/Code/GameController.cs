using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level;
    public int level_s;
    public int deaths;
    public int starsCollected;
    public int totalStars;
    public int time;
    public int timeRemaining;
    public int grade;
    private static GameController instance;
    private Transform teleport;

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
    }
    private void Start()
    {
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
      
    }

    void Update()
    {
        DontDestroyOnLoad(this);
        Stars();

        WhichLevel();
          level_s = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(level_s);
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
        int half = totalStars / 2;

        if (starsCollected == totalStars)
        {
            grade = 3;
        }
        else if (starsCollected >= half)
        {
            grade = 2;
        }
        else if ((starsCollected <= half - 1) && starsCollected != 0)
        {
            grade = 1;
        }
        else
        {
            grade = 0;
        }
    }
}
