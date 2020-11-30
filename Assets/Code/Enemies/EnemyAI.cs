using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{

    public LevelGenerator levelGenerator;
    [Header("Stats")]
    [Space]
    public int health;
    public float speed;
    public int damage;

    [Header("Stopping Distance")]
    [Space]
    public float playerDistance;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;

    [Header("Weapons")]
    [Space]
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyProjectile;

    [Header("Animations")]
    [Space]
    public Animator animator;

    void Awake()
    {
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();

        timeBtwShots = startTimeBtwShots + (UnityEngine.Random.value * 2);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //enemyProjectile.damage = damage;
        if (Vector2.Distance(transform.position, player.position) < playerDistance)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                animator.SetBool("IsMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                animator.SetBool("IsMoving", false);
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                animator.SetBool("IsMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
            if (timeBtwShots <= 0)
            {
                Instantiate(enemyProjectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (health <= 0)
        {
            levelGenerator.EnemyCount--;
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            Destroy(gameObject);
        }
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
