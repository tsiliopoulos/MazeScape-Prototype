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

    [SerializeField]
    float respawnTimer;

    Vector3 respawnPoint;

    private Rigidbody rigid;

    Health healthClass;

    LineRenderer line;

    // Use this for initialization
    void Start()
    {
        healthClass = GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthClass.GetHealth <= 0)
        {
            // StartCoroutine(Dead());
            GameManager.enemiesKilled++;
            Destroy(this.gameObject);
        }

        if (Vector3.Distance(transform.position, target.position) < 5)
            attack = true;
        else if (Vector3.Distance(transform.position, target.position) > 7.5f)
            attack = false;

        if (!outOfSight && rigid.velocity.y >= -1.5f)
            rigid.AddForce(target.position - transform.position, ForceMode.Acceleration);

        if (attack && !outOfSight)
            Attack();
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }

    void Respawn()
    {
        GameManager.enemiesKilled++;
        this.gameObject.SetActive(false);

        float time = 5f;
        while(time >= 0)
        {
            time -= Time.fixedDeltaTime;
        }
        
        if(time <=0)
        {
            this.gameObject.SetActive(true);
            healthClass.ResetHealth();
        }
    }

    IEnumerator Dead()
    {
        GameManager.enemiesKilled++;
        healthClass.ResetHealth();
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnTimer);
        this.gameObject.SetActive(true);
    }
    void FixedUpdate()
    {
        RaycastHit ra;
        if (Physics.Raycast(transform.position, target.position - transform.position, out ra) && ra.collider.tag == "Player")
        {
            outOfSight = false;
        }
        else
            outOfSight = true;
    }

    void Attack()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
        target.GetComponent<Health>().DealDamage(0.5f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            if (col.relativeVelocity.magnitude > 5)
            {
                float dam = (col.relativeVelocity.magnitude * 10);
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
