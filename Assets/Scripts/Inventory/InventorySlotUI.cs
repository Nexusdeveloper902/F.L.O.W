using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;

    public void Set(ItemStack stack)
    {
        if (stack == null || stack.item == null || stack.amount <= 0)
        {
            iconImage.enabled = false;
            countText.text = "";
            return;
        }

        iconImage.enabled = true;
        iconImage.sprite = stack.item.icon;
        countText.text = stack.amount > 1 ? stack.amount.ToString() : "";
    }
}
