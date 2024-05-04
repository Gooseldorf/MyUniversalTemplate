using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
    internal static class MessengerInternal
    {
        public const MessengerModeEnum DEFAULT_MODE = MessengerModeEnum.REQUIRE_LISTENER;

        public static readonly Dictionary<string, Delegate> EventTable = new();

        public static void AddListener(string eventName, Delegate callback)
        {
            OnListenerAdding(eventName, callback);
            EventTable[eventName] = Delegate.Combine(EventTable[eventName], callback);
        }

        public static void RemoveListener(string eventName, Delegate handler)
        {
            if (OnListenerRemoving(eventName, handler) == false)
                return;

            EventTable[eventName] = Delegate.Remove(EventTable[eventName], handler);
            OnListenerRemoved(eventName);
        }

        public static T[] GetInvocationList<T>(string eventName)
        {
            if (!EventTable.TryGetValue(eventName, out Delegate del))
                return default;

            if (del != null)
            {
                Delegate[] invocationList = del.GetInvocationList();
                IEnumerable<T> enumerable = Cast(invocationList);
                T[] array = new T[invocationList.Length];
                using IEnumerator<T> en = enumerable.GetEnumerator();

                int index = 0;
                while (en.MoveNext())
                {
                    T current = en.Current;
                    array[index] = current;
                    index++;
                }

                return array;
            }

            IEnumerable<T> Cast(IEnumerable source)
            {
                foreach (T result in source)
                    yield return result;
            }

            throw CreateBroadcastSignatureException(eventName);
        }

        public static void Clear() => EventTable.Clear();

        private static void OnListenerAdding(string eventName, Delegate listenerBeingAdded)
        {
            EventTable.TryAdd(eventName, null);

            Delegate d = EventTable[eventName];

            if (d != null && d.GetType() != listenerBeingAdded.GetType())
                throw new ListenerException($"Attempting to add listener with inconsistent signature for event type {eventName}. Current listeners have type {d.GetType().Name} and listener being added has type {listenerBeingAdded.GetType().Name}");
        }

        private static bool OnListenerRemoving(string eventName, Delegate listenerBeingRemoved)
        {
            if (!EventTable.ContainsKey(eventName))
                return false;

            Delegate d = EventTable[eventName];

            if (d == null)
                return false;

            if (d.GetType() != listenerBeingRemoved.GetType())
                throw new ListenerException($"Attempting to remove listener with inconsistent signature for event type {eventName}. Current listeners have type {d.GetType().Name} and listener being removed has type {listenerBeingRemoved.GetType().Name}");

            return true;
        }

        private static void OnListenerRemoved(string eventName)
        {
            if (EventTable[eventName] == null)
                EventTable.Remove(eventName);
        }

        public static void OnBroadcasting(string eventName, MessengerModeEnum mode)
        {
            if (mode == MessengerModeEnum.REQUIRE_LISTENER && !EventTable.ContainsKey(eventName))
                throw new BroadcastException($"Broadcasting message {eventName} but no listener found.");
        }

        private static BroadcastException CreateBroadcastSignatureException(string eventName)
        {
            return new BroadcastException($"Broadcasting message {eventName} but listeners have a different signature than the broadcaster.");
        }

        private sealed class BroadcastException : Exception
        {
            public BroadcastException(string msg) : base(msg) { }
        }

        private sealed class ListenerException : Exception
        {
            public ListenerException(string msg) : base(msg) { }
        }
    }
}