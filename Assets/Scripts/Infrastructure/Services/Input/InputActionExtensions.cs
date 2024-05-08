using System;
using UniRx;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public static class InputActionExtensions
    {
        public static IObservable<T> GeneratePerformCanceledObservable<T>(this InputAction inputAction, Func<InputAction.CallbackContext, T> readValueFunc)
        {
            var observable = Observable.Create<T>(observer =>
            {
                Action<InputAction.CallbackContext> performedHandler = ctx => observer.OnNext(readValueFunc(ctx));
                Action<InputAction.CallbackContext> canceledHandler = ctx => observer.OnNext(readValueFunc(ctx));
                
                inputAction.performed += performedHandler;
                inputAction.canceled += canceledHandler;

                return new CompositeDisposable()
                {
                    Disposable.Create(() => inputAction.performed -= performedHandler),
                    Disposable.Create(() => inputAction.canceled -= canceledHandler)
                };
            });

            return observable.Publish().RefCount();
        }
        
        public static IObservable<T> GeneratePerformObservable<T>(this InputAction inputAction, Func<InputAction.CallbackContext, T> readValueFunc)
        {
            var observable = Observable.Create<T>(observer =>
            {
                Action<InputAction.CallbackContext> performedHandler = ctx => observer.OnNext(readValueFunc(ctx));
                
                inputAction.performed += performedHandler;

                return Disposable.Create(() => inputAction.performed -= performedHandler);
            });

            return observable.Publish().RefCount();
        }
        
        public static IObservable<T> GenerateContinuousObservable<T>(this InputAction inputAction, Func<T> readValueFunc)
        {
            var observable = Observable.EveryUpdate()
                .Where(_ => inputAction.inProgress)
                .Select(_ => readValueFunc());

            return observable.Publish().RefCount();
        }
    }
}