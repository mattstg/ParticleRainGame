using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> allEvents = new List<ParticleCollisionEvent>();
        other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, allEvents);
        //Debug.Log("I was hit by a particle spawned by the system: " + other.name);
        foreach (ParticleCollisionEvent pce in allEvents)
        {
            Vector2 offset = Vector2.one * .5f * pce.velocity.normalized; //janky way to calculate which offset it hit
            TileManager.Instance.FireParticleHitLocation(pce.intersection + (Vector3)offset);
        }
    }
}
