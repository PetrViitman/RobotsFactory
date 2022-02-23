public class GFireflyViewPool : GView
{
	private GFireflyView[] fireflyView_gfv_arr;
	private int length_int;

	public GFireflyViewPool(int aMaximalFirefliesNumber_int)
		: base()
	{	
		this.fireflyView_gfv_arr = new GFireflyView[aMaximalFirefliesNumber_int];
	}

	public void drop()
	{
		this.length_int = 0;
	}

	public int length()
	{
		return this.length_int;
	}

	public void add(float aX_num, float aY_num)
	{
		if(!this.isFull())
		{
			GFireflyView firefly_gfv = this.getNextFirefly();
			firefly_gfv.setXY(aX_num, aY_num);
			firefly_gfv.reset();
		}
	}

	public void delete(int aIndex_int)
	{
		this.fireflyView_gfv_arr[aIndex_int].copy(this.fireflyView_gfv_arr[this.length_int-1]);
		this.length_int--;
	}

	public GFireflyView getFirefly(int aIndex_int)
	{
		return this.fireflyView_gfv_arr[aIndex_int];
	}

	private GFireflyView getNextFirefly()
	{
		int index_int = this.length_int;

		if(this.fireflyView_gfv_arr[index_int] == null)
		{
			this.fireflyView_gfv_arr[index_int] = new GFireflyView();
		}

		this.length_int++;

		return this.fireflyView_gfv_arr[index_int];
	}

	public bool isFull()
	{
		return this.length_int == this.fireflyView_gfv_arr.Length - 1;
	}
}