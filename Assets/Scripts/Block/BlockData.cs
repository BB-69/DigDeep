public enum BlockType
{
    Empty,
    Normal,
    Copper,
    Iron,
    Gold,
    Emerald
}
[System.Serializable]
public class BlockData
{
    public BlockType blockType;
    public int hp;
    public bool canDig;

    public BlockData(BlockType blockType, bool canDig, int hp = 0)
    {
        this.blockType = blockType;
        this.canDig = canDig;
        this.hp = hp;
    }
}