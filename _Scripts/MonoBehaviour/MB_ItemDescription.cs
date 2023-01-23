using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MB_ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private GameObject _itemDescriptionPrefab;
    private GameObject _itemDescription;
    private GameObject _parentGameObject;

    void Start()
    {
        _parentGameObject = GameObject.FindGameObjectWithTag("Inventory");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<MB_Slot>().Item != null)
        {
            _itemDescription = Instantiate(_itemDescriptionPrefab, this.transform.position, Quaternion.identity, _parentGameObject.transform);
            _itemDescription.GetComponentInChildren<Text>().text = GetComponent<MB_Slot>().Item.GetItemDescriptionDetailed();
            _itemDescription.transform.position = transform.position + Vector3.right * (-100f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_itemDescription);
    }
}
