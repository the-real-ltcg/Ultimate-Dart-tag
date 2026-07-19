using UnityEngine;
using Unity.Netcode;

public class ColorController : NetworkBehaviour
{
    private NetworkVariable<Color> PlayerColor = new NetworkVariable<Color>();

    private Renderer _playerRenderer;

    private void Awake()
    {
        _playerRenderer = GetComponentInChildren<Renderer>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // Assign an initial random color
            SetPlayerColorServerRpc(Random.ColorHSV());
        }

        // Subscribe to color updates
        PlayerColor.OnValueChanged += OnColorChanged;

        // Immediately apply the current color for late joiners
        OnColorChanged(Color.clear, PlayerColor.Value);
    }

    public override void OnNetworkDespawn()
    {
        PlayerColor.OnValueChanged -= OnColorChanged;
    }

    private void Update()
    {
        if (!IsOwner) return;

        // Check for color change input
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetPlayerColorServerRpc(Random.ColorHSV());
        }
    }

    [ServerRpc]
    private void SetPlayerColorServerRpc(Color color)
    {
        PlayerColor.Value = color; // Synchronize color with all clients
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        if (_playerRenderer != null)
        {
            _playerRenderer.material.color = newColor; // Update the local renderer
        }
    }
}
