using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootController : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] BoxCollider2D _collider;
    
    [Header("Growth Variables")]
    [Range(0, 1)][SerializeField] float _initialGrowth = 0.1f;
    [SerializeField] float _currentGrowth;
    [SerializeField] float _maxGrowthTime = 60;


    // Start is called before the first frame update
    void Start()
    {
        _currentGrowth = _initialGrowth;
        ChangeSize(_currentGrowth);
        StartCoroutine(GrowRootRoutine(_maxGrowthTime));
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeSize(rootFill);


    }
    private void OnDisable() {
        StopAllCoroutines();
    }

    public float CurrentRootLength()
    {
        return _image.fillAmount;
    }


    public void ChangeSize(float amount)
    {
        float newFillAmount = amount;

        if (amount > 1)
        {
            newFillAmount = 1;
        }
        else if (amount < 0)
        {
            newFillAmount = 0;
        }
        _image.fillAmount = newFillAmount;




        if (newFillAmount == 0)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
            Vector3 newScale = _collider.size;
            newScale.y = newFillAmount;
            float colliderHorizontalOffset = (newFillAmount) / 2;
            Vector2 newOffset = _collider.offset;
            newOffset.y = colliderHorizontalOffset;
            _collider.offset = newOffset;
            _collider.size = newScale;
        }


    }

    IEnumerator GrowRootRoutine(float maxTime)
    {
        float rate = (1 - _initialGrowth) / (maxTime / Time.deltaTime);
        while (true)
        {
            if (_currentGrowth >= 1)
            {
                //Victory!
                Debug.Log("Victory!");
                break;
            }
            else
            {
                _currentGrowth += rate;
                ChangeSize(_currentGrowth);
                yield return null;
            }
        }
    }


}
