using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Squad target;
    [SerializeField] private float speed = 80;
    [SerializeField] private int damage = 2;

    public Squad Target
    {
        set { target = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        transform.up = target.transform.position - transform.position;

        if (Vector2.Distance(transform.position, target.transform.position) <= 20)
        {
            target.DamageSquad(damage);
            Destroy(this.gameObject);
        }
    }
}
