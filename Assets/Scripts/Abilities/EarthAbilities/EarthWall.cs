using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour, IAbility
{
    [Header("References")]
    public Rigidbody rb;
    public Collider col;

    private void Start()
    {
        
    }

    public void OnPunch(Vector3 direction)
    {
        rb.velocity = Vector3.up * 10;
    }
}
