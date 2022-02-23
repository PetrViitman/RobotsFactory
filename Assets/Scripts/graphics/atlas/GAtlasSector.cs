using UnityEngine;

public class GAtlasSector
{
	private Sprite sprite_s;
	private string id_str;

	public GAtlasSector(string aId_str, Sprite aSprite_s)
	{
		this.id_str = aId_str;
		this.sprite_s = aSprite_s;
	}

	public Sprite getSprite()
	{
		return this.sprite_s;
	}

	public bool hasMatchingId(string aId_str)
	{
		return this.id_str == aId_str;
	}
}