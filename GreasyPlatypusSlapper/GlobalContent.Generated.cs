#if ANDROID || IOS || DESKTOP_GL
// Android doesn't allow background loading. iOS doesn't allow background rendering (which is used by converting textures to use premult alpha)
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;
using GreasyPlatypusSlapper.DataTypes;
using FlatRedBall.IO.Csv;

namespace GreasyPlatypusSlapper
{
    public static partial class GlobalContent
    {
        
        public static Microsoft.Xna.Framework.Graphics.Texture2D spriteSheet { get; set; }
        public static FlatRedBall.Graphics.Animation.AnimationChainList Particles { get; set; }
        public static System.Collections.Generic.Dictionary<string, GreasyPlatypusSlapper.DataTypes.FeatureFlags> FeatureFlags { get; set; }
        public static FlatRedBall.Gum.GumIdb GumProject { get; set; }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "spriteSheet":
                    return spriteSheet;
                case  "Particles":
                    return Particles;
                case  "FeatureFlags":
                    return FeatureFlags;
                case  "GumProject":
                    return GumProject;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "spriteSheet":
                    return spriteSheet;
                case  "Particles":
                    return Particles;
                case  "FeatureFlags":
                    return FeatureFlags;
                case  "GumProject":
                    return GumProject;
            }
            return null;
        }
        public static bool IsInitialized { get; private set; }
        public static bool ShouldStopLoading { get; set; }
        const string ContentManagerName = "Global";
        public static void Initialize () 
        {
            
            spriteSheet = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/spritesheet.png", ContentManagerName);
            Particles = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/globalcontent/particles.achx", ContentManagerName);
            if (FeatureFlags == null)
            {
                {
                    // We put the { and } to limit the scope of oldDelimiter
                    char oldDelimiter = FlatRedBall.IO.Csv.CsvFileManager.Delimiter;
                    FlatRedBall.IO.Csv.CsvFileManager.Delimiter = ',';
                    System.Collections.Generic.Dictionary<string, GreasyPlatypusSlapper.DataTypes.FeatureFlags> temporaryCsvObject = new System.Collections.Generic.Dictionary<string, GreasyPlatypusSlapper.DataTypes.FeatureFlags>();
                    FlatRedBall.IO.Csv.CsvFileManager.CsvDeserializeDictionary<string, GreasyPlatypusSlapper.DataTypes.FeatureFlags>("content/globalcontent/featureflags.csv", temporaryCsvObject);
                    FlatRedBall.IO.Csv.CsvFileManager.Delimiter = oldDelimiter;
                    FeatureFlags = temporaryCsvObject;
                }
            }
            FlatRedBall.Gum.GumIdb.StaticInitialize("content/gumproject/gumproject.gumx"); FlatRedBall.Gum.GumIdbExtensions.RegisterTypes();  FlatRedBall.Gui.GuiManager.BringsClickedWindowsToFront = false;FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged += (not, used) => { FlatRedBall.Gum.GumIdb.UpdateDisplayToMainFrbCamera(); };Gum.Wireframe.GraphicalUiElement.ShowLineRectangles = false;
            			IsInitialized = true;
            // Added by GumPlugin becasue of the Show Mouse checkbox on the .gumx:
            FlatRedBall.FlatRedBallServices.Game.IsMouseVisible = true;
            #if DEBUG && WINDOWS
            InitializeFileWatch();
            #endif
        }
        public static void Reload (object whatToReload) 
        {
            if (whatToReload == FeatureFlags)
            {
                FlatRedBall.IO.Csv.CsvFileManager.UpdateDictionaryValuesFromCsv(FeatureFlags, "content/globalcontent/featureflags.csv");
            }
        }
        #if DEBUG && WINDOWS
        static System.IO.FileSystemWatcher watcher;
        private static void InitializeFileWatch () 
        {
            string globalContent = FlatRedBall.IO.FileManager.RelativeDirectory + "content/globalcontent/";
            if (System.IO.Directory.Exists(globalContent))
            {
                watcher = new System.IO.FileSystemWatcher();
                watcher.Path = globalContent;
                watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
                watcher.Filter = "*.*";
                watcher.Changed += HandleFileChanged;
                watcher.EnableRaisingEvents = true;
            }
        }
        private static void HandleFileChanged (object sender, System.IO.FileSystemEventArgs e) 
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                var fullFileName = e.FullPath;
                var relativeFileName = FlatRedBall.IO.FileManager.MakeRelative(FlatRedBall.IO.FileManager.Standardize(fullFileName));
                if (relativeFileName == "content/globalcontent/spritesheet.png")
                {
                    Reload(spriteSheet);
                }
                if (relativeFileName == "content/globalcontent/particles.achx")
                {
                    Reload(Particles);
                }
                if (relativeFileName == "content/gumproject/gumproject.gumx")
                {
                    Reload(GumProject);
                }
            }
            catch{}
        }
        #endif
        
        
    }
}
