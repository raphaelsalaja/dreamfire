using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{

    Vector3 mousePos, mouseVector;
    public int side;

    [Header("Stats")]
    [Space]
    private float timeBtwShots;
    public float startTimeBtwShots;

    [Header("Aiming")]
    [Space]
    public Camera MyCamera;
    public Transform shotPoint;
    public GameObject Projectile;

    [Header("FX")]
    [Space]
    public GameObject MuzzleFlash;
    public SpriteRenderer sr;

    [Header("Prefabs")]
    [Space]
    public LevelController LevelController;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        LevelController = GameObject.FindGameObjectWithTag("UI").GetComponent<LevelController>();
    }

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        mouseVector = (mousePos - transform.position).normalized;
        if (mouseVector.x > 0)
        {
            side = 1;
            Flip(side);
        }
        if (mouseVector.x < 0)
        {
            side = -1;
            Flip(side);
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && LevelController.ammo > 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerShot");
                Vector3 shakeVector = new Vector3(0.1f, 0.1f, 0);
                MyCamera.transform.DOShakePosition(1f, 0.25f, 5, 10f, false, true);
                Instantiate(MuzzleFlash, shotPoint.position, transform.rotation);
                Instantiate(Projectile, shotPoint.position, transform.rotation);
                LevelController.ReduceAmmo();
                Debug.Log("Hello");
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void Flip(int side)
    {

        bool state = (side == 1) ? false : true;
        sr.flipY = state;
    }
}
