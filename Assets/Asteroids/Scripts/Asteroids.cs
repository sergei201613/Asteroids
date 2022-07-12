using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroids", menuName = "ScriptableObjects/Asteroids", order = 1)]
public class Asteroids : ScriptableObject
{
    public List<Asteroid> BigAsteroids;
    public List<Asteroid> MediumAsteroids;
    public List<Asteroid> SmallAsteroids;
}