using UnityEngine;

public class PlayerAiHybridController : Controller
{
    private float _switchTime = 3f;

    private Controller _playerController;
    private Controller _aiController;
    private InputController _inputController;

    private Controller _currentController;

    public PlayerAiHybridController(
        Controller playerController,
        Controller aiController,
        InputController inputController)
    {
        _playerController = playerController;
        _aiController = aiController;
        _inputController = inputController;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Controller targetController;

        if (IsManualControlActive())
            targetController = _playerController;
        else
            targetController = _aiController;

        if (_currentController != targetController)
            SwitchController(targetController);

        _currentController.Update(Time.deltaTime);
    }

    private void SwitchController(Controller newController)
    {
        if (_currentController != null)
            _currentController.Disable();

        _currentController = newController;
        _currentController.Enable();
    }

    private bool IsManualControlActive() => _inputController.TimeSinceLastClick < _switchTime;
}
