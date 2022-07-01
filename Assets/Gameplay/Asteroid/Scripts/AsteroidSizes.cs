using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidSizes", menuName = "ScriptableObjects/AsteroidSizes", order = 1)]
public class AsteroidSizes : ScriptableObject
{
    public float AsteroidLargeScale = 1;
    public float AsteroidMediumScale = .75f;
    public float AsteroidSmallScale = .5f;
}