﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] food;
    private int count;
    private bool win = false;
    public Text text;

    void Start()
    {
        food = GameObject.FindGameObjectsWithTag("Food");
        count = food.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == count - 1)
        {
           
            food.OrderBy(quads => quads.transform.position.y).ToArray();

            GameObject highest = GameObject.FindGameObjectsWithTag("Food").OrderByDescending(go => go.transform.position.y).First();
            Debug.Log(highest.name+highest.transform.position.y);
            if (highest.name.Equals("Bread") || highest.name.Equals("Bread1"))
            {
                Debug.Log("WİN");

                win = true;
                text.enabled = true;
            }
        }
    }

}