using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMole : MonoBehaviour, IAbility
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Vars")]
    public float movementSpeed;
    public float knockupForce;

    [HideInInspector]
    public Vector3 direction;

    void Update()
    {
        rb.velocity = new Vector3(direction.x * movementSpeed, rb.velocity.y, direction.z * movementSpeed);
    }

    //public void SetDirection(Vector3 dir)
    //{
    //    direction = dir;
    //}

    public void OnPunch(Vector3 direction)
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerMovement>().KnockupPlayer(knockupForce);
        }
    }
}
