using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    private float fObsticleMoveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * fObsticleMoveSpeed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Collider"))
		{
			Destroy(gameObject);
		}
	}
}
