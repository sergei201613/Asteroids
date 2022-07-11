using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lives = new();

    public void Refresh(int lives)
    {
        foreach (var item in _lives)
            item.SetActive(false);

        for (int i = 0; i < lives; i++)
            _lives[i].SetActive(true);
    }
}