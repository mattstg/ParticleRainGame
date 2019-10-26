using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : Unit
{
    float counter;
    Transform closestTarget;
    
    public override void Refresh()
    {
        counter -= Time.deltaTime;
        base.PhysicsRefresh();
        if (counter <= 0 || closestTarget == null)
        {
            float closestDistance = float.MaxValue;
            foreach (Enemy enemy in EnemyManager.Instance.units)
            {
                float d = Vector2.SqrMagnitude(transform.position - enemy.transform.position);
                if (d < closestDistance)
                {
                    closestDistance = d;
                    closestTarget = enemy.transform;
                }
            }
            if(!closestTarget)
                Move(transform.position - closestTarget.position);
            counter = .1f + Random.value * .5f;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
            GameObject.Destroy(collision.gameObject);
    }

    public void Die()
    {
        NPCManager.Instance.HasDied(this);
        GameObject.Destroy(gameObject);
    }
}
