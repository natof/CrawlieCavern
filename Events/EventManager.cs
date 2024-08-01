using System;
using System.Collections.Generic;

namespace DefaultNamespace {
    public static class EventManager {
        private static readonly Dictionary<Type, Action<object>> EventHandlers = new();

        public static void Subscribe<T>(Action<T> handler) where T : class {
            if (!EventHandlers.ContainsKey(typeof(T))) {
                EventHandlers[typeof(T)] = e => handler(e as T);
            } else {
                EventHandlers[typeof(T)] += e => handler(e as T);
            }
        }

        public static void Trigger<T>(T @event) where T : class {
            if (EventHandlers.TryGetValue(typeof(T), out var handler)) {
                handler(@event);
            }
        }
    }
}