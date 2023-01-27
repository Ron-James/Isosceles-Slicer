using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] AnimationCurve _tweenCurve;
    [SerializeField] float _tweenDuration = 0.1f;
    [SerializeField] Vector3 _offset = new Vector3(0, 0, 10);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _player.transform.position + _offset;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void onMovementEnter()
    {

    }

    public void MoveToPlayer()
    {
        StartCoroutine(MoveToPlayerRoutine(_tweenDuration));
    }
    IEnumerator MoveToPlayerRoutine(float duration)
    {
        float time = 0;
        while (true)
        {
            if (time >= duration)
            {
                break;
            }
            else
            {
                yield return null;
            }

        }

    }
}
