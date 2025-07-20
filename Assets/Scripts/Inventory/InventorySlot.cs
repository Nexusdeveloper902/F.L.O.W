using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemStack stack;

    public bool IsEmpty => stack == null || stack.amount <= 0;

    public bool CanAdd(Item item) =>
        IsEmpty || (stack.item == item && !stack.IsFull);

    public int Add(Item item, int amount)
    {
        if (!CanAdd(item)) return amount;

        if (IsEmpty)
        {
            stack = new ItemStack(item, Mathf.Min(amount, item.maxStackSize));
            return amount - stack.amount;
        }
        else
        {
            int toAdd = Mathf.Min(stack.SpaceLeft, amount);
            stack.amount += toAdd;
            return amount - toAdd;
        }
    }

    public int Remove(int amount)
    {
        if (IsEmpty) return 0;

        int toRemove = Mathf.Min(amount, stack.amount);
        stack.amount -= toRemove;
        if (stack.amount <= 0) stack = null;
        return toRemove;
    }
}

