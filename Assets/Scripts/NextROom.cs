using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class NextROom : MonoBehaviour
{

    private int Level;

    private Text textV;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Level += 1;
                File.WriteAllText(Application.dataPath + "cLevel.donotopen", Level.ToString());
                SceneManager.LoadScene(1);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Level += 1;
                File.WriteAllText(Application.dataPath + "cLevel.donotopen", Level.ToString());
                SceneManager.LoadScene(0);
            }
        }   
    }


    void Start()
    {
        if (GameObject.Find("Level") != null)
        {
            textV = GameObject.Find("Level").GetComponent<Text>();
        }


        
        if (!File.Exists(Application.dataPath + "cLevel.donotopen"))
        {
            Level = 0;
        }else
        {
            string levelS = File.ReadAllText(Application.dataPath + "cLevel.donotopen");
            Level = int.Parse(levelS);
        }

        File.WriteAllText(Application.dataPath + "cLevel.donotopen" , Level.ToString());
    }

    void Update()
    {
        if (File.ReadAllText(Application.dataPath + "cLevel.donotopen") != Level.ToString())
        {
            File.WriteAllText(Application.dataPath + "cLevel.donotopen", Level.ToString());
        }
        if(textV != null)
        textV.text = File.ReadAllText(Application.dataPath + "cLevel.donotopen");
    }
}
