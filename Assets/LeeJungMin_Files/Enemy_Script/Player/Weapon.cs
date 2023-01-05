using System.Collections;
using System.Collections.Generic;
using static UnityEngine.ParticleSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Character character;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        character = GameManager.Inst.Character;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (other.CompareTag("Enemy"))
                {
                    Debug.Log("적을 공격했다.");
                    IBattle target = other.GetComponent<IBattle>();
                    if (target != null)
                    {
                        character.Attack(target);
                    }
                }
            i++;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적을 공격했다.");
            IBattle target = other.GetComponent<IBattle>();
            if (target != null)
            {
               character.Attack(target);
            }
        }
    }
}
