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
using FlatRedBall.Math.Geometry;
namespace GreasyPlatypusSlapper.Entities
{
    public partial class Bullet : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable, FlatRedBall.Math.Geometry.ICollidable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimationChainListFile;
        
        private FlatRedBall.Sprite SpriteInstance;
        private FlatRedBall.Math.Geometry.Circle mCircleInstance;
        public FlatRedBall.Math.Geometry.Circle CircleInstance
        {
            get
            {
                return mCircleInstance;
            }
            private set
            {
                mCircleInstance = value;
            }
        }
        private FlatRedBall.Sprite BulletShadow;
        private GreasyPlatypusSlapper.Entities.Effects.MissileTrail MissileTrailInstance;
        public float Damage = 25f;
        public int Index { get; set; }
        public bool Used { get; set; }
        private FlatRedBall.Math.Geometry.ShapeCollection mGeneratedCollision;
        public FlatRedBall.Math.Geometry.ShapeCollection Collision
        {
            get
            {
                return mGeneratedCollision;
            }
        }
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public Bullet () 
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Bullet (string contentManagerName) 
        	: this(contentManagerName, true)
        {
        }
        public Bullet (string contentManagerName, bool addToManagers) 
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            SpriteInstance = new FlatRedBall.Sprite();
            SpriteInstance.Name = "SpriteInstance";
            mCircleInstance = new FlatRedBall.Math.Geometry.Circle();
            mCircleInstance.Name = "mCircleInstance";
            BulletShadow = new FlatRedBall.Sprite();
            BulletShadow.Name = "BulletShadow";
            MissileTrailInstance = new GreasyPlatypusSlapper.Entities.Effects.MissileTrail(ContentManagerName, false);
            MissileTrailInstance.Name = "MissileTrailInstance";
            
            PostInitialize();
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }
        public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(BulletShadow, LayerProvidedByContainer);
            MissileTrailInstance.ReAddToManagers(LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(BulletShadow, LayerProvidedByContainer);
            MissileTrailInstance.AddToManagers(LayerProvidedByContainer);
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity () 
        {
            
            MissileTrailInstance.Activity();
            CustomActivity();
        }
        public virtual void Destroy () 
        {
            if (Used)
            {
                Factories.BulletFactory.MakeUnused(this, false);
            }
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (CircleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(CircleInstance);
            }
            if (BulletShadow != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(BulletShadow);
            }
            if (MissileTrailInstance != null)
            {
                MissileTrailInstance.Destroy();
                MissileTrailInstance.Detach();
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.CopyAbsoluteToRelative();
                SpriteInstance.AttachTo(this, false);
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Z = 2f;
            }
            else
            {
                SpriteInstance.RelativeZ = 2f;
            }
            SpriteInstance.TextureScale = 1f;
            SpriteInstance.AnimationChains = AnimationChainListFile;
            SpriteInstance.CurrentChainName = "OrangeMissile";
            if (mCircleInstance.Parent == null)
            {
                mCircleInstance.CopyAbsoluteToRelative();
                mCircleInstance.AttachTo(this, false);
            }
            CircleInstance.Radius = 3f;
            CircleInstance.Visible = false;
            if (BulletShadow.Parent == null)
            {
                BulletShadow.CopyAbsoluteToRelative();
                BulletShadow.AttachTo(this, false);
            }
            if (BulletShadow.Parent == null)
            {
                BulletShadow.X = 10f;
            }
            else
            {
                BulletShadow.RelativeX = 10f;
            }
            if (BulletShadow.Parent == null)
            {
                BulletShadow.Y = -10f;
            }
            else
            {
                BulletShadow.RelativeY = -10f;
            }
            if (BulletShadow.Parent == null)
            {
                BulletShadow.Z = 0.1f;
            }
            else
            {
                BulletShadow.RelativeZ = 0.1f;
            }
            BulletShadow.TextureScale = 1f;
            BulletShadow.UseAnimationRelativePosition = false;
            BulletShadow.AnimationChains = AnimationChainListFile;
            BulletShadow.CurrentChainName = "MissileShadow";
            BulletShadow.ParentRotationChangesPosition = false;
            if (MissileTrailInstance.Parent == null)
            {
                MissileTrailInstance.CopyAbsoluteToRelative();
                MissileTrailInstance.AttachTo(this, false);
            }
            if (MissileTrailInstance.Parent == null)
            {
                MissileTrailInstance.X = -20f;
            }
            else
            {
                MissileTrailInstance.RelativeX = -20f;
            }
            if (MissileTrailInstance.Parent == null)
            {
                MissileTrailInstance.Z = 2f;
            }
            else
            {
                MissileTrailInstance.RelativeZ = 2f;
            }
            mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
            mGeneratedCollision.Circles.AddOneWay(mCircleInstance);
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (CircleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(CircleInstance);
            }
            if (BulletShadow != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(BulletShadow);
            }
            MissileTrailInstance.RemoveFromManagers();
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                MissileTrailInstance.AssignCustomVariables(true);
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Z = 2f;
            }
            else
            {
                SpriteInstance.RelativeZ = 2f;
            }
            SpriteInstance.TextureScale = 1f;
            SpriteInstance.AnimationChains = AnimationChainListFile;
            SpriteInstance.CurrentChainName = "OrangeMissile";
            CircleInstance.Radius = 3f;
            CircleInstance.Visible = false;
            if (BulletShadow.Parent == null)
            {
                BulletShadow.X = 10f;
            }
            else
            {
                BulletShadow.RelativeX = 10f;
            }
            if (BulletShadow.Parent == null)
            {
                BulletShadow.Y = -10f;
            }
            else
            {
                BulletShadow.RelativeY = -10f;
            }
            if (BulletShadow.Parent == null)
            {
                BulletShadow.Z = 0.1f;
            }
            else
            {
                BulletShadow.RelativeZ = 0.1f;
            }
            BulletShadow.TextureScale = 1f;
            BulletShadow.UseAnimationRelativePosition = false;
            BulletShadow.AnimationChains = AnimationChainListFile;
            BulletShadow.CurrentChainName = "MissileShadow";
            BulletShadow.ParentRotationChangesPosition = false;
            if (MissileTrailInstance.Parent == null)
            {
                MissileTrailInstance.X = -20f;
            }
            else
            {
                MissileTrailInstance.RelativeX = -20f;
            }
            if (MissileTrailInstance.Parent == null)
            {
                MissileTrailInstance.Z = 2f;
            }
            else
            {
                MissileTrailInstance.RelativeZ = 2f;
            }
            Damage = 25f;
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(BulletShadow);
            MissileTrailInstance.ConvertToManuallyUpdated();
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
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BulletStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/bullet/animationchainlistfile.achx", ContentManagerName))
                {
                    registerUnload = true;
                }
                AnimationChainListFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/bullet/animationchainlistfile.achx", ContentManagerName);
            }
            GreasyPlatypusSlapper.Entities.Effects.MissileTrail.LoadStaticContent(contentManagerName);
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BulletStaticUnload", UnloadStaticContent);
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
                if (AnimationChainListFile != null)
                {
                    AnimationChainListFile= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "AnimationChainListFile":
                    return AnimationChainListFile;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "AnimationChainListFile":
                    return AnimationChainListFile;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "AnimationChainListFile":
                    return AnimationChainListFile;
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
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(CircleInstance);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(BulletShadow);
            MissileTrailInstance.SetToIgnorePausing();
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo) 
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(SpriteInstance);
            }
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(CircleInstance);
            }
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(CircleInstance, layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(BulletShadow);
            }
            FlatRedBall.SpriteManager.AddToLayer(BulletShadow, layerToMoveTo);
            MissileTrailInstance.MoveToLayer(layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
