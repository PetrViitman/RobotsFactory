public class GColor
{
	private int red_int;
	private int green_int;
	private int blue_int;

	public GColor(int aRed_int, int aGreen_int, int aBlue_int)
	{
		this.red_int = aRed_int;
		this.green_int = aGreen_int;
		this.blue_int = aBlue_int;
	}

	public void copy(GColor aColor_gc)
	{
		this.setRGB(
			aColor_gc.getRed(),
			aColor_gc.getGreen(),
			aColor_gc.getBlue());
	}

	public void setRGB(int aRed_int, int aGreen_int, int aBlue_int)
	{
		this.red_int = aRed_int;
		this.green_int = aGreen_int;
		this.blue_int = aBlue_int;
	}

	public int getRed()
	{
		return this.red_int;
	}

	public int getGreen()
	{
		return this.green_int;
	}

	public int getBlue()
	{
		return this.blue_int;
	}

	public bool isEqual(GColor aColor_gc)
	{
		return (
			this.red_int == aColor_gc.getRed() &&
			this.green_int == aColor_gc.getGreen() &&
			this.blue_int == aColor_gc.getBlue());
	}
}