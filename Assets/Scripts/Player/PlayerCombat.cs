using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCombat : NetworkBehaviour
{
    [Header("Elemental Abilities")] //Zasto ovaj header ne radi haha
    Transform earthWall;
    Transform earthBall;
    //prefabs
    [SerializeField]
    Transform earthWallPrefab;
    [SerializeField]
    Transform earthBallPrefab;

    [Header("References")]
    [SerializeField]
    Transform abilitySpawnPos;
    [SerializeField]

    private void Update()
    {
        if(!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnObjectServerRPC(1);

        } else if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnObjectServerRPC(2);
        }

        //Ukoliko zelim da despawnujem nesto
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            Destroy(earthWall.gameObject);
            earthWall.GetComponent<NetworkObject>().Despawn(true); //Despawn it from network

            //Za abilitije u NetworkObject od komponente moram otkacitiDont Destroy With Owner jer ako owner izadje unisti ce objekat
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
}
