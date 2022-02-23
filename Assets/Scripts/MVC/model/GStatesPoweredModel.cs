public class GStatesPoweredModel : GModel
{
	public const int STATE_ID_INVALID = -1;

	private int previousStateId_int = GStatesPoweredModel.STATE_ID_INVALID;
	private int stateId_int = GStatesPoweredModel.STATE_ID_INVALID;

	public GStatesPoweredModel()
		: base()
	{

	}

	public void setStateId(int aStateId_int)
	{
		this.previousStateId_int = this.stateId_int;
		this.stateId_int = aStateId_int;
	}

	public int getStateId()
	{
		return this.stateId_int;
	}

	public int getPreviousStateId()
	{
		return this.previousStateId_int;
	}
}