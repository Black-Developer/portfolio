using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject floatingDamagePrefab;
    public float maxHealth;
    public float currentHealth;
    Ragdoll ragdoll;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public float hitIntensity;
    public float hitDuration;
    private float hitTimer;
    private void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies)
        {
            HitBox hitbox = rigidBody.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(floatingDamagePrefab)
        {
            ShowDamage(damage);
        }
        hitTimer = hitDuration;
    }

    private void Update()
    {
        if (skinnedMeshRenderer != null)
        {
            hitTimer -= Time.deltaTime;
            float lerp = Mathf.Clamp01(hitTimer / hitDuration);
            float intensity = lerp * hitIntensity;
            skinnedMeshRenderer.material.color = Color.red * intensity + Color.white * 0.9f;
        }
    }
    void ShowDamage(float damage)
    {
        var obj = Instantiate(floatingDamagePrefab,transform.position, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMesh>().text = damage.ToString();
    }
}
