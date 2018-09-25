using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : Door
{
    public override void Move(bool input)
    {
        Debug.Log("move DoorMove " + doorType);
    }
}
