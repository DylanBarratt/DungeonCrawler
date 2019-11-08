using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
 {
    [SerializeField]
    private float MovementSpeed, IdleTime;

    public static float health, storedHealth;

    private Animator PlayerAnimator;

    [SerializeField]
    private GameObject projectile;
    private GameObject hearts0, hearts1, hearts2, hearts3;

    private SpriteRenderer sr;

    private Camera cam1, cam2;

    private bool ded, shot;



    void Start ()
	{
        if (GameObject.Find("Main Camera")!= null)
            cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (GameObject.Find("Camera") != null)
            cam2 = GameObject.Find("Camera").GetComponent<Camera>();

        if (GameObject.Find("lifes_0") != null)
        {
            hearts0 = GameObject.Find("lifes_0");
            hearts1 = GameObject.Find("lifes_1");
            hearts2 = GameObject.Find("lifes_2");
            hearts3 = GameObject.Find("lifes_3");
        }


        PlayerAnimator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        IdleTime = 5f;
        health = 1f;
        storedHealth = 1f;
        ded = false;
        shot = false;

        PlayerAnimator.SetBool("Movement", false);
    }
	

    void Update()
    {
        if (sr.color != new Color(133, 133, 133))
        {
            TurnBack();
        }

        if (ded == false)
        {

            if (Input.GetAxis("Horizontal") > 0.1)
            {
                sr.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.1)
            {
                sr.flipX = false;
            }

            if (health <= 0)
            {
                if (cam2 != null)
                    cam2.enabled = true;

                if (cam1 != null)
                    cam1.enabled = false;

                Invoke("ChangeScene", 3f);

                ded = true;
            }

            if (health < storedHealth)
            {
                storedHealth = health;
                Invoke("TurnBack", 0.1f);
            }

            if (health < 0.75f)
            {
                hearts0.SetActive(false);
            }
            if (health < 0.50f)
            {
                hearts1.SetActive(false);
            }
            if (health < 0.25f)
            {
                hearts2.SetActive(false);
            }
            if (health < 0f)
            {
                hearts3.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (ded == false)
        {
            if (Input.GetAxis("Horizontal") > 0.3f || Input.GetAxis("Horizontal") < -0.3f)
            {
                PlayerAnimator.SetBool("Movement", true);
                IdleTime = 5f;
                transform.position = new Vector2(transform.position.x + (MovementSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime), transform.position.y);
            }
            if (Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Vertical") < -0.3f)
            {
                PlayerAnimator.SetBool("Movement", true);
                IdleTime = 5f;
                transform.position = new Vector2(transform.position.x, transform.position.y + (MovementSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime));
            }

            IdleTime -= 1 * Time.deltaTime;

            if (IdleTime <= 0)
            {
                if (PlayerAnimator != null)
                    PlayerAnimator.SetBool("Movement", false);
            }

            if (Input.GetButtonDown("Attack"))
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
            }

            if (Input.GetAxis("Attack") > 0.1f)
            {
                if (shot == false)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    Invoke("Reload", 1);
                    shot = true;
                }
            }
        }    
    }
    public void TurnBack()
    {
        sr.color = new Color(0.5215687f, 0.5215687f, 0.5215687f);
    }

    private void ChangeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Reload()
    {
        shot = false;
    }
}
