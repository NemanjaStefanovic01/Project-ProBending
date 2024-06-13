using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerCombat : NetworkBehaviour
{
    Transform earthWall;
    Transform earthBall;
    Transform earthMole;
    
    [Header("Elemental Abilities")]
    [SerializeField]
    Transform earthWallPrefab;
    [SerializeField]
    Transform earthBallPrefab;
    [SerializeField]
    Transform earthMolePrefab;

    [Header("References")]
    [SerializeField]
    Transform abilitySpawnPos;
    [SerializeField]
    Camera cam;

    [Header("Shooting Abbilities")]
    public float raycastRange;

    private void Update()
    {
        if(!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Q)) //Wall
        {
            SpawnObjectServerRPC(1, GetLocalCameraOrientationAndDirection()[1]);

        } else if (Input.GetKeyDown(KeyCode.E)) //Ball
        {
            SpawnObjectServerRPC(2, GetLocalCameraOrientationAndDirection()[1]);

        }else if(Input.GetKeyDown(KeyCode.R)) //Mole
        {
            SpawnObjectServerRPC(3, GetLocalCameraOrientationAndDirection()[1]);

        } else if (Input.GetMouseButtonDown(0)) //Punch
        {
            Vector3 orientation = GetLocalCameraOrientationAndDirection()[0];
            Vector3 direction = GetLocalCameraOrientationAndDirection()[1];

            PunchAbbilityServerRpc(orientation, direction);
        }

        //Ukoliko zelim da despawnujem nesto
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            Destroy(earthWall.gameObject);
            earthWall.GetComponent<NetworkObject>().Despawn(true); //Despawn it from network
        }
    }

    List<Vector3> GetLocalCameraOrientationAndDirection()
    {
        List<Vector3> ret = new List<Vector3>();

        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        ret.Add(origin);
        ret.Add(direction);

        return ret;
    }

    [ServerRpc]
    private void SpawnObjectServerRPC(int ability, Vector3 direction)
    {
        switch (ability)
        {
            case 1:
                earthWall = Instantiate(earthWallPrefab, abilitySpawnPos.position, abilitySpawnPos.rotation);
                earthWall.GetComponent<NetworkObject>().Spawn(true); //Spawns it on network
                break;
            case 2:
                earthBall = Instantiate(earthBallPrefab, abilitySpawnPos.position, abilitySpawnPos.rotation);
                earthBall.GetComponent<NetworkObject>().Spawn(true); 
                break;
            case 3:
                Vector3 spawnPos = new Vector3(abilitySpawnPos.position.x, abilitySpawnPos.position.y + 0.2f, abilitySpawnPos.position.z);
                earthMole = Instantiate(earthMolePrefab, spawnPos, abilitySpawnPos.rotation);
                earthMole.GetComponent<NetworkObject>().Spawn(true);
                earthMole.GetComponent<EarthMole>().direction = direction;
                break;
            default: break;
        }
    }

    [ServerRpc]
    private void PunchAbbilityServerRpc(Vector3 origin, Vector3 direction)
    {
        Debug.DrawRay(origin, direction * raycastRange, Color.red, 2.0f);

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, raycastRange))
        {
            //Debug.Log("Hit object: " + hit.collider.name);
            hit.transform.GetComponent<IAbility>().OnPunch(direction);
        }
    }

    // Ovo ne radi zato sto nemogu da saljem Transform putem rpc-a

    //[ServerRpc]
    //private void SendDirectionToAbilityServerRpc(Transform obj, Vector3 dir)
    //{
    //    obj.GetComponent<EarthMole>().SetDirection(dir);
    //}
}
