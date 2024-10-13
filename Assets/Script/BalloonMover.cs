using BalloonGame;
using UnityEngine;

public class BalloonMover : MonoBehaviour
{
    // Reference to the Rigidbody2D component for controlling the balloon's physics
    private Rigidbody2D rb2D;
    // Speed at which the balloon moves upwards
    private float _speed;

    private void Awake()
    {
        // Get the Rigidbody2D component attached to this balloon
        rb2D = GetComponent<Rigidbody2D>();
        // Get the balloon speed from the GameControl instance
        _speed = GameControl.instance.Speed;
    }

    private void FixedUpdate()
    {
        // Move the balloon upwards at the specified speed (scaled by Time.fixedDeltaTime for consistent movement)
        rb2D.velocity = Vector2.up * _speed * Time.fixedDeltaTime;
    }

    // Method to stop the balloon's movement
    public void StopTheBalloon()
    {
        // Set the balloon's velocity to zero to stop its movement
        rb2D.velocity = Vector2.zero;
        // Disable this script to stop further updates to the balloon's movement
        enabled = false;
    }
}
