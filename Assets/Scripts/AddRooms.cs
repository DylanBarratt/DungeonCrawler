using UnityEngine;

public class AddRooms : MonoBehaviour
{

    private RoomTemplates templates;

    private int numCowbBois;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        numCowbBois = Random.Range(1, 5);

        for (int i = 0; i < numCowbBois; i++)
        {
            Instantiate(Resources.Load("Cowboi"), new Vector3 (Random.Range(transform.position.x - 5, transform.position.x + 5) , Random.Range(transform.position.y - 5, transform.position.y + 5), transform.position.z), Quaternion.identity);
        }
    }
}
