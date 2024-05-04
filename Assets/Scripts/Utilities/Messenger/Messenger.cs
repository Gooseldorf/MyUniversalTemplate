using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Messenger
    {
        public static void AddListener(string eventName, Action handler) => MessengerInternal.AddListener(eventName, handler);
        
        public static void AddListener<T>(string eventName, Action<T> handler) => MessengerInternal.AddListener(eventName, handler);
        
        public static void RemoveListener(string eventName, Action handler) => MessengerInternal.RemoveListener(eventName, handler);
        
        public static void RemoveListener<T>(string eventName, Action<T> handler) => MessengerInternal.RemoveListener(eventName, handler);
        
        public static void Broadcast(string eventName) => Broadcast(eventName, MessengerInternal.DEFAULT_MODE);
        
        public static void Broadcast<TReturn>(string eventName, Action<TReturn> returnCall) => Broadcast(eventName, returnCall, MessengerInternal.DEFAULT_MODE);
        

        public static void Broadcast(string eventName, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);

            Action[] invocationList = MessengerInternal.GetInvocationList<Action>(eventName);

            if (invocationList == null)
                return;

            for (int i = 0; i < invocationList.Length; i++)
            {
                try
                {
                    invocationList[i].Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e);
                    Debug.LogException(e);
                }
            }
        }

        public static void Broadcast<TReturn>(string eventName, Action<TReturn> returnCall, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);

            var invocationList = MessengerInternal.GetInvocationList<Func<TReturn>>(eventName);

            if (invocationList == null)
                return;

            List<TReturn> returns = new();
            foreach (Func<TReturn> func in invocationList)
                returns.Add(func.Invoke());

            foreach (TReturn result in returns)
            {
                try
                {
                    returnCall.Invoke(result);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e.ToString());
                    Debug.LogException(e);
                }
            }
        }

        public static void Clear() => MessengerInternal.Clear();
    }
    
    public static class Messenger<T>
    {
        public static void AddListener(string eventName, Action<T> handler) => MessengerInternal.AddListener(eventName, handler);
        
        public static void AddListener<TReturn>(string eventName, Func<T, TReturn> handler) => MessengerInternal.AddListener(eventName, handler);
        
        public static void RemoveListener(string eventName, Action<T> handler) => MessengerInternal.RemoveListener(eventName, handler);
        
        public static void RemoveListener<TReturn>(string eventName, Func<T, TReturn> handler) => MessengerInternal.RemoveListener(eventName, handler);
        
        public static void Broadcast(string eventName, T arg1) => Broadcast(eventName, arg1, MessengerInternal.DEFAULT_MODE);
        
        public static void Broadcast<TReturn>(string eventName, T arg1, Action<TReturn> returnCall) => Broadcast(eventName, arg1, returnCall, MessengerInternal.DEFAULT_MODE);
        
        public static void Broadcast(string eventName, T arg1, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);

            Action<T>[] invocationList = MessengerInternal.GetInvocationList<Action<T>>(eventName);

            if (invocationList == null)
                return;

            foreach (var t in invocationList)
            {
                try
                {
                    t.Invoke(arg1);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e.ToString());
                    Debug.LogException(e);
                }
            }
        }

        public static void Broadcast<TReturn>(string eventName, T arg1, Action<TReturn> returnCall, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, TReturn>>(eventName);
            if (invocationList == null)
                return;

            List<TReturn> returns = new();
            foreach (Func<T, TReturn> func in invocationList)
                returns.Add(func.Invoke(arg1));

            foreach (TReturn result in returns)
            {
                try
                {
                    returnCall.Invoke(result);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e.ToString());
                    Debug.LogException(e);
                }
            }
        }

        public static void Clear() => MessengerInternal.Clear();
    }
    
    public static class Messenger<T, U>
    {
        public static void AddListener(string eventName, Action<T, U> handler) => MessengerInternal.AddListener(eventName, handler);

        public static void AddListener<TReturn>(string eventName, Func<T, U, TReturn> handler) => MessengerInternal.AddListener(eventName, handler);

        public static void RemoveListener(string eventName, Action<T, U> handler) => MessengerInternal.RemoveListener(eventName, handler);

        public static void RemoveListener<TReturn>(string eventName, Func<T, U, TReturn> handler) => MessengerInternal.RemoveListener(eventName, handler);

        public static void Broadcast(string eventName, T arg1, U arg2) => Broadcast(eventName, arg1, arg2, MessengerInternal.DEFAULT_MODE);

        public static void Broadcast<TReturn>(string eventName, T arg1, U arg2, Action<TReturn> returnCall) => Broadcast(eventName, arg1, arg2, returnCall, MessengerInternal.DEFAULT_MODE);

        public static void Broadcast(string eventName, T arg1, U arg2, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);

            Action<T, U>[] invocationList = MessengerInternal.GetInvocationList<Action<T, U>>(eventName);
            if (invocationList == null)
                return;
            for (int i = 0; i < invocationList.Length; i++)
            {
                try
                {
                    invocationList[i].Invoke(arg1, arg2);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e.ToString());
                    Debug.LogException(e);
                    //TODO: Remove Listener from this broadcast 
                }
            }
        }

        public static void Broadcast<TReturn>(string eventName, T arg1, U arg2, Action<TReturn> returnCall, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, TReturn>>(eventName);
            if (invocationList == null)
                return;

            List<TReturn> returns = new();
            foreach (Func<T, U, TReturn> func in invocationList)
                returns.Add(func.Invoke(arg1, arg2));

            foreach (TReturn result in returns)
            {
                try
                {
                    returnCall.Invoke(result);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error In Broadcast of: " + eventName + " event. " + e.ToString());
                    Debug.LogException(e);
                    //TODO: Remove Listener from this broadcast 
                }
            }
        }

        public static void Clear() => MessengerInternal.Clear();
    }
    
    public static class Messenger<T, U, V>
    {
        public static void AddListener(string eventName, Action<T, U, V> handler) => MessengerInternal.AddListener(eventName, handler);

        public static void AddListener<TReturn>(string eventName, Func<T, U, V, TReturn> handler) => MessengerInternal.AddListener(eventName, handler);

        public static void RemoveListener(string eventName, Action<T, U, V> handler) => MessengerInternal.RemoveListener(eventName, handler);

        public static void RemoveListener<TReturn>(string eventName, Func<T, U, V, TReturn> handler) => MessengerInternal.RemoveListener(eventName, handler);

        public static void Broadcast(string eventName, T arg1, U arg2, V arg3) => Broadcast(eventName, arg1, arg2, arg3, MessengerInternal.DEFAULT_MODE);

        public static void Broadcast<TReturn>(string eventName, T arg1, U arg2, V arg3, Action<TReturn> returnCall) => Broadcast(eventName, arg1, arg2, arg3, returnCall, MessengerInternal.DEFAULT_MODE);

        public static void Broadcast(string eventName, T arg1, U arg2, V arg3, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);
            if (mode == MessengerModeEnum.DONT_REQUIRE_LISTENER && !MessengerInternal.EventTable.ContainsKey(eventName)) return;
            Delegate del;
            MessengerInternal.EventTable.TryGetValue(eventName, out del);
            if (del != null) ((Action<T, U, V>)del).Invoke(arg1, arg2, arg3);
        }

        public static void Broadcast<TReturn>(string eventName, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerModeEnum mode)
        {
            MessengerInternal.OnBroadcasting(eventName, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, V, TReturn>>(eventName);
            if (invocationList == null)
                return;

            List<TReturn> returns = new();
            foreach (Func<T, U, V, TReturn> func in invocationList)
                returns.Add(func.Invoke(arg1, arg2, arg3));

            foreach (TReturn result in returns)
            {
                returnCall.Invoke(result);
            }
        }

        public static void Clear() => MessengerInternal.Clear();
    }
}