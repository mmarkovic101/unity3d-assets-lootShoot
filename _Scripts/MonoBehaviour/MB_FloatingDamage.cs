using UnityEngine;
using UnityEngine.UI;

public class MB_FloatingDamage : MonoBehaviour
{
    [field:SerializeField] private GameObject _text;
    private float _positionX;
    void Start()
    {
        Destroy(gameObject, 0.25f);
        _positionX = _text.transform.position.x;
    }

    void Update()
    {
        if(_positionX < Input.mousePosition.x)
        {
            _text.transform.position += new Vector3(-0.75f, 1f, 0f);
        }
        else
        {
            _text.transform.position += new Vector3(0.75f, 1f, 0f);
        }
    }
}
