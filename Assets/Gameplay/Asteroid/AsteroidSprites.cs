using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroiSprites", menuName = "ScriptableObjects/AsteroidSprites", order = 1)]
public class AsteroidSprites : ScriptableObject
{
    public Sprite[] Sprites;
}
