using Interfaces;
using UniRx;

namespace UI
{
    public abstract class WindowControllerBase : IInit, IDispose
    {
        protected CompositeDisposable disposables = new CompositeDisposable();

        public abstract void Init();

        public virtual void Dispose() => disposables.Dispose();

        public abstract void Show();
        public abstract void Hide();
    }
}