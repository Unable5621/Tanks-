using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shell : MonoBehaviour
{
    //The time in seconds before the shell is removed
    public float m_MaxLifeTime = 2f;
    //The amount of damage done if the explosion is centred on a tank
    public float m_MaxDamage = 34f;
    //The maximum distance away from the explosion tanks can be and are still affected
    public float m_ExlposionRadius = 5f;
    //The amount of force added to a tank at te center of the explosion
    public float m_ExplosionForce = 100f;

    //Reference to the particles that will play on explosionm
    public ParticleSystem m_ExplosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        //if it isn't destoryed by then, destroy the shell after it's lifetime
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        //find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        // only tanks will have rigidbody scripts
        if (targetRigidbody != null )
        {
            // Add an explosion force
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExlposionRadius);

            // find the TankHealth script associated with the rigidbody
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (targetHealth != null )
            {
                // Calculate the amount of damage the target should take based on it's distance from the shell
                float damage = CalculateDamage(targetRigidbody.position);

                // Deal this damage to the tank
                targetHealth.TakeDamage(damage);
            }
        }

        //Unparent the particles from the shell
        m_ExplosionParticles.transform.parent = null;

        //Play the particle system
        m_ExplosionParticles.Play();

        //Once the particles have finished, destroy the gameObject they are on
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        //Destroy the shell
        Destroy(gameObject);
    }

    float CalculateDamage(Vector3 pos)
    {
        return 0;
    }

}
