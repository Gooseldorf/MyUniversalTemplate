using Infrastructure.Services.Input;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(CharacterController))] 
[RequireComponent(typeof(PlayerInput))]
public class PlayerView: MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInput input;
    private IInputService inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        this.inputService = inputService;
    }

    private void Start()
    {
        inputService.Move2DStream.Subscribe(Move);
    }

    private void OnDestroy()
    {
        inputService.Dispose();
    }

    private void Move(Vector2 moveVector) => controller.Move(new Vector3(moveVector.x, 0, moveVector.y) * 10 * Time.deltaTime);
    
}
