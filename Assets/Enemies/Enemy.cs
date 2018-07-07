using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float secondsBetweenShots = 0.5f;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 4f;
    [SerializeField] float chaseRadius = 6f;

    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;

    [SerializeField] Vector3 aimOffset = new Vector3(0, 1, 0);

    float currentHealthPoints = 100f;
    AICharacterControl aICharacterControl = null;
    GameObject player = null;

    bool isAttacking = false;

    public float HealthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();

        projectileComponent.SetDamage(damagePerShot);

        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;

        float projectileSpeed = projectileComponent.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aICharacterControl = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0, secondsBetweenShots);
        }

        if (distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            CancelInvoke("SpawnProjectile");
        }

        if (distanceToPlayer <= chaseRadius)
        {
            aICharacterControl.SetTarget(player.transform);
        }
        else
        {
            aICharacterControl.SetTarget(transform);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = new Color(0, 0f, 255, .5f);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
