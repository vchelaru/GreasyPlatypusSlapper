#if ANDROID || IOS || DESKTOP_GL
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using GreasyPlatypusSlapper.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using GreasyPlatypusSlapper.Performance;
using GreasyPlatypusSlapper.Entities;
using GreasyPlatypusSlapper.Entities.Effects;
using GreasyPlatypusSlapper.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
namespace GreasyPlatypusSlapper.Entities.Effects
{
    public partial class TreadEffect : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        protected static FlatRedBall.Graphics.Animation.AnimationChainList Particles;
        
        private FlatRedBall.Sprite Tread1;
        static float Tread1XReset;
        static float Tread1YReset;
        static float Tread1ZReset;
        static float Tread1XVelocityReset;
        static float Tread1YVelocityReset;
        static float Tread1ZVelocityReset;
        static float Tread1RotationXReset;
        static float Tread1RotationYReset;
        static float Tread1RotationZReset;
        static float Tread1RotationXVelocityReset;
        static float Tread1RotationYVelocityReset;
        static float Tread1RotationZVelocityReset;
        static float Tread1AlphaReset;
        static float Tread1AlphaRateReset;
        private FlatRedBall.Sprite Tread2;
        static float Tread2XReset;
        static float Tread2YReset;
        static float Tread2ZReset;
        static float Tread2XVelocityReset;
        static float Tread2YVelocityReset;
        static float Tread2ZVelocityReset;
        static float Tread2RotationXReset;
        static float Tread2RotationYReset;
        static float Tread2RotationZReset;
        static float Tread2RotationXVelocityReset;
        static float Tread2RotationYVelocityReset;
        static float Tread2RotationZVelocityReset;
        static float Tread2AlphaReset;
        static float Tread2AlphaRateReset;
        public float AlphaRate = -0.15f;
        public int Index { get; set; }
        public bool Used { get; set; }
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public TreadEffect () 
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public TreadEffect (string contentManagerName) 
        	: this(contentManagerName, true)
        {
        }
        public TreadEffect (string contentManagerName, bool addToManagers) 
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            Tread1 = new FlatRedBall.Sprite();
            Tread1.Name = "Tread1";
            Tread2 = new FlatRedBall.Sprite();
            Tread2.Name = "Tread2";
            
            PostInitialize();
            if (Tread1.Parent == null)
            {
                Tread1XReset = Tread1.X;
            }
            else
            {
                Tread1XReset = Tread1.RelativeX;
            }
            if (Tread1.Parent == null)
            {
                Tread1YReset = Tread1.Y;
            }
            else
            {
                Tread1YReset = Tread1.RelativeY;
            }
            if (Tread1.Parent == null)
            {
                Tread1ZReset = Tread1.Z;
            }
            else
            {
                Tread1ZReset = Tread1.RelativeZ;
            }
            if (Tread1.Parent == null)
            {
                Tread1XVelocityReset = Tread1.XVelocity;
            }
            else
            {
                Tread1XVelocityReset = Tread1.RelativeXVelocity;
            }
            if (Tread1.Parent == null)
            {
                Tread1YVelocityReset = Tread1.YVelocity;
            }
            else
            {
                Tread1YVelocityReset = Tread1.RelativeYVelocity;
            }
            if (Tread1.Parent == null)
            {
                Tread1ZVelocityReset = Tread1.ZVelocity;
            }
            else
            {
                Tread1ZVelocityReset = Tread1.RelativeZVelocity;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationXReset = Tread1.RotationX;
            }
            else
            {
                Tread1RotationXReset = Tread1.RelativeRotationX;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationYReset = Tread1.RotationY;
            }
            else
            {
                Tread1RotationYReset = Tread1.RelativeRotationY;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationZReset = Tread1.RotationZ;
            }
            else
            {
                Tread1RotationZReset = Tread1.RelativeRotationZ;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationXVelocityReset = Tread1.RotationXVelocity;
            }
            else
            {
                Tread1RotationXVelocityReset = Tread1.RelativeRotationXVelocity;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationYVelocityReset = Tread1.RotationYVelocity;
            }
            else
            {
                Tread1RotationYVelocityReset = Tread1.RelativeRotationYVelocity;
            }
            if (Tread1.Parent == null)
            {
                Tread1RotationZVelocityReset = Tread1.RotationZVelocity;
            }
            else
            {
                Tread1RotationZVelocityReset = Tread1.RelativeRotationZVelocity;
            }
            Tread1AlphaReset = Tread1.Alpha;
            Tread1AlphaRateReset = Tread1.AlphaRate;
            if (Tread2.Parent == null)
            {
                Tread2XReset = Tread2.X;
            }
            else
            {
                Tread2XReset = Tread2.RelativeX;
            }
            if (Tread2.Parent == null)
            {
                Tread2YReset = Tread2.Y;
            }
            else
            {
                Tread2YReset = Tread2.RelativeY;
            }
            if (Tread2.Parent == null)
            {
                Tread2ZReset = Tread2.Z;
            }
            else
            {
                Tread2ZReset = Tread2.RelativeZ;
            }
            if (Tread2.Parent == null)
            {
                Tread2XVelocityReset = Tread2.XVelocity;
            }
            else
            {
                Tread2XVelocityReset = Tread2.RelativeXVelocity;
            }
            if (Tread2.Parent == null)
            {
                Tread2YVelocityReset = Tread2.YVelocity;
            }
            else
            {
                Tread2YVelocityReset = Tread2.RelativeYVelocity;
            }
            if (Tread2.Parent == null)
            {
                Tread2ZVelocityReset = Tread2.ZVelocity;
            }
            else
            {
                Tread2ZVelocityReset = Tread2.RelativeZVelocity;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationXReset = Tread2.RotationX;
            }
            else
            {
                Tread2RotationXReset = Tread2.RelativeRotationX;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationYReset = Tread2.RotationY;
            }
            else
            {
                Tread2RotationYReset = Tread2.RelativeRotationY;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationZReset = Tread2.RotationZ;
            }
            else
            {
                Tread2RotationZReset = Tread2.RelativeRotationZ;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationXVelocityReset = Tread2.RotationXVelocity;
            }
            else
            {
                Tread2RotationXVelocityReset = Tread2.RelativeRotationXVelocity;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationYVelocityReset = Tread2.RotationYVelocity;
            }
            else
            {
                Tread2RotationYVelocityReset = Tread2.RelativeRotationYVelocity;
            }
            if (Tread2.Parent == null)
            {
                Tread2RotationZVelocityReset = Tread2.RotationZVelocity;
            }
            else
            {
                Tread2RotationZVelocityReset = Tread2.RelativeRotationZVelocity;
            }
            Tread2AlphaReset = Tread2.Alpha;
            Tread2AlphaRateReset = Tread2.AlphaRate;
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }
        public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(Tread1, LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(Tread2, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            PostInitialize();
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(Tread1, LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(Tread2, LayerProvidedByContainer);
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity () 
        {
            
            CustomActivity();
        }
        public virtual void Destroy () 
        {
            if (Used)
            {
                Factories.TreadEffectFactory.MakeUnused(this, false);
            }
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (Tread1 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(Tread1);
            }
            if (Tread2 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(Tread2);
            }
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (Tread1.Parent == null)
            {
                Tread1.CopyAbsoluteToRelative();
                Tread1.AttachTo(this, false);
            }
            if (Tread1.Parent == null)
            {
                Tread1.Y = 10f;
            }
            else
            {
                Tread1.RelativeY = 10f;
            }
            if (Tread1.Parent == null)
            {
                Tread1.Z = 0.1f;
            }
            else
            {
                Tread1.RelativeZ = 0.1f;
            }
            Tread1.TextureScale = 1f;
            Tread1.UseAnimationRelativePosition = false;
            Tread1.Animate = false;
            Tread1.AnimationChains = Particles;
            Tread1.CurrentChainName = "TankTread";
            if (Tread2.Parent == null)
            {
                Tread2.CopyAbsoluteToRelative();
                Tread2.AttachTo(this, false);
            }
            if (Tread2.Parent == null)
            {
                Tread2.Y = -10f;
            }
            else
            {
                Tread2.RelativeY = -10f;
            }
            if (Tread2.Parent == null)
            {
                Tread2.Z = 0.1f;
            }
            else
            {
                Tread2.RelativeZ = 0.1f;
            }
            Tread2.TextureScale = 1f;
            Tread2.UseAnimationRelativePosition = false;
            Tread2.Animate = false;
            Tread2.AnimationChains = Particles;
            Tread2.CurrentChainName = "TankTread";
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (Tread1 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(Tread1);
            }
            if (Tread2 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(Tread2);
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
            }
            if (Tread1.Parent == null)
            {
                Tread1.Y = 10f;
            }
            else
            {
                Tread1.RelativeY = 10f;
            }
            if (Tread1.Parent == null)
            {
                Tread1.Z = 0.1f;
            }
            else
            {
                Tread1.RelativeZ = 0.1f;
            }
            Tread1.TextureScale = 1f;
            Tread1.UseAnimationRelativePosition = false;
            Tread1.Animate = false;
            Tread1.AnimationChains = Particles;
            Tread1.CurrentChainName = "TankTread";
            if (Tread1.Parent == null)
            {
                Tread1.X = Tread1XReset;
            }
            else
            {
                Tread1.RelativeX = Tread1XReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.Y = Tread1YReset;
            }
            else
            {
                Tread1.RelativeY = Tread1YReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.Z = Tread1ZReset;
            }
            else
            {
                Tread1.RelativeZ = Tread1ZReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.XVelocity = Tread1XVelocityReset;
            }
            else
            {
                Tread1.RelativeXVelocity = Tread1XVelocityReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.YVelocity = Tread1YVelocityReset;
            }
            else
            {
                Tread1.RelativeYVelocity = Tread1YVelocityReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.ZVelocity = Tread1ZVelocityReset;
            }
            else
            {
                Tread1.RelativeZVelocity = Tread1ZVelocityReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationX = Tread1RotationXReset;
            }
            else
            {
                Tread1.RelativeRotationX = Tread1RotationXReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationY = Tread1RotationYReset;
            }
            else
            {
                Tread1.RelativeRotationY = Tread1RotationYReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationZ = Tread1RotationZReset;
            }
            else
            {
                Tread1.RelativeRotationZ = Tread1RotationZReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationXVelocity = Tread1RotationXVelocityReset;
            }
            else
            {
                Tread1.RelativeRotationXVelocity = Tread1RotationXVelocityReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationYVelocity = Tread1RotationYVelocityReset;
            }
            else
            {
                Tread1.RelativeRotationYVelocity = Tread1RotationYVelocityReset;
            }
            if (Tread1.Parent == null)
            {
                Tread1.RotationZVelocity = Tread1RotationZVelocityReset;
            }
            else
            {
                Tread1.RelativeRotationZVelocity = Tread1RotationZVelocityReset;
            }
            Tread1.Alpha = Tread1AlphaReset;
            Tread1.AlphaRate = Tread1AlphaRateReset;
            if (Tread2.Parent == null)
            {
                Tread2.Y = -10f;
            }
            else
            {
                Tread2.RelativeY = -10f;
            }
            if (Tread2.Parent == null)
            {
                Tread2.Z = 0.1f;
            }
            else
            {
                Tread2.RelativeZ = 0.1f;
            }
            Tread2.TextureScale = 1f;
            Tread2.UseAnimationRelativePosition = false;
            Tread2.Animate = false;
            Tread2.AnimationChains = Particles;
            Tread2.CurrentChainName = "TankTread";
            if (Tread2.Parent == null)
            {
                Tread2.X = Tread2XReset;
            }
            else
            {
                Tread2.RelativeX = Tread2XReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.Y = Tread2YReset;
            }
            else
            {
                Tread2.RelativeY = Tread2YReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.Z = Tread2ZReset;
            }
            else
            {
                Tread2.RelativeZ = Tread2ZReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.XVelocity = Tread2XVelocityReset;
            }
            else
            {
                Tread2.RelativeXVelocity = Tread2XVelocityReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.YVelocity = Tread2YVelocityReset;
            }
            else
            {
                Tread2.RelativeYVelocity = Tread2YVelocityReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.ZVelocity = Tread2ZVelocityReset;
            }
            else
            {
                Tread2.RelativeZVelocity = Tread2ZVelocityReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationX = Tread2RotationXReset;
            }
            else
            {
                Tread2.RelativeRotationX = Tread2RotationXReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationY = Tread2RotationYReset;
            }
            else
            {
                Tread2.RelativeRotationY = Tread2RotationYReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationZ = Tread2RotationZReset;
            }
            else
            {
                Tread2.RelativeRotationZ = Tread2RotationZReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationXVelocity = Tread2RotationXVelocityReset;
            }
            else
            {
                Tread2.RelativeRotationXVelocity = Tread2RotationXVelocityReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationYVelocity = Tread2RotationYVelocityReset;
            }
            else
            {
                Tread2.RelativeRotationYVelocity = Tread2RotationYVelocityReset;
            }
            if (Tread2.Parent == null)
            {
                Tread2.RotationZVelocity = Tread2RotationZVelocityReset;
            }
            else
            {
                Tread2.RelativeRotationZVelocity = Tread2RotationZVelocityReset;
            }
            Tread2.Alpha = Tread2AlphaReset;
            Tread2.AlphaRate = Tread2AlphaRateReset;
            AlphaRate = -0.15f;
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Tread1);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Tread2);
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            ContentManagerName = contentManagerName;
            #if DEBUG
            if (contentManagerName == FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                HasBeenLoadedWithGlobalContentManager = true;
            }
            else if (HasBeenLoadedWithGlobalContentManager)
            {
                throw new System.Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
            }
            #endif
            bool registerUnload = false;
            if (LoadedContentManagers.Contains(contentManagerName) == false)
            {
                LoadedContentManagers.Add(contentManagerName);
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TreadEffectStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/globalcontent/particles.achx", ContentManagerName))
                {
                    registerUnload = true;
                }
                Particles = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/globalcontent/particles.achx", ContentManagerName);
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TreadEffectStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
            }
            CustomLoadStaticContent(contentManagerName);
        }
        public static void UnloadStaticContent () 
        {
            if (LoadedContentManagers.Count != 0)
            {
                LoadedContentManagers.RemoveAt(0);
                mRegisteredUnloads.RemoveAt(0);
            }
            if (LoadedContentManagers.Count == 0)
            {
                if (Particles != null)
                {
                    Particles= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Particles":
                    return Particles;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "Particles":
                    return Particles;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Particles":
                    return Particles;
            }
            return null;
        }
        protected bool mIsPaused;
        public override void Pause (FlatRedBall.Instructions.InstructionList instructions) 
        {
            base.Pause(instructions);
            mIsPaused = true;
        }
        public virtual void SetToIgnorePausing () 
        {
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Tread1);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Tread2);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo) 
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(Tread1);
            }
            FlatRedBall.SpriteManager.AddToLayer(Tread1, layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(Tread2);
            }
            FlatRedBall.SpriteManager.AddToLayer(Tread2, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
