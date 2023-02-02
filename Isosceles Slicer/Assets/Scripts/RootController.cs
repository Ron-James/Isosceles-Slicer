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
    [SerializeField] bool isVulnerable = false;
    

    public bool IsVulnerable { get => isVulnerable; set => isVulnerable = value; }


    // Start is called before the first frame update
    void Start()
    {
        ChangeSize(_initialGrowth);
        StartCoroutine(GrowRootRoutine(_maxGrowthTime));
        
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeSize(rootFill);


    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public float CurrentRootLength()
    {
        return _currentGrowth;
    }


    public void ChangeSize(float amount)
    {
        float newFillAmount = amount;

        if (amount >= 1)
        {
            newFillAmount = 1;
            
        }
        else if (amount <= 0)
        {
            newFillAmount = 0;
            StartVulnerablePeriod(GameManager.instance.RootVulnerableTime);
        }
        _image.fillAmount = newFillAmount;
        _currentGrowth = newFillAmount;




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

    public void StartVulnerablePeriod(float period){
        StartCoroutine(SetVulnerableRoutine(period));
    }

    IEnumerator SetVulnerableRoutine(float period)
    {
        float time = 0;
        IsVulnerable = true;
        while (true)
        {
            if (time >= period)
            {
                IsVulnerable = false;
                break;
            }
            else
            {
                time += Time.deltaTime;
                yield return null;
            }
        }

    }

    IEnumerator GrowRootRoutine(float maxTime)
    {
        float time = 0;
        //float rate = (1 - _initialGrowth) / (maxTime / Time.deltaTime);
        while (true)
        {

            //float rate = (1 - _initialGrowth) / (maxTime / Time.deltaTime);
            float rate = (_currentGrowth) / ((maxTime*0.43478f) / Time.deltaTime);
            //Debug.Log("Rate is " + rate);
            if (_currentGrowth >= 1)
            {
                //Victory!
                GameManager.instance.Victory();
                Debug.Log("Victory!");
                Debug.Log("Time elapsed = " + time);
                break;
            }
            else
            {
                time += Time.deltaTime;
                if (!IsVulnerable)
                {
                    float newGrowth = _currentGrowth + rate;
                    ChangeSize(newGrowth);
                }

                yield return null;
            }
        }
    }


}
