﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMono<PlayerController>
{
    [SerializeField]
    private Rigidbody rb;                   //link to rigidbody
    [SerializeField]
    private float speedPlayer = 5;          //speed of player
    [SerializeField]
    private Animator animator;

    private float horiz = 0;                //move horiz input
    private float verti = 0;

    private bool hasMoved = false;          //tell if player has input moved in the current frame
    private bool enabledScript = true;      //tell if this script should be active or not
    private bool firstMove = false;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        enabledScript = true;               //active this script at start
        animator.SetBool("Dead", false);
        firstMove = false;
    }

    /// <summary>
    /// manage input player (in uptate)
    /// if we move: set hasMoved to true
    /// </summary>
    private void InputPlayer()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        verti = Input.GetAxisRaw("Vertical");

        hasMoved = (horiz != 0 || verti != 0);
    }

    /// <summary>
    /// get called when Arduino send data
    /// </summary>
    /// <param name="x">from -1 to 1</param>
    /// <param name="y">from -1 to 1</param>
    public void InputPlayerArduino(float x, float y)
    {
        horiz = x;
        verti = y;

        hasMoved = (horiz != 0 || verti != 0);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
        if (hasMoved)
        {
            if (!firstMove)
            {
                firstMove = true;
                EventManager.TriggerEvent(GameData.Event.PlayerMove);
            }
            Vector3 dirPlayer = new Vector3(horiz, 0, verti);
            UnityMovement.MoveByForcePushing_WithPhysics(rb, dirPlayer, speedPlayer);
        }
    }

    /// <summary>
    /// called when the game is over: desactive player
    /// </summary>
    private void GameOver()
    {
        Debug.Log("game over !!");
        enabledScript = false;
        animator.SetBool("Dead", true);
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript)
            return;

        SoundManager.Instance.SetBallSpeed((rb.velocity.magnitude - 2) / 10);
        if (ArdManager.Instance.enableKeyboard)
            InputPlayer();
    }

    /// <summary>
    /// handle move physics
    /// </summary>
    private void FixedUpdate()
    {
        if (!enabledScript)
            return;
        MovePlayer();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
