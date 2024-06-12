using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour, IAbility
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Vars")]
    public float punchForce;
    private void Start()
    {
        
    }

    public void OnPunch(Vector3 direction)
    {
        Vector3 punchTrajectory = new Vector3(direction.x, rb.velocity.y, direction.z);
        rb.AddForce(punchTrajectory * punchForce, ForceMode.Impulse);
    }
}
