using UnityEngine;

public class RandomAnimationInterval : MonoBehaviour
{
    public float minInterval = 5f;
    public float maxInterval = 10f;

    private Animator _animator;
    private float _nextAnimationTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _nextAnimationTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (Time.time > _nextAnimationTime)
        {
            _animator.Play("Random");
            _nextAnimationTime = Time.time + Random.Range(minInterval, maxInterval);
        }
    }
}