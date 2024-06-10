using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBall : MonoBehaviour, IAbility
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Vars")]
    public float startJumpForce;
    public float punchForce;
    public float upwardDirectionModifier;

    private void Start()
    {
        rb.AddForce(Vector3.up * startJumpForce, ForceMode.Impulse);
    }

    public void OnPunch(Vector3 direction)
    {
        Vector3 punchTrajectory = new Vector3(direction.x, direction.y + upwardDirectionModifier, direction.z);
        rb.AddForce(punchTrajectory * punchForce, ForceMode.Impulse);
    }
}
