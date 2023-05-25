using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    [SerializeField] GameObject winPanal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") winPanal.SetActive(true);

    }
}
