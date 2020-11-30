using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transisition;

    public GameController gc;
    public int nextLevel;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        nextLevel = gc.level + 1;
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel(3));
    }

    public void Story()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void Controls()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void Die()
    {
        StartCoroutine(LoadLevel(6));
    }
    public void Win()
    {
        StartCoroutine(LoadLevel(7));
    }
    public void GoBack()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void GoNext()
    {
        StartCoroutine(LoadLevel(nextLevel));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(1);

        transisition.SetTrigger("Start");
    }
}
