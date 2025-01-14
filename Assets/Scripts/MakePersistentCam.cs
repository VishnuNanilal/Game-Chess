﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePersistentCam : MonoBehaviour
{
    private static MakePersistentCam instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
