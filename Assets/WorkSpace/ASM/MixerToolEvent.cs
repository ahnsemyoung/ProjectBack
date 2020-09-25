﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerToolEvent : MonoBehaviour
{
    [SerializeField, Range(0.01f, 1f)]
    private float CookingSpeed = 0.1f;

    [SerializeField]
    private GameObject Juice = null;

    private Dictionary<int, Transform> _ingredientTransform = new Dictionary<int, Transform>();
    private MeshRenderer _juiceMeshRenderer = null;
    private Color _originalColor;

    // Start is called before the first frame update
    void Start()
    {
        if (Juice == null) Debug.Log("MixerToolEvent: No Inspector Value");

        _juiceMeshRenderer = Juice.GetComponentInChildren<MeshRenderer>();
        _originalColor = _juiceMeshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        MixerAction();
    }
    //재료: 크기 작아지고 삭제, 물: 차오르면서 재료색으로 변형
    private void MixerAction()
    {
        if (_ingredientTransform.Count != 0)
        {
            foreach (Transform targetTranform in _ingredientTransform.Values)
            {

                if (targetTranform != null)
                {
                    //크기가 줄어든다
                    targetTranform.localScale += new Vector3(-1, -1, -1) * Time.deltaTime * CookingSpeed;
                    //특정 크기 밑이 되면 삭제
                    if (targetTranform.localScale.y < 0.01f)
                    {
                        Destroy(targetTranform.gameObject);
                        print("Destroy");
                    }
                    //가능하면 물색도 같이 바꿔주기 해당 재료색으로
                    _juiceMeshRenderer.material.color = Color.Lerp(_juiceMeshRenderer.material.color, targetTranform.GetComponent<MeshRenderer>().material.color, Time.deltaTime * CookingSpeed);
                    // 액체를 채워줍시다     
                    Vector3 juiceEmty = Juice.transform.localScale;
                    Vector3 juiceFull = juiceEmty + Vector3.up;
                    Juice.transform.localScale = Vector3.Lerp(juiceEmty, juiceFull, Time.deltaTime * CookingSpeed);

                }
            }

        }
    }

    //충돌감지 
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Ingredient")
        {
            _ingredientTransform[other.transform.GetInstanceID()] = other.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _ingredientTransform.Remove(collision.transform.GetInstanceID());
    }
}
