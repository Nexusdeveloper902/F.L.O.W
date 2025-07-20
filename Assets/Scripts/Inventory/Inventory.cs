using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 20;
    public List<InventorySlot> slots = new();

    public Item iron;

    void Awake()
    {
        for (int i = 0; i < slotCount; i++)
            slots.Add(new InventorySlot());
    }
    
    public bool AddItem(Item item, int amount)
    {
        int remaining = amount;

        // 1) Try stacking in existing compatible slots
        foreach (var slot in slots)
        {
            if (slot.CanAdd(item))
            {
                remaining = slot.Add(item, remaining);
                if (remaining <= 0) return true;
            }
        }

        // 2) Then fill any empty slots
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                remaining = slot.Add(item, remaining);
                if (remaining <= 0) return true;
            }
        }

        return false; // If we still have remaining > 0, we ran out of space
    }

    public int RemoveItem(Item item, int amountRequested)
    {
        int removed = 0;

        // Try to remove from each slot that has the item
        foreach (var slot in slots)
        {
            if (slot.IsEmpty || slot.stack.item != item)
                continue;

            // Ask the slot to remove as much as it can, up to what remains to remove
            int thisRemoved = slot.Remove(amountRequested - removed);
            removed += thisRemoved;

            if (removed >= amountRequested)
                break;
        }

        return removed;
    }


    public int CountItem(Item item)
    {
        int total = 0;
        foreach (var slot in slots)
            if (!slot.IsEmpty && slot.stack.item == item)
                total += slot.stack.amount;
        return total;
    }
}