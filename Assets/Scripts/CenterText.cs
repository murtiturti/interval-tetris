using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private Camera _mainCamera;

    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.rectTransform.position = _mainCamera.WorldToScreenPoint(transform.position + offset);
    }
}
