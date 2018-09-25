using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMono<PlayerController>
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speedPlayer = 5;

    private float horiz = 0;
    private float verti = 0;

    private bool hasMoved = false;

    private void InputPlayer()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        verti = Input.GetAxisRaw("Vertical");

        if (horiz != 0 || verti != 0)
        {
            hasMoved = true;
        }
    }

    private void MovePlayer()
    {
        Vector3 dirPlayer = new Vector3(horiz, 0, verti);
        UnityMovement.MoveTowards_WithPhysics(rb, dirPlayer, speedPlayer);
    }

    private void Update()
    {
        InputPlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}
