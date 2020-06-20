using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PortableAudioPlayerAssistant.Context;
using PortableAudioPlayerAssistant.Services;
using PortableAudioPlayerAssistant.Services.Dispose;
using PortableAudioPlayerAssistant.Services.MediaPlayer;
using PortableAudioPlayerAssistant.StorageManager.Services;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Linq;

namespace PortableAudioPlayerAssistant
{
    public class App : PrismApplication
    {

        public static bool IsSingleViewLifetime =>
            Environment.GetCommandLineArgs()
                .Any(a => a == "--fbdev" || a == "--drm");

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            base.Initialize();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IOC.RegisterIOC(Container);

            // TODO: Register services here

            //moduleCatalog.Register<IMainService, MainService>();

            containerRegistry.RegisterSingleton<StorageManagerService>();
            containerRegistry.RegisterSingleton<AudioPlayerService>();
            containerRegistry.RegisterSingleton<DisposeService>();
        }

        protected override IAvaloniaObject CreateShell()
        {
            //if (IsSingleViewLifetime)
            //    return Container.Resolve<MainControl>(); // For Linux Framebuffer or DRM
            //else
            //    return Container.Resolve<MainWindow>();

            return Container.Resolve<ShellWindowView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // TODO: Register modules

            //moduleCatalog.AddModule<Module1.Module>();
            //moduleCatalog.AddModule<Module2.Module>();
            //moduleCatalog.AddModule<Module3.Module>();
        }

    }
}