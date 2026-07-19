using System;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<ThirdPersonController>();
            if (player != null)
            {
                player.TagPlayerServerRpc(true);
            }
        }

        // Stop the projectile and disable its collider to simulate falling
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
//        GetComponent<Rigidbody>().useGravity = true;
        // GetComponent<Collider>().enabled = false;

        // Notify all clients to destroy the projectile
        DestroyProjectileClientRpc();
    }

    [ClientRpc]
    private void DestroyProjectileClientRpc()
    {
        Destroy(gameObject, 2f);
    }
}
