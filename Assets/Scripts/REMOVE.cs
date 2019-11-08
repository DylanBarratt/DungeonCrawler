using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REMOVE : MonoBehaviour
 {

    private float lifeSpan;

	void Start ()
	{
        lifeSpan = 0.1f;
	}
	

	void Update ()
	{
        if (lifeSpan > 0)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
            Destroy(gameObject);
	}

}
