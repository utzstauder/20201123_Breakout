using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSaveData
{
    public abstract void Save(string payload);
    public abstract string Load();
}
