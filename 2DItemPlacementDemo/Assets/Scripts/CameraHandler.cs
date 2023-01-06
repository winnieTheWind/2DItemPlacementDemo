using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CameraHandler : MonoBehaviour
{
    public Camera cam;
    public InputAction handlerControls;
    private Vector2 moveDir = Vector2.zero;
    public Rigidbody2D rb;

    public float moveSpeed = 5f;

    private float minX = 11.53f;
    private float maxX = 23.28f;
    private float minY = 8.64f;
    private float maxY = 23.17f;

    private GameObject groundLayer;
    private Tilemap layer;

    private Vector3 _cam;

    private void OnEnable() {
        handlerControls.Enable();
    }

    void Update()
    {
        moveDir = handlerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        Vector2 force = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        rb.AddForce(force);
    }
}
