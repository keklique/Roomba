using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : SingletonPersistent<LevelManager>
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
