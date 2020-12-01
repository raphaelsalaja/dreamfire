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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isLastLevel)
            {
                WinGame();
            }
            else
            {
                LoadNextLevel();
            }
        }
    }
    private void Update()
    {
         if (Input.GetKey(KeyCode.Alpha8))
        {
            LoadNextLevel();
        }
        if (isLastLevel && Input.GetKey(KeyCode.Alpha9))
        {
            WinGame();
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(8));
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
