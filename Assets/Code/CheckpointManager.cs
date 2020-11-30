using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{

    private static GameController instance;
    public Vector2 lastCheckpointPos;
    private Transform teleport;

    private void Awake()
    {
        lastCheckpointPos = new Vector2(GameObject.Find("Checkpoint").transform.position.x, GameObject.Find("Checkpoint").transform.position.y);

    }

    public void Respawn(Collider2D other)
    {
        other.transform.position = lastCheckpointPos + new Vector2(0, 2f);
    }
}
