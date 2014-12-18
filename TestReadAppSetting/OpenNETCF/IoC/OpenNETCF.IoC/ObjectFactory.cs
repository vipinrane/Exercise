// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

using TheInvoker = System.Windows.Forms.Control;

namespace OpenNETCF.IoC
{
    internal class ObjectFactory<TBaseSmartPart>
        where TBaseSmartPart : class, ISmartPart
    {
        private static Dictionary<Type, SubscriptionDescriptor[]> m_subscriptionDescriptorCache =
            new Dictionary<Type, SubscriptionDescriptor[ ]> ( );

        private static Dictionary<Type, PublicationDescriptor[]> m_publicationDescriptorCache =
            new Dictionary<Type, PublicationDescriptor[ ]> ( );

        private static Dictionary<Type, InjectionConstructor> m_constructorCache = new Dictionary<Type, InjectionConstructor> ( );

        private static Dictionary<Type, string[]> m_eventSourceNameCache = new Dictionary<Type, string[ ]> ( );

        internal static string GenerateServiceName ( Type t )
        {
            return t.Name + "Service";
        }

        internal static string GenerateItemName<TItem> ( Type t, ManagedObjectCollection<TItem, TBaseSmartPart> parent )
            where TItem : class
        {
            string name = string.Empty;
            int i = 0;
            do
            {
                name = t.Name + ( ++i ).ToString ( );
            } while ( parent[ name ] != null );
            return name;
        }

        internal static string GenerateItemName ( Type t, WorkItem<TBaseSmartPart> root )
        {
            string name = string.Empty;
            int i = 0;
            do
            {
                name = t.Name + ( ++i ).ToString ( );
            } while ( root.Items[ name ] != null );
            return name;
        }

        internal class PublicationDescriptor
        {
            public EventPublication Publication { get; set; }
            public EventInfo EventInfo { get; set; }
        }

        internal class SubscriptionDescriptor
        {
            public EventSubscription Subscription { get; set; }
            public MethodInfo MethodInfo { get; set; }
        }

        internal static SubscriptionDescriptor[ ] GetEventSinks ( Type type )
        {
            if ( m_subscriptionDescriptorCache.ContainsKey ( type ) )
            {
                return m_subscriptionDescriptorCache[ type ];
            }

            var methods =
            from n in type.GetMethods ( BindingFlags.Public | BindingFlags.Instance )
            where
            !n.IsVirtual &&
            n.GetCustomAttributes ( typeof ( EventSubscription ), true ).Length > 0
            select n;

            List<SubscriptionDescriptor> descriptors = new List<SubscriptionDescriptor> ( );
            foreach ( MethodInfo mi in methods )
            {
                descriptors.Add ( new SubscriptionDescriptor
                {
                    MethodInfo = mi,
                    Subscription = mi.GetCustomAttributes ( typeof ( EventSubscription ), true ).FirstOrDefault ( ) as EventSubscription
                } );
            }

            SubscriptionDescriptor[] result = descriptors.ToArray ( );
            m_subscriptionDescriptorCache.Add ( type, result );
            return result;
        }

        internal static PublicationDescriptor[ ] GetEventSources ( Type type )
        {
            if ( m_publicationDescriptorCache.ContainsKey ( type ) )
            {
                return m_publicationDescriptorCache[ type ];
            }

            // there has to be a less convoluted LINQ query to pull this off, I just can't work it out yet

            var events =
            from n in type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            where n.GetCustomAttributes(typeof(EventPublication), true).Length > 0
            select n;

            List<PublicationDescriptor> descriptors = new List<PublicationDescriptor>();
            foreach (EventInfo ei in events)
            {
                descriptors.Add(new PublicationDescriptor
                {
                    EventInfo = ei,
                    Publication = ei.GetCustomAttributes(typeof(EventPublication), true).FirstOrDefault() as EventPublication
                });
            }
            PublicationDescriptor[] result = descriptors.ToArray();
            m_publicationDescriptorCache.Add(type, result);
            return result;
        }

        internal static string[] GetEventSourceNames(Type type)
        {
            if(m_eventSourceNameCache.ContainsKey(type))
            {
                return m_eventSourceNameCache[type];
            }
            else
            {
                string[] names = (from n in (type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(
                e => e.GetCustomAttributes(typeof(EventPublication), true).First() as EventPublication))
                    select n.EventName).Distinct().ToArray();

                m_eventSourceNameCache.Add(type, names);
                return names;
            }
        }

        internal static EventSubscription[] GetEventSinkSubscriptions(Type type)
        {
            return (from n in (type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(
                m => m.GetCustomAttributes(typeof(EventSubscription), true).FirstOrDefault() as EventSubscription))
                    select n).Distinct().Where(e => e != null).ToArray();
        }

        internal static EventInfo[] GetEventSourcesFromTypeByName(Type type, string eventName)
        {
            if (m_publicationDescriptorCache.ContainsKey(type))
            {
                return (from e in m_publicationDescriptorCache[type]
                        where e.Publication.EventName == eventName
                        select e.EventInfo).ToArray();
            }
            else
            {
                return (from e in type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        where
                    (from a in e.GetCustomAttributes(typeof(EventPublication), true) as EventPublication[]
                     where a.EventName == eventName
                     select a).Count() > 0
                            select e).ToArray();
            }

        }

        internal static MethodInfo[] GetEventSinksFromTypeByName(Type type, string eventName, ThreadOption option)
        {
            if (m_subscriptionDescriptorCache.ContainsKey(type))
            {
                return (from e in m_subscriptionDescriptorCache[type]
                        where e.Subscription.EventName == eventName
                        && e.Subscription.ThreadOption == option
                        select e.MethodInfo).ToArray();
            }
            else
            {
                return (from e in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        where
                    (from a in e.GetCustomAttributes(typeof(EventSubscription), true) as EventSubscription[]
                     where a.EventName == eventName
                     && a.ThreadOption == option
                     select a).Count() > 0
                            select e).ToArray();
            }
        }

        private static void AddCollectionEventHandlers<TKey, TItem>(object instance, IEnumerable<KeyValuePair<TKey, TItem>> collection, PublicationDescriptor[] sourceEvents, SubscriptionDescriptor[] eventSinks)
        {
            if (collection == null) return;

            var invokerControl = RootWorkItem<TBaseSmartPart>.Instance.Items.Get<TheInvoker> ( IOCConstants.IOCEventInvokerName );

            foreach (var item in collection)
            {
                if (instance.Equals(item.Value)) continue;

                if ( ( item.Value is WorkItem<TBaseSmartPart> ) && ( collection is ServiceCollection<TBaseSmartPart> ) )
                {
                    // this prevents recursion (and a stack overflow) for WorkItems
                    continue;
                }

                foreach (var source in sourceEvents)
                {
                    // wire up events
                    foreach (var sink in GetEventSinksFromTypeByName(item.Value.GetType(), source.Publication.EventName, ThreadOption.Caller))
                    {
                        Delegate d = Delegate.CreateDelegate(source.EventInfo.EventHandlerType, item.Value, sink);
                        source.EventInfo.AddEventHandler(instance, d);
                    }

                    foreach (var sink in GetEventSinksFromTypeByName(item.Value.GetType(), source.Publication.EventName, ThreadOption.UserInterface))
                    {
                        // wire up event handlers on the UI thread
                        Delegate d = Delegate.CreateDelegate(source.EventInfo.EventHandlerType, item.Value, sink);

                        if (source.EventInfo.EventHandlerType == typeof(EventHandler))
                        {
                            // unsure why so far but this fails if the EventHandler signature takes a subclass of EventArgs as the second param
                            // and if you use just EventArgs, the arg data gets lost
                            BasicInvoker invoker = new BasicInvoker(invokerControl, d);
                            Delegate intermediate = Delegate.CreateDelegate(source.EventInfo.EventHandlerType, invoker, invoker.HandlerMethod);
                            source.EventInfo.AddEventHandler(instance, intermediate);
                        }
                        else if ((source.EventInfo.EventHandlerType.IsGenericType) && (source.EventInfo.EventHandlerType.GetGenericTypeDefinition().Name == "EventHandler`1"))
                        {
                            BasicInvoker invoker = new BasicInvoker(invokerControl, d);
                            Delegate intermediate = Delegate.CreateDelegate(source.EventInfo.EventHandlerType, invoker, invoker.HandlerMethod);
                            source.EventInfo.AddEventHandler(instance, intermediate);
                        }

                    }
                }

                // back-wire any sinks
                foreach (var sink in eventSinks)
                {
                    foreach (var ei in GetEventSourcesFromTypeByName(item.Value.GetType(), sink.Subscription.EventName))
                    {
                        try
                        {
                            // (type, consumer instance, consumer method)
                            Delegate d = Delegate.CreateDelegate(ei.EventHandlerType, instance, sink.MethodInfo);

                            if (sink.Subscription.ThreadOption == ThreadOption.Caller)
                            {
                                ei.AddEventHandler(item.Value, d);
                            }
                            else
                            {
                                // wire up event handlers on the UI thread
                                if ((ei.EventHandlerType.IsGenericType) && (ei.EventHandlerType.GetGenericTypeDefinition().Name == "EventHandler`1")
                                    || (ei.EventHandlerType == typeof(EventHandler))
                                        || ( ei.EventHandlerType == typeof ( KeyEventHandler ) ) )
                                {
                                    // unsure why so far but this fails if the EventHandler signature takes a subclass of EventArgs as the second param
                                    // and if you use just EventArgs, the arg data gets lost

                                
                                    BasicInvoker invoker = new BasicInvoker(invokerControl, d);
                                    Delegate intermediate = Delegate.CreateDelegate(ei.EventHandlerType, invoker, invoker.HandlerMethod);
                                    ei.AddEventHandler(item.Value, intermediate);
                                }
                                else
                                {
                                    throw new ArgumentException("ThreadOption.UserInterface only supported for EventHandler and EventHandler<T> events");
                                }
                            }
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException(string.Format("Unable to attach EventHandler '{0}' to '{1}'.\r\nDo the publisher and subscriber signatures match?", ei.Name, instance.GetType().Name));
                        }
                    }
                }

                WorkItem<TBaseSmartPart> wi = item.Value as WorkItem<TBaseSmartPart>;
                if (wi != null)
                {
                    AddEventHandlers(instance, wi, false);
                }
            }
        }
    
        internal static void AddEventHandlers ( object instance, WorkItem<TBaseSmartPart> root )
        {
            AddEventHandlers(instance, root, true);
        }

        internal static void AddEventHandlers ( object instance, WorkItem<TBaseSmartPart> root, bool walkUpToRoot )
        {
            Type instanceType = instance.GetType();

            // get all of the sources from the object
            var sourceEvents = GetEventSources(instanceType);

            // get all of the sinks in the object
            var eventSinks = GetEventSinks(instanceType);

            // find any items that subscribe to the source events
            WorkItem<TBaseSmartPart> wi = instance as WorkItem<TBaseSmartPart>;
            if (wi != null)
            {
                WorkItem<TBaseSmartPart> localRoot = root;
                if (walkUpToRoot)
                {
                    while (localRoot.Parent != null)
                    {
                        localRoot = root.Parent;
                    }
                }

                // recurse for WorkItems
                AddCollectionEventHandlers(instance, localRoot.Items, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, localRoot.Services, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, localRoot.WorkItems, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, localRoot.SmartParts, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, localRoot.Workspaces, sourceEvents, eventSinks);

                foreach (var childItem in localRoot.WorkItems)
                {
                    if (childItem.Value == instance) continue;
                    AddEventHandlers(instance, childItem.Value, false);
                }
            }
            else
            {
                AddCollectionEventHandlers(instance, root.Items, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, root.Services, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, root.WorkItems, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, root.SmartParts, sourceEvents, eventSinks);
                AddCollectionEventHandlers(instance, root.Workspaces, sourceEvents, eventSinks);
            }
        }

        private struct InjectionConstructor
        {
            public ConstructorInfo CI { get; set; }
            public ParameterInfo[] ParameterList { get; set; }
        }

        private static object CreateObjectFromCache ( Type t, WorkItem<TBaseSmartPart> root )
        {
            InjectionConstructor ic = m_constructorCache[t];

            try
            {
                if ((ic.ParameterList == null) || (ic.ParameterList.Length == 0))
                {
                    return ic.CI.Invoke(null);
                }
                else
                {
                    object[] inputs = GetParameterObjectsForParameterList(ic.ParameterList, root, t.Name);
                    return ic.CI.Invoke(inputs);
                }
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        internal static object CreateObject ( Type t, WorkItem<TBaseSmartPart> root )
        {
            object instance = null;

            // first check the cache
            if(m_constructorCache.ContainsKey(t))
            {
                return CreateObjectFromCache(t, root);
            }

            ConstructorInfo ci;

            if (t.IsInterface)
            {
                throw new IOCException(string.Format("Cannot create an instance of an interface class ({0}). Check your registration code.", t.Name));
            }


            // see if there is an injection ctor
            var ctors = (from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         where c.IsPublic == true
                         && c.GetCustomAttributes(typeof(InjectionConstructorAttribute), true).Count() > 0
                         select c);

            if (ctors.Count() == 0)
            {
                // no injection ctor, get the default, parameterless ctor
                var parameterlessCtors = (from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                          where c.GetParameters().Length == 0
                                          select c);
                if (parameterlessCtors.Count() == 0)
                {
                    throw new ArgumentException(string.Format("Type '{0}' has no public parameterless constructor or injection constructor.\r\nAre you missing the InjectionConstructor attribute?", t));
                }

                // create the object
                ci = parameterlessCtors.First();
                try
                {
                    instance = ci.Invoke(null);
                    m_constructorCache.Add(t, new InjectionConstructor { CI = ci });
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            else if (ctors.Count() == 1)
            {
                // call the injection ctor
                ci = ctors.First();
                ParameterInfo[] paramList = ci.GetParameters();
                object[] inputs = GetParameterObjectsForParameterList(paramList, root, t.Name);
                try
                {
                    instance = ci.Invoke(inputs);
                    m_constructorCache.Add(t, new InjectionConstructor { CI = ci, ParameterList = paramList });
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            else
            {
                throw new ArgumentException(string.Format("Type '{0}' has {1} defined injection constructors.  Only one is allowed", t.Name, ctors.Count()));
            }
            // NOTE: we don't do injections here, as if the created object has a dependency that requires this instance it would fail becasue this instance is not yet in the item list.

            return instance;
        }

        internal static void DoInjections ( object instance, WorkItem<TBaseSmartPart> root )
        {
            Type t = instance.GetType();

            var injectionmethods = (from c in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                    where 
                                    !c.IsVirtual && 
                                    c.GetCustomAttributes(typeof(InjectionMethodAttribute), true).Count() > 0
                                    select c);

            foreach (MethodInfo mi in injectionmethods)
            {
                ParameterInfo[] paramList = mi.GetParameters();
                object[] inputs = GetParameterObjectsForParameterList(paramList, root, t.Name);
                mi.Invoke(instance, inputs);
            }

            // TODO: cache these

            // look for service dependecy setters
            var serviceDependecyProperties = from p in t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                             where p.GetCustomAttributes(typeof(ServiceDependencyAttribute), true).Count() > 0
                                             select p;

            foreach (PropertyInfo pi in serviceDependecyProperties)
            {
                // we know this is > 0 since they came through the LINQ filter above
                var attrib = pi.GetCustomAttributes(typeof(ServiceDependencyAttribute), true).Cast<ServiceDependencyAttribute>().First();

                if (attrib.RegistrationType == null) attrib.RegistrationType = pi.PropertyType;

                // see if we have the service already created
                if (!root.Services.Contains(attrib.RegistrationType))
                {
                    if (!attrib.EnsureExists)
                    {
                        throw new ServiceMissingException(string.Format("Type '{0}' has a service dependency on type '{1}'",
                            t.Name, attrib.RegistrationType.Name));
                    }
                    // create the service
                    root.Services.AddNew(pi.PropertyType, attrib.RegistrationType);
                }
                pi.SetValue(instance, root.Services.Get(attrib.RegistrationType), null);
            }

            AddEventHandlers(instance, root);
        }

        private static object[ ] GetParameterObjectsForParameterList ( ParameterInfo[ ] paramList, WorkItem<TBaseSmartPart> root, string typeName )
        {
            List<object> paramObjects = new List<object>();

            foreach (var pi in paramList)
            {
                if (pi.ParameterType.IsValueType)
                {
                    throw new ArgumentException(string.Format("Injection on type '{0}' cannot have value type parameters",
                        typeName));
                }

                object item = null;

                bool isServiceDependency = false;
                bool createNewRequested = false;

                var attrib = pi.GetCustomAttributes(typeof(ServiceDependencyAttribute), true).FirstOrDefault();

                if (attrib != null)
                {
                    isServiceDependency = true;
                }
                Type serviceRegiteredAsType = null;
                if (isServiceDependency)
                {
                    ServiceDependencyAttribute sda = (ServiceDependencyAttribute)attrib;
                    createNewRequested = sda.EnsureExists == true;
                    serviceRegiteredAsType = sda.RegistrationType;
                }
                else
                {
                    createNewRequested = pi.GetCustomAttributes(typeof(CreateNewAttribute), true).Count() > 0;
                }

                // check the service list
                if (isServiceDependency)
                {
                    var requiredServiceType = serviceRegiteredAsType == null ? pi.ParameterType : serviceRegiteredAsType;
                    object service = root.Services.Get(requiredServiceType);
                    if (service != null)
                    {
                        item = service;
                    }
                    else if (createNewRequested)
                    {
                        // create a new one
                        item = root.Services.AddNew(pi.ParameterType);
                    }
                    else if (root.Services.TypeRegistrations.ContainsKey(requiredServiceType))
                    {
                        // check the service list again as we might be recursing
                        service = root.Services.Get(requiredServiceType);
                        if (service == null)
                        {
                            item = root.Services.AddNew(root.Services.TypeRegistrations[requiredServiceType]);
                        }
                    else
                    {
                            item = service;
                        }
                    }
                    else
                    {

                        throw new ServiceMissingException(string.Format("Type '{0}' has a service dependency on type '{1}'",
                            typeName, pi.ParameterType.Name));
                    }
                }
                else // non service dependencies
                {
                    // see if there is an item that matches the type
                    object[] itemList = root.Items.FindByType(pi.ParameterType).ToArray();

                    if (itemList.Length != 0)
                    {
                        item = itemList[0];
                    }
                    else if (createNewRequested)
                    {
                        item = root.Items.AddNew(pi.ParameterType);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Injection on type '{0}' requires an item of type '{1}'",
                            typeName, pi.ParameterType.Name));
                    }
                }

                paramObjects.Add(item);
            }

            return paramObjects.ToArray();
        }
    }
}
