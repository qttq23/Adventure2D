﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int numCoin = 0;

    AudioSource audioSource;

    void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // used by UI such as CoinBar
    public delegate void OnCoinChanged(int newValue);
    public event OnCoinChanged EventCoinChanged;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable item = collision.gameObject.GetComponent<Collectable>();
        if (item && item.type == Collectable.ItemType.COIN)
        {
            changeNumCoin(1);

            Destroy(collision.gameObject);

            if (audioSource)
            {

                audioSource.PlayOneShot(audioSource.clip);
            }
        }

    }

    void changeNumCoin(int value)
    {
        numCoin = numCoin + value;
        // signal
        EventCoinChanged?.Invoke(numCoin);
        print("Inventory.cs: numCoin: " + numCoin);
    }

}
