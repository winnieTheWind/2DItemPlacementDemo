using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGroundHandler : MonoBehaviour
{
    public bool IsObjectTouchingGround = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            IsObjectTouchingGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            IsObjectTouchingGround = false;
        }
    }

}
