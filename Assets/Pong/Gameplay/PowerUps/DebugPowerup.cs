using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPowerup : Powerup
{
    public string message = "DebugPowerup activated!";

    public override void Activate()
    {
        Debug.Log(message);
    }
}
