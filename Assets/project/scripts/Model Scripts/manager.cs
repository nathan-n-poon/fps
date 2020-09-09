using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class manager : MonoBehaviour
{
    public string playerPrefab;

    private void Start()
    {
        spawn();
    }

    public void spawn()
    {
        PhotonNetwork.Instantiate()
    }
}
