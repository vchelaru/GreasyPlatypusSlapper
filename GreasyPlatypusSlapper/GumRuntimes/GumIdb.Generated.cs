    namespace FlatRedBall.Gum
    {
        public  class GumIdbExtensions
        {
            public static void RegisterTypes () 
            {
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Circle", typeof(GreasyPlatypusSlapper.GumRuntimes.CircleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("ColoredRectangle", typeof(GreasyPlatypusSlapper.GumRuntimes.ColoredRectangleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Container", typeof(GreasyPlatypusSlapper.GumRuntimes.ContainerRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("NineSlice", typeof(GreasyPlatypusSlapper.GumRuntimes.NineSliceRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Rectangle", typeof(GreasyPlatypusSlapper.GumRuntimes.RectangleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Sprite", typeof(GreasyPlatypusSlapper.GumRuntimes.SpriteRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Text", typeof(GreasyPlatypusSlapper.GumRuntimes.TextRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("PlayerSelectionBox", typeof(GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("PlayerSelectionUI", typeof(GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("GameScreenGum", typeof(GreasyPlatypusSlapper.GumRuntimes.GameScreenGumRuntime));
            }
        }
    }
