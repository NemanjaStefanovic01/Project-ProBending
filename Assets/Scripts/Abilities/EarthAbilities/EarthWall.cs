using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour, IAbility
{
    [Header("References")]
    public Rigidbody rb;

    public void OnPunch()
    {
        rb.velocity = Vector3.up * 10;
    }
}
