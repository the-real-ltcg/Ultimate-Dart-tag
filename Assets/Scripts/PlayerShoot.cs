using Unity.Netcode;
using UnityEngine;

public class DartController : NetworkBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector
    public Transform firePoint;        // The position to fire the projectile from
    public float projectileSpeed = 10f;

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T)) // Throw dart
        {
            ShootProjectileServerRpc();
        }
    }

    [ServerRpc]
    private void ShootProjectileServerRpc()
    {
        // Make a start position for the projectile that's slightly in front of the player, and slightly above the ground.
        var startPosition = firePoint.position + firePoint.forward * 1.5f + firePoint.up * 1.5f;

        // Make s starting rotation for the projectile that's the same as the firePoint's rotation, 
        // but a little up in direction so the projectile doesn't hit the ground immediately.
        var startRotation = firePoint.rotation * Quaternion.Euler(270, 0, 0);

        GameObject projectile = Instantiate(projectilePrefab, startPosition, startRotation);

        projectile.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * projectileSpeed;

        // From OwnerClientId with love...
        projectile.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);

    }
}
