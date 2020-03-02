using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    protected void Awake()
    {
        Instance = this;
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
    }
}
