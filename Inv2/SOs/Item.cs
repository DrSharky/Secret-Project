using UnityEngine;
[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public bool CanDrop;
    public bool CanSell;
    public bool CanUse;
    public bool CanEquip;
    public GameObject groundItem;
    public Sprite InvPic;
    public Sprite uiDisplay;
    public Sprite uiDisplaySelect;
    public bool spinnable;
    public string infoTitle;
    [TextArea(15, 20)]
    public string description;

    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        CanDrop = item.data.CanDrop;
        CanSell = item.data.CanSell;
        CanUse = item.data.CanUse;
        CanEquip = item.data.CanEquip;
        groundItem = item.data.groundItem;
        spinnable = item.data.spinnable;
        infoTitle = item.data.infoTitle;
        InvPic = item.data.InvPic;
        uiDisplay = item.data.uiDisplay;
        uiDisplaySelect = item.uiDisplaySelect;
        description = item.data.description;
        buffs = new ItemBuff[item.data.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].Min, item.data.buffs[i].Max)
            {
                stat = item.data.buffs[i].stat
            };
        }
    }
}
