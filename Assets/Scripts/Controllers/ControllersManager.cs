using UnityEngine;
using UnityEngine.AI;

public class ControllersManager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private ClickPointerView _clickPointerView;

    private Controller _playerCharacterController;
    private Controller _aiCharacterController;
    private Controller _playerAiHybridController;

    private GroundClickRaycaster _groundClickRaycaster;
    private InputController _inputController;


    private void Awake()
    {
        _groundClickRaycaster = new GroundClickRaycaster(_agentCharacter.GroundLayer);
        _inputController = new InputController(_groundClickRaycaster);

        _clickPointerView.Initialize(_inputController);

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = 1;

        _aiCharacterController = new CompositeController(
            new AgentRandomMovableController(_agentCharacter, queryFilter),
            new AlongMovableVelocityRotatableController(_agentCharacter, _agentCharacter));

        _playerCharacterController = new CompositeController(
            new AgentCharacterController(_agentCharacter, _inputController),
            new AlongMovableVelocityRotatableController(_agentCharacter, _agentCharacter));

        _playerAiHybridController = new PlayerAiHybridController(
            _playerCharacterController,
            _aiCharacterController,
            _inputController);

        _inputController.Enable();
        _playerAiHybridController.Enable();
    }

    private void Update()
    {
        _inputController.Update(Time.deltaTime);
        _playerAiHybridController.Update(Time.deltaTime);
    }
}
