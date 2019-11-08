using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float speed, lifeSpan, TBS, STBS;

    private Vector2 endPos;

    public GameObject echo;

    void Start()
    {
        endPos = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) ;

        STBS = 0.05f;
        lifeSpan = 0.5f;
        speed = 0.1f;
    }

    void Update()
    {
        if (TBS <= 0)
        {
            Instantiate(echo, transform.position, Quaternion.identity);
            TBS = STBS;
        }else
        {
            TBS -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        lifeSpan -= 1 * Time.deltaTime;
        if (new Vector2 (transform.position.x, transform.position.y) != endPos)
        {
            transform.position = Vector2.Lerp(transform.position, endPos, speed);
        }

        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

}
