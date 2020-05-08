using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant
{
    public static class IOC
    {
        static IContainerProvider _container;
        public static void RegisterIOC(IContainerProvider container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
