using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveRespawn : MonoBehaviour
{
    private bool active = false;
    public teleportScript player;
    // Update is called once per frame
    void Update()
    {
        if (!active && this.gameObject.activeSelf)
        {
            active = true;
            player.IsPoolRemoved = true;
        }
    }
}
