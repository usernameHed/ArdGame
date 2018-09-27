using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpin : Door
{
    [SerializeField]
    private Transform doorToSpin;

    private float desiredRot;
    public float rotSpeed = 250;
    public float damping = 10;

    private void OnEnable()
    {
        desiredRot = doorToSpin.eulerAngles.y;
    }

    private void Start()
    {
        ArdManager.Instance.AddDoor(this);
    }

    public override void Move(float input)
    {
        Debug.Log("move doorSpin");

        desiredRot += rotSpeed * Time.deltaTime * input;


        var desiredRotQ = Quaternion.Euler(doorToSpin.eulerAngles.x, desiredRot, doorToSpin.eulerAngles.z);
        doorToSpin.rotation = Quaternion.Lerp(doorToSpin.rotation, desiredRotQ, Time.deltaTime * damping);
    }
}
