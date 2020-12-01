
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class CheckpointLight : MonoBehaviour
{

    Light2D light2D;
    private Checkpoint Checkpoint;
    private void Start()
    {
        Checkpoint = GetComponentInParent<Checkpoint>();
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        if (Checkpoint.reachedCheckpoint)
        {
            light2D.enabled = true;
        }
        else
        {
            light2D.enabled = false;
        }
    }
}
