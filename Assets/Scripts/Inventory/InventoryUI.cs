using UnityEngine;
using UnityEngine.UI;
using TMPro;  // only if you’re using TextMeshPro

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    public Inventory inventory;              // your Inventory component
    public GameObject slotPrefab;            // the InventorySlot prefab
    public Transform slotsParent;            // InventoryPanel (GridLayoutGroup)

    private InventorySlotUI[] slotUIs;

    void Start()
    {
        // 1) Instantiate one UI element per slot
        int slotCount = inventory.slots.Count;
        slotUIs = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            slotUIs[i] = go.GetComponent<InventorySlotUI>();
        }

        // 2) Initial draw
        RefreshUI();
    }

    void Update()
    {
        // If you want real‑time updates as inventory changes:
        RefreshUI();
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            var data = inventory.slots[i].stack;
            slotUIs[i].Set(data);
        }
    }
}
