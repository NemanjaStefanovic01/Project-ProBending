using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCombat : NetworkBehaviour
{
    Transform earthWall;
    Transform earthBall;
    
    [Header("Elemental Abilities")]
    [SerializeField]
    Transform earthWallPrefab;
    [SerializeField]
    Transform earthBallPrefab;

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
            SpawnObjectServerRPC(1);

        } else if (Input.GetKeyDown(KeyCode.E)) //Ball
        {
            SpawnObjectServerRPC(2);

        } else if (Input.GetMouseButtonDown(0)) //Punch
        {
            PunchAbbilityServerRpc();

        }

        //Ukoliko zelim da despawnujem nesto
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            Destroy(earthWall.gameObject);
            earthWall.GetComponent<NetworkObject>().Despawn(true); //Despawn it from network
        }
    }

    [ServerRpc]
    private void SpawnObjectServerRPC(int ability)
    {
        switch (ability)
        {
            case 1:
                earthWall = Instantiate(earthWallPrefab, abilitySpawnPos.position, abilitySpawnPos.rotation);
                earthWall.GetComponent<NetworkObject>().Spawn(true); //Spawns it on network
                break;
            case 2:
                earthBall = Instantiate(earthBallPrefab, abilitySpawnPos.position, abilitySpawnPos.rotation);
                earthBall.GetComponent<NetworkObject>().Spawn(true); //Spawns it on network
                break;

            default: break;
        }
        
    }

    [ServerRpc]
    private void PunchAbbilityServerRpc()
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, raycastRange))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }
    }
}
