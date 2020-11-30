using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private GameController GameController;
    private static Star instance;
    [Space]
    [Header("Polish")]
    public GameObject StarParticles;
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GameController.starsCollected += 1;
            Instantiate(StarParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
