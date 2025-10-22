using UnityEngine;

public class SizeActivator : MonoBehaviour
{
    public float targetSize = 0.75f;
    public float cooldown = 5f;

    private GameObject _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            _player.transform.localScale =  Vector3.one * targetSize;

            Invoke(nameof(ResetScale), cooldown);
        }
    }

    void ResetScale()
    {
        _player.transform.localScale = Vector3.one;
    }
}
