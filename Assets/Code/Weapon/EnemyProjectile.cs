using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    [Header("Stats")]
    [Space]
    public float speed;
    public int damage;

    [Header("Collision")]
    [Space]
    public float distance;
    public LayerMask whatIsSolid;
    Rigidbody2D projectileRB;
    private Vector2 target;

    [Header("Player")]
    [Space]
    public Transform player;
    public LevelController LevelController;

    [Header("Enemy")]
    [Space]
    public EnemyAI enemyAI;

    public void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        LevelController = GameObject.FindGameObjectWithTag("UI").GetComponent<LevelController>();

        target = new Vector2(player.position.x, player.position.y);
        Vector2 moveDir = (player.transform.position - transform.position).normalized * speed;
        projectileRB.velocity = new Vector2(moveDir.x, moveDir.y);

    }


    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        {
            if (hitinfo.collider != null)
            {
                if (hitinfo.collider.CompareTag("Player"))
                {

                }
                DestroyProjectile();
            }
        }
    }

    void DestroyProjectile()
    {
        ChooseBulletHit();
        Destroy(gameObject);
    }
    void ChooseBulletHit()
    {
        float RandomValue = UnityEngine.Random.value;
        if (RandomValue <= 0.2)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit");
        }
        else if (RandomValue >= 0.2 && RandomValue < 0.4)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit2");
        }
        else if (RandomValue >= 0.4 && RandomValue < 0.8)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit3");
        }
        else if (RandomValue >= 0.8 && RandomValue <= 1)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit4");
        }
    }

}
