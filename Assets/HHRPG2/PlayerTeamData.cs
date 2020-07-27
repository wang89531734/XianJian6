using SoftStar.Item;
using SoftStar.Pal6;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerTeamData
{
	public bool Enqueue = true;

	public int HP;

	public int mCharacterID = -1;

	public int mLevel = 1;

    public List<PalNPC.SkillInfo> m_SkillIDs = new List<PalNPC.SkillInfo>();

    public List<ItemD> m_ItemIDs = new List<ItemD>();
}
