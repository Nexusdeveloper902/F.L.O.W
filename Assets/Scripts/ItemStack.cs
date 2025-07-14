[System.Serializable]
public class ItemStack
{
    public Item item;
    public int amount;

    public ItemStack(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public bool IsFull => amount >= item.maxStackSize;
    public int SpaceLeft => item.maxStackSize - amount;
}