﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(scene);
    }
}
