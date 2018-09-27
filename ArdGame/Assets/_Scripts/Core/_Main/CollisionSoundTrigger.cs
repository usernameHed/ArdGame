using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundTrigger : MonoBehaviour {

	private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name != "BackGround")
            SoundManager.Instance.PlayRandomCollision();
    }
}
