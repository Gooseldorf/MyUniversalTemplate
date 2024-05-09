namespace Infrastructure.States
{
    public interface IState
    {
        void Exit();
    }
    
    public interface IStateNoArg: IState
    {
        void Enter();
    }
    
    public interface IStateWithArg<TArg>: IState
    {
        void Enter(TArg arg);
    }
}