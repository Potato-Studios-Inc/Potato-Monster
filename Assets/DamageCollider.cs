using Player;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private bool _playerHit;
    public playerHealth player;
    public Camera camera;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        _animator.enabled = camera.isActiveAndEnabled;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        player.TakeDamage(25);
    }
    
    // Function is used by the animation event
    public void play_sound()
    {
        _audioSource.Play();
    }
}
