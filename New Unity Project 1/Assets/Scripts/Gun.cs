using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
        {
            Ray ray = new Ray(Camera.main.transform.position - (-1 * Camera.main.transform.forward), Camera.main.transform.forward);
            RaycastHit raycast;
            if(Physics.Raycast(ray,out raycast, Mathf.Infinity))
            {
                if (raycast.collider.tag == "Enemy")
                {
                    raycast.collider.GetComponent<Enemy>().DealDamage(2);
                }
            }
        }
	
	}
}
