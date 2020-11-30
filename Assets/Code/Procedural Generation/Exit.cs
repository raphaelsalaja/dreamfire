using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private SceneReloader sr;
    public Animator transisition;
    private LevelController lc;
    private LevelController lc_2;
    public bool isLastLevel;

    private void Awake()
    {
        //levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
    }
    private void Start()
    {
        // GameObject lc = GameObject.Find("Canvas");
        // LevelController other = (LevelController)lc.GetComponent(typeof(LevelController));
        // lc_2 = GameObject.FindGameObjectWithTag("lc").GetComponent<LevelController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isLastLevel)
            {
                WinGame();
            }
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(8 ));
    }
    public void WinGame()
    {
        StartCoroutine(LoadLevel(7));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        transisition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
    }
}
