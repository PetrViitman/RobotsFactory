using UnityEngine;

public class GCellView : GInteractiveRectangleView
{
	public const int DIRECTION_ID_UNDEFINED = 0;
	public const int DIRECTION_ID_HORIZONTAL = 1;
	public const int DIRECTION_ID_VERTICAL = 2;

	private const int FLOATING_UP_DOWN_DURATION_IN_FRAMES = 150;

	private GMouseModel mouseModel_gmm;
	private GGridView gridView_ggv;
	private GPoint mouseOffset_gp;
	private bool isGrabed_bl;
	private int directionId_int;
	private GRobotDetailView robotDetailView_grdv;
	private GAdjustableFloatingValue floatingHeigth_gafv;

	public GCellView(GGridView aGridView_gv, float aOptX_num, float aOptY_num, float aOptWidth_num, float aOptHeight_num)
		: base(aOptX_num, aOptY_num, aOptWidth_num, aOptHeight_num)
	{
		this.mouseModel_gmm = (GMouseModel) GMain.getMouseController().getModel();
		this.gridView_ggv = aGridView_gv;
		this.mouseOffset_gp = new GPoint();
		this.isGrabed_bl = false;
		this.directionId_int = GCellView.DIRECTION_ID_UNDEFINED;
		this.robotDetailView_grdv = null;
		this.floatingHeigth_gafv = new GAdjustableFloatingValue(GCellView.FLOATING_UP_DOWN_DURATION_IN_FRAMES);
		this.floatingHeigth_gafv.randomize();
	}

	protected override void onModelSet(GModel aModel_gm)
	{
		GCellModel model_gсm = (GCellModel) aModel_gm;
		GRobotDetailView robotDetailView_grdv = GRobotTemplate.ROBOT_DESCRIPTOR.generateDetailView(model_gсm.getTypeId());

		this.setRobotDetailView(robotDetailView_grdv);
	}

	public GRobotDetailView getRobotDetailView()
	{
		return this.robotDetailView_grdv;
	}

	public void setRobotDetailView(GRobotDetailView aRobotDetailView_grdv)
	{
		this.robotDetailView_grdv = aRobotDetailView_grdv;
	}

	protected override bool isInteraction()
	{
		GMouseModel mouseModel_gmm = this.mouseModel_gmm;

		if(!mouseModel_gmm.isDown())
		{
			return false;
		}

		if(this.isGrabed_bl)
		{
			return true;
		}

		return this.isCollision(mouseModel_gmm.getDownPoint());
	}

	protected override void onInteractionStart()
	{
		GPoint mouseDownPoint_gp = this.mouseModel_gmm.getDownPoint();
		float mouseX_num = mouseDownPoint_gp.getX();
		float mouseY_num = mouseDownPoint_gp.getY();

		float mouseOffsetX_num = mouseX_num - this.getX();
		float mouseOffsetY_num = mouseY_num - this.getY();

		this.mouseOffset_gp.setXY(
			mouseOffsetX_num,
			mouseOffsetY_num);

		this.isGrabed_bl = true;

		//DIRECTION AXIS IDENTIFICATION...
		float width_num = this.getWidth() * GScreen.getSidesRatio();
		float height_num = this.getHeight();

		float x_num = this.getX();
		float y_num = this.getY();

		float centerX_num = x_num + width_num/2f;
		float centerY_num = y_num + height_num/2f;
		
		float corner1X_num = x_num;
		float corner1Y_num = y_num;

		float corner2X_num = x_num + width_num;
		float corner2Y_num = y_num;

		float corner3X_num = x_num + width_num;
		float corner3Y_num = y_num + height_num;

		float corner4X_num = x_num;
		float corner4Y_num = y_num + height_num;

		//UP...
		if(this.isTriangleCollision(
			corner1X_num,
			corner1Y_num,
			corner2X_num,
			corner2Y_num,
			centerX_num,
			centerY_num,
			mouseX_num,
			mouseY_num))
		{
			this.directionId_int = GCellView.DIRECTION_ID_VERTICAL;
			return;
		}
		//...UP

		//DOWN...
		if(this.isTriangleCollision(
			corner3X_num,
			corner3Y_num,
			corner4X_num,
			corner4Y_num,
			centerX_num,
			centerY_num,
			mouseX_num,
			mouseY_num))
		{
			this.directionId_int = GCellView.DIRECTION_ID_VERTICAL;
			return;
		}
		//...DOWN

		//LEFT...
		if(this.isTriangleCollision(
			corner1X_num,
			corner1Y_num,
			corner4X_num,
			corner4Y_num,
			centerX_num,
			centerY_num,
			mouseX_num,
			mouseY_num))
		{
			this.directionId_int = GCellView.DIRECTION_ID_HORIZONTAL;
			return;
		}
		//...LEFT

		//RIGHT...
		if(this.isTriangleCollision(
			corner2X_num,
			corner2Y_num,
			corner3X_num,
			corner3Y_num,
			centerX_num,
			centerY_num,
			mouseX_num,
			mouseY_num))
		{
			this.directionId_int = GCellView.DIRECTION_ID_HORIZONTAL;
			return;
		}
		//...RIGHT

		//...DIRECTION AXIS IDENTIFICATION
	}

	private bool isTriangleCollision(
		float aCorner1X_num,
		float aCorner1Y_num,
		float aCorner2X_num,
		float aCorner2Y_num,
		float aCorner3X_num,
		float aCorner3Y_num,
		float aPointX_num,
		float aPointY_num)
	{
		float originalTriangleArea_num = 
			this.calculateTriangleArea(
				aCorner1X_num,
				aCorner1Y_num,
				aCorner2X_num,
				aCorner2Y_num,
				aCorner3X_num,
				aCorner3Y_num) * 1.01f;

		float newTriangleArea1_num = 
			this.calculateTriangleArea(
				aPointX_num,
				aPointY_num,
				aCorner2X_num,
				aCorner2Y_num,
				aCorner3X_num,
				aCorner3Y_num);

		float newTriangleArea2_num = 
			this.calculateTriangleArea(
				aCorner1X_num,
				aCorner1Y_num,
				aPointX_num,
				aPointY_num,
				aCorner3X_num,
				aCorner3Y_num);

		float newTriangleArea3_num = 
			this.calculateTriangleArea(
				aCorner1X_num,
				aCorner1Y_num,
				aCorner2X_num,
				aCorner2Y_num,
				aPointX_num,
				aPointY_num);

		return (
			newTriangleArea1_num +
			newTriangleArea2_num +
			newTriangleArea3_num
			) <= originalTriangleArea_num;
	}

	private float calculateTriangleArea(
		float aCorner1X_num,
		float aCorner1Y_num,
		float aCorner2X_num,
		float aCorner2Y_num,
		float aCorner3X_num,
		float aCorner3Y_num)
	{
		return (float)Mathf.Abs(
			(aCorner2X_num - aCorner1X_num) *
			(aCorner3Y_num - aCorner1Y_num) - 
			(aCorner3X_num - aCorner1X_num) *
			(aCorner2Y_num - aCorner1Y_num));
	}

	protected override void onInteraction()
	{
		switch(this.directionId_int)
		{
			case GCellView.DIRECTION_ID_HORIZONTAL:
			{
				this.gridView_ggv.setRowXByActiveCell(this.mouseModel_gmm.getX() - this.mouseOffset_gp.getX());
			}
			break;
			case GCellView.DIRECTION_ID_VERTICAL:
			{
				this.gridView_ggv.setColumnYByActiveCell(this.mouseModel_gmm.getY() - this.mouseOffset_gp.getY());
			}
			break;
		}
	}

	protected override void onInteractionEnd()
	{
		this.isGrabed_bl = false;
	}


	public void setDirectionId(int aDirectionId_int)
	{
		this.directionId_int = aDirectionId_int;
	}

	public int getDirectionId()
	{
		return this.directionId_int;
	}

	public float getFloatingHeightValue()
	{
		return this.floatingHeigth_gafv.getFloatingValue();
	}

	public void updateFloating()
	{
		this.floatingHeigth_gafv.update();
	}
}