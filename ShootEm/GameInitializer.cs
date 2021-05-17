using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour 
{
	void Awake()
    {
        // initialize utils
        ScreenUtils.Initialize();
        ObjectPool.Initialize();
    }
}
