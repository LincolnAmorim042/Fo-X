using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update(){
        transform.position = new Vector3(0 + offset.x, player.position.y + offset.y, offset.z);
    }
}
