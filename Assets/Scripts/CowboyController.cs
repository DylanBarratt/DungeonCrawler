using UnityEngine;

public class CowboyController : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer sr;

    private float speed, health, timeSinceLastShot;

    private Animator CowboiAnimator;

    private bool shot, ded;

    private Collider2D[] cNear, cFar;

    void Start()
    {
        player = GameObject.Find("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();
        CowboiAnimator = gameObject.GetComponent<Animator>();

        shot = false;
        ded = false;
        speed = 0.03f;
        health = 1;
    }

    void Update()
    {
        if (health <= 0)
        {
            CowboiAnimator.Play("C_ded");
            ded = true;
            Invoke("DestroyV", 1f);
        }

        if (timeSinceLastShot > 0)
        {
            timeSinceLastShot -= Time.deltaTime;
        }else
        {
            shot = false;
        }
    }

    void FixedUpdate()
    {
        if (ded == false)
        {
            cNear = (Physics2D.OverlapCircleAll(transform.position, 1f));
            cFar = (Physics2D.OverlapCircleAll(transform.position, 4f));

            int i = 0;
            foreach (Collider2D x in cNear)
            {
                if (x.gameObject == player)
                {
                    if (shot == false)
                    {
                        CowboiAnimator.Play("C_Shoot");
                        Instantiate(Resources.Load("Bullet"), transform.position, Quaternion.identity);
                        shot = true;
                        timeSinceLastShot = 1;
                    }
                }
                else
                    i++;
            }

            foreach (Collider2D x in cFar)
            {
                if (x.gameObject == player && i == cNear.Length)
                {
                    transform.position = Vector2.Lerp(transform.position, player.transform.position, speed);
                }
            }

            if (player.transform.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else
                sr.flipX = false;
        }

    }
    
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            sr.color = Color.red;
            Invoke("TurnBack", 0.3f);
            health -= 0.25f;
        }
    }

    void TurnBack ()
    {
        sr.color =  new Color(0.5215687f, 0.5215687f, 0.5215687f);
    }

    void DestroyV()
    {
        Destroy(gameObject);
    }
}
