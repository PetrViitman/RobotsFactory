using UnityEngine;

public class GMouseController : GController
{
	private GMouseModel mouseModel_gmm;

	public GMouseController(GModel aModel_gm)
		: base(aModel_gm)
	{
		this.mouseModel_gmm = (GMouseModel)this.getModel();
	}

	//DESKTOP MOUSE...
	private void onMouseDown()
	{
		GMouseModel mouseModel_gmm = this.mouseModel_gmm;

		mouseModel_gmm.setIsDown(true);
		mouseModel_gmm.setDownXY(
			GScreen.toPercentageX(Input.mousePosition.x),
			100f - GScreen.toPercentageY(Input.mousePosition.y));
	}

	private void onMouseMove()
	{
		this.mouseModel_gmm.setXY(
			GScreen.toPercentageX(Input.mousePosition.x),
			100f - GScreen.toPercentageY(Input.mousePosition.y));
	}

	private void onMouseUp()
	{	
		this.mouseModel_gmm.setIsDown(false);
	}
	//...DESKTOP MOUSE

	//TOUCH SCREEN...
	private void onTouchStart()
	{
		GMouseModel mouseModel_gmm = this.mouseModel_gmm;
		Vector2 touchPosition_v2 = Input.GetTouch(0).position;

		mouseModel_gmm.setIsDown(true);

		mouseModel_gmm.setDownXY(
			GScreen.toPercentageX(touchPosition_v2.x),
			100f - GScreen.toPercentageY(touchPosition_v2.y));

		mouseModel_gmm.setXY(
			GScreen.toPercentageX(touchPosition_v2.x),
			100f - GScreen.toPercentageY(touchPosition_v2.y));

		Screen.fullScreen = true;
	}

	private void onTouchMove()
	{
		GMouseModel mouseModel_gmm = this.mouseModel_gmm;
		Vector2 touchPosition_v2 = Input.GetTouch(0).position;

		mouseModel_gmm.setXY(
			GScreen.toPercentageX(touchPosition_v2.x),
			100f - GScreen.toPercentageY(touchPosition_v2.y));
	}

	private void onTouchEnd()
	{
		this.mouseModel_gmm.setIsDown(false);
	}
	//...TOUCH SCREEN

	public override void update()
	{
		//DESKTOP MOUSE HANDLING...
		GMouseModel mouseModel_gmm = (GMouseModel) this.getModel();
		
		if(Input.GetMouseButtonDown(0))
		{
			this.onMouseDown();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			this.onMouseUp();
		}

		this.onMouseMove();
		//...DESKTOP MOUSE HANDLING

		//TOUCH SCREEN HANDLING...
		if(Input.touchCount > 0)
        {
        	Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                {
                	this.onTouchStart();
                }
                break;
                case TouchPhase.Moved:
                {
                	this.onTouchStart();
                }
                break;
                case TouchPhase.Ended:
                {
                	this.onTouchEnd();
                }
                break;
            }
        }
		//...TOUCH SCREEN HANDLING

		if(mouseModel_gmm.isDown())
		{
			mouseModel_gmm.incrementInteractionInpuIndex();
		}
	}
}