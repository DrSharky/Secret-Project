[System.Serializable]
public class Attr
{
    [System.NonSerialized]
    public PlayerStats parent;
    public StatId type;
    public ModifiableInt value;
    public void SetParent(PlayerStats _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}