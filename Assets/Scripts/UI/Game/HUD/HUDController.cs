using Interfaces;
using UniRx;

namespace UI.Game.HUD
{
    public interface IHUDController : IInit, IDispose
    {
        
    }
    
    public class HUDController : IHUDController 
    {
        private readonly HUDView hudView;
        private HUDData initialHUDData;

        private readonly CompositeDisposable disposes = new CompositeDisposable();
        
        public HUDController(HUDView hudView, HUDData hudData)
        {
            this.hudView = hudView;
            initialHUDData = hudData;
        }
        
        public void Init()
        {
            //Subscribe to streams here
        }
        
        public void Dispose() => disposes.Dispose();
        
        public void SetToInitialState(){}

        public void SetState(HUDData hudData)
        {
            initialHUDData = hudData;
        }
    }
}