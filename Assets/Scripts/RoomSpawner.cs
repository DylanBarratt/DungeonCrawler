using UnityEngine;


public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;

    public bool spawned = false;
    private bool canSpawnHere;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);
    }

    void Update ()
    {
        if (spawned == true)
        {
            Destroy(gameObject);
        }
    }
    void Spawn()
    {
        if (Physics2D.OverlapCircleAll(transform.position, 1).Length > 0)
        {
            for (int i = 0; i < Physics2D.OverlapCircleAll(transform.position, 1).Length; i++)
            {
                if (Physics2D.OverlapCircleAll(transform.position, 1)[i].gameObject != gameObject)
                {
                    spawned = true;
                    return;
                }
            }
        }

        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                spawned = true;
                return;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                spawned = true;
                return;
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                spawned = true;
                return;
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                spawned = true;
                return;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                if (gameObject != null)
                {
                    if (templates != null)
                    {
                        Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
                
            }
            spawned = true;
        }
    }
}