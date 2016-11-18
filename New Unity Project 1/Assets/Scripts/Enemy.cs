using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool outOfSight;

    [SerializeField]
    private bool attacking, attack;

    private Rigidbody rigid;

    Health healthClass;

    // Use this for initialization
    void Start()
    {
        healthClass = GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (healthClass.GetHealth <= 0)
        {
            GameManager.enemiesKilled++;
            Destroy(this.gameObject);
        }

        if (Vector3.Distance(transform.position, target.position) < 5)
            attack = true;
        else if (Vector3.Distance(transform.position, target.position) > 7.5f)
            attack = false;

        if (!outOfSight)
            rigid.AddForce(target.position - transform.position, ForceMode.Acceleration);

        if (attack && !outOfSight)
            Attack();
    }

    void FixedUpdate()
    {
        //Ray ray = new Ray(transform.position, target.position);
        RaycastHit ra;
        if (Physics.Raycast(transform.position, target.position - transform.position, out ra) && ra.collider.tag == "Player")
        {
            outOfSight = false;
            //if (Vector3.Distance(transform.position, target.position) < 10 && ra.collider.tag == "Player")
            //{
            //    Debug.Log("Hit");
            //}
        }
        else
            outOfSight = true;
    }

    void Attack()
    {

        target.GetComponent<Health>().DealDamage(0.5f);


    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            if (col.relativeVelocity.magnitude > 5)
            {
                float dam = (col.relativeVelocity.magnitude * 10) / 1;
                Debug.Log("Magnitude: " + col.relativeVelocity.magnitude + ", Damage: " + dam);
                target.GetComponent<Health>().DealDamage(dam);
            }
        }
    }

    //public void DamageEnemy(int damage)
    //{
    //    healthClass.DealDamage(damage);
    //}

    public void DealDamage(int damage)
    {
        healthClass.DealDamage(damage);
    }
}
