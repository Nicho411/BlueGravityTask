using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    public Transform exitPoint;
    [SerializeField] private GameObject _currentCamera;
    [SerializeField] private GameObject _newCamera;

    public void Teleport(GameObject player)
    {
        player.transform.position = exitPoint.position;
        _currentCamera.SetActive(false);
        _newCamera.SetActive(true);
    }
}
