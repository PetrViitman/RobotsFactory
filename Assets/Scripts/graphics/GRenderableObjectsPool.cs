using UnityEngine;

public class GRenderableObjectsPool
{
	private const int DEFAULT_MAXIMAL_OBJECTS_NUMBER = 250;

	private Sprite sprite_s;
	private GameObject[] objects_go_arr;
	private int objectsTotalNumber_int;
	private int objectsCurrentNumber_int;

	public GRenderableObjectsPool(Sprite aSprite_s, int aMaximalObjectsNumber_int)
	{
		this.sprite_s = aSprite_s;
		this.objects_go_arr = new GameObject[aMaximalObjectsNumber_int];
		this.objectsTotalNumber_int = 0;
	}

	public GRenderableObjectsPool(Sprite aSprite_s)
		: this(aSprite_s, GRenderableObjectsPool.DEFAULT_MAXIMAL_OBJECTS_NUMBER)
	{

	}

	public Sprite getSprite()
	{
		return this.sprite_s;
	}

	public void addObject(GameObject aObject_go)
	{
		if(this.objectsTotalNumber_int == this.objects_go_arr.Length - 1)
		{
			return;
		}

		this.objects_go_arr[this.objectsTotalNumber_int] = aObject_go;
		this.objectsTotalNumber_int++;
	}

	public GameObject getNextRenderableObject()
	{
		GameObject object_go = this.objects_go_arr[this.objectsCurrentNumber_int];
		this.objectsCurrentNumber_int++;

		return object_go;
	}

	public void drop()
	{
		this.objectsCurrentNumber_int = 0;
	}
}