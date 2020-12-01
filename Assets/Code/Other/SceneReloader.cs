using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // public Animator transisition;
    // public HealthUI ui;
    // private void Start()
    // {
    //     GameObject ui = GameObject.Find("Canvas");
    //     HealthUI other = (HealthUI)ui.GetComponent(typeof(HealthUI));
    //     other.World();

    // }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         LoadNextLevel();
    //         ui.World();
    //         // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    //     if (HealthUI.HP <= 0)
    //     {
    //         LoadNextLevel_Menu();
    //     }
    // }

    // public void LoadNextLevel()
    // {
    //     StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    // }

    // public void LoadNextLevel_Menu()
    // {
    //     StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    // }

    // private IEnumerator LoadLevel(int levelIndex)
    // {
    //     SceneManager.LoadScene(levelIndex);

    //     yield return new WaitForSeconds(1); transisition.SetTrigger("Start");

    // }


}