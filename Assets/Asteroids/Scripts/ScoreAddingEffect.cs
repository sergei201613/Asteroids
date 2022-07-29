using TeaGames.MonoBehaviourExtensions;
using TMPro;
using UnityEngine;

public class ScoreAddingEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(int score)
    {
        _text.text = "+" + score;
    }

    private void Awake()
    {
        this.Delay(_lifeTime, () =>
        {
            Destroy(gameObject);
        });
    }
}