using System;
using UnityEngine;

public class CowboyBUlletController : MonoBehaviour
{

    private GameObject player;

    private SpriteRenderer sr;

    private float speed, lifeSpan;


    void Start()
    {
        player = GameObject.Find("Player");
        sr = player.GetComponent<SpriteRenderer>();

        speed = 0.03f;
        lifeSpan = 1.5f;
    }

    void Update()
    {
        lifeSpan -= Time.deltaTime;

        if (lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, speed);
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (sr != null)
            {
                sr.color = Color.red;
            }
            PlayerController.health -= 0.1f;
            Destroy(gameObject);
        }
    }

}
