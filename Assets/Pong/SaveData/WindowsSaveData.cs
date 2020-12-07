using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsSaveData : AbstractSaveData
{
    public override string Load()
    {
        return "WindowsSaveData";
    }

    public override void Save(string payload)
    {
        Debug.Log("Windows save data saved!");
    }
}
