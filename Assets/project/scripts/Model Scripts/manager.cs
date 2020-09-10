using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class manager : MonoBehaviour
{
    string playerPrefab = "Character";
    Transform spawnTransform;

    private void Start()
    {
        spawnTransform = gameObject.GetComponent<Transform>();
        spawn();
    }

    public void spawn()
    {
        PhotonNetwork.Instantiate("Character", spawnTransform.position, spawnTransform.rotation);
    }
}
