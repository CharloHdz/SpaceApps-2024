using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_ParticlesController : MonoBehaviour
{
    public Color paintColor;
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;

    [Space]
    ParticleSystem particleSys;
    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSys.GetCollisionEvents(other, collisionEvents);
        sp_Paintable paintable = other.GetComponent<sp_Paintable>();

        if (paintable != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(paintable,pos,radius,hardness,strength,paintColor);
            }
        }
    }
}
