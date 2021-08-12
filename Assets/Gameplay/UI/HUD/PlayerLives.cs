using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lives = new List<GameObject>();

    public void Refresh(int lives)
    {
        foreach (var item in _lives)
            item.SetActive(false);

        for (int i = 0; i < lives; i++)
            _lives[i].SetActive(true);
    }
}