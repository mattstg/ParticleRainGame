using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    float counter;
    Transform closestTarget = null;
    public override void Refresh()
    {
        counter -= Time.deltaTime;
        base.PhysicsRefresh();
        if (counter <= 0 || closestTarget == null)
        {
            float closestDistance = float.MaxValue;
            foreach (NPC npc in NPCManager.Instance.units)
            {
                float d = Vector2.SqrMagnitude(transform.position - npc.transform.position);
                if (d < closestDistance)
                {
                    closestDistance = d;
                    closestTarget = npc.transform;
                }
            }

            if (Vector2.SqrMagnitude(PlayerManager.Instance.player.transform.position - transform.position) < closestDistance)
                closestTarget = PlayerManager.Instance.player.transform;


            Move(closestTarget.position - transform.position);
            counter = .25f + Random.value*.5f;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("NPC"))
        {
            Vector2 pos = collision.gameObject.transform.position;
            collision.gameObject.GetComponent<NPC>().Die();
            EnemyManager.Instance.CreateEnemy(pos);
        }
        else if(collision.transform.CompareTag("Player"))
        {
            PlayerManager.Instance.player.Die();
        }
        else if(collision.transform.CompareTag("Orb"))
        {
            Vector2 pos = transform.position;
            NPCManager.Instance.CreateNPC(pos);
            GameObject.Destroy(collision.gameObject);
            Die();
        }
    }

    public void Die()
    {
        EnemyManager.Instance.EnemyDied(this);
        GameObject.Destroy(gameObject);
    }
}
