using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private CheckpointManager CheckpointManager;
    private GameController GameController;
    private void Start()
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        GameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GameController.deaths += 1;
            CheckpointManager.Respawn(other);
        }

    }
}
