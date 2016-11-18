using UnityEngine;
using System.Collections;

public class PlayerHealth : Health
{
    [SerializeField]
    Vector3 checkpoint;


    void Start()
    {
        SetStartingHealth();
        checkpoint = transform.position;
        Debug.Log(GetStartingHealth);
    }

    void Update()
    {
        if(GetHealth <= 0)
        {
            Respawn();
            GameManager.playerDeaths++;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.collider.tag == "Checkpoint")
        {
            checkpoint = col.gameObject.transform.position;
            Debug.Log("Checkpoint reached!");
        }
    }

    void Respawn()
    {
        ResetHealth();
        transform.position = checkpoint;
    }

}
