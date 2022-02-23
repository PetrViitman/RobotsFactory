using UnityEngine;

public class GFirefliesView: GView
{
	private const int MAXIMAL_FIREFLIES_NUMBER = 5;
	private const int FIREFLY_GENERATION_INTERVAL_IN_FRAMES = 15;

	private GFireflyViewPool fireflies_gfvp;
	private int frameCounter_int;

	public GFirefliesView()
		: base()
	{	
		this.fireflies_gfvp = new GFireflyViewPool(GFirefliesView.MAXIMAL_FIREFLIES_NUMBER);
		this.frameCounter_int = 0;

		//GENERATE ALL FIREFLIES ON START...
		GFireflyViewPool fireflies_gfvp = this.fireflies_gfvp;
		fireflies_gfvp.drop();

		for( int i = 0; i < GFirefliesView.FIREFLY_GENERATION_INTERVAL_IN_FRAMES * GFirefliesView.FIREFLY_GENERATION_INTERVAL_IN_FRAMES; i++ )
		{
			this.update();
		}
		//...GENERATE ALL FIREFLIES ON START
	}

	public GFireflyViewPool getFirefliesPool()
	{
		return this.fireflies_gfvp;
	}

	private void generateFirefly()
	{
		this.fireflies_gfvp.add(Random.Range(0, 100), Random.Range(0, 100));
	}

	public void update()
	{
		GFireflyViewPool fireflies_gfvp = this.fireflies_gfvp;
		
		for( int i = 0; i < fireflies_gfvp.length(); i++ )
		{
			fireflies_gfvp.getFirefly(i).update();
		}

		for( int i = 0; i < fireflies_gfvp.length(); i++ )
		{
			GFireflyView firefly_gfv = fireflies_gfvp.getFirefly(i);

			if(firefly_gfv.isNotRequired())
			{
				fireflies_gfvp.delete(i);
			}
		}

		this.frameCounter_int++;

		if(this.frameCounter_int > GFirefliesView.FIREFLY_GENERATION_INTERVAL_IN_FRAMES)
		{
			this.frameCounter_int = 0;
			this.generateFirefly();
		}
	}
}