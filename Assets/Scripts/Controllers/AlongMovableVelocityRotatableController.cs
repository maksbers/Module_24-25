public class AlongMovableVelocityRotatableController : Controller
{
    private AgentCharacter _movable;
    private IDirectionalRotatable _rotatable;

    public AlongMovableVelocityRotatableController(AgentCharacter movable, IDirectionalRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}
