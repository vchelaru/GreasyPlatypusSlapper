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
    public partial class Tank : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable, FlatRedBall.Math.Geometry.ICollidable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        public enum TankColor
        {
            Uninitialized = 0, //This exists so that the first set call actually does something
            Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
            Orange = 2, 
            Red = 3, 
            Green = 4, 
            Olive = 5
        }
        protected int mCurrentTankColorState = 0;
        public Entities.Tank.TankColor CurrentTankColorState
        {
            get
            {
                if (mCurrentTankColorState >= 0 && mCurrentTankColorState <= 5)
                {
                    return (TankColor)mCurrentTankColorState;
                }
                else
                {
                    return TankColor.Unknown;
                }
            }
            set
            {
                mCurrentTankColorState = (int)value;
                switch(CurrentTankColorState)
                {
                    case  TankColor.Uninitialized:
                        break;
                    case  TankColor.Unknown:
                        break;
                    case  TankColor.Orange:
                        SpriteInstanceCurrentChainName = "OrangeBody";
                        TurretInstanceCurrentTurretColorState = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
                        break;
                    case  TankColor.Red:
                        SpriteInstanceCurrentChainName = "RedBody";
                        TurretInstanceCurrentTurretColorState = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Red;
                        break;
                    case  TankColor.Green:
                        SpriteInstanceCurrentChainName = "GreenBody";
                        TurretInstanceCurrentTurretColorState = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Green;
                        break;
                    case  TankColor.Olive:
                        SpriteInstanceCurrentChainName = "OliveBody";
                        TurretInstanceCurrentTurretColorState = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Olive;
                        break;
                }
            }
        }
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
        private GreasyPlatypusSlapper.Entities.Turret TurretInstance;
        private FlatRedBall.Sprite TankShadow;
        private GreasyPlatypusSlapper.Entities.Effects.Smoke SmokeInstance;
        public string SpriteInstanceCurrentChainName
        {
            get
            {
                return SpriteInstance.CurrentChainName;
            }
            set
            {
                SpriteInstance.CurrentChainName = value;
            }
        }
        public float DefaultSpeed = 100f;
        public float MudSpeed = 50f;
        public float RoadSpeed = 200f;
        public float TreadSpacing = 5f;
        public float MaxHealth = 100f;
        public float BoostDurationInSeconds = 1f;
        public float BoostTimeoutInSeconds = 5f;
        public float BoostPenaltySpeedMultiplier = 0.1f;
        public float BoostPenaltyDurationInSeconds = 1f;
        public float LowHealthThreshold = 0.3f;
        public float BoostSpeedMultiplier = 5f;
        public Entities.Turret.TurretColor TurretInstanceCurrentTurretColorState
        {
            get
            {
                return TurretInstance.CurrentTurretColorState;
            }
            set
            {
                TurretInstance.CurrentTurretColorState = value;
            }
        }
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
        public Tank () 
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Tank (string contentManagerName) 
        	: this(contentManagerName, true)
        {
        }
        public Tank (string contentManagerName, bool addToManagers) 
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
            TurretInstance = new GreasyPlatypusSlapper.Entities.Turret(ContentManagerName, false);
            TurretInstance.Name = "TurretInstance";
            TankShadow = new FlatRedBall.Sprite();
            TankShadow.Name = "TankShadow";
            SmokeInstance = new GreasyPlatypusSlapper.Entities.Effects.Smoke(ContentManagerName, false);
            SmokeInstance.Name = "SmokeInstance";
            
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
            TurretInstance.ReAddToManagers(LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(TankShadow, LayerProvidedByContainer);
            SmokeInstance.ReAddToManagers(LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
            TurretInstance.AddToManagers(LayerProvidedByContainer);
            FlatRedBall.SpriteManager.AddToLayer(TankShadow, LayerProvidedByContainer);
            SmokeInstance.AddToManagers(LayerProvidedByContainer);
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity () 
        {
            
            TurretInstance.Activity();
            SmokeInstance.Activity();
            CustomActivity();
        }
        public virtual void Destroy () 
        {
            if (Used)
            {
                Factories.TankFactory.MakeUnused(this, false);
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
            if (TurretInstance != null)
            {
                TurretInstance.Destroy();
                TurretInstance.Detach();
            }
            if (TankShadow != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(TankShadow);
            }
            if (SmokeInstance != null)
            {
                SmokeInstance.Destroy();
                SmokeInstance.Detach();
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
                SpriteInstance.Z = 1f;
            }
            else
            {
                SpriteInstance.RelativeZ = 1f;
            }
            SpriteInstance.TextureScale = 1f;
            SpriteInstance.AnimationChains = AnimationChainListFile;
            SpriteInstance.CurrentChainName = "OrangeBody";
            if (mCircleInstance.Parent == null)
            {
                mCircleInstance.CopyAbsoluteToRelative();
                mCircleInstance.AttachTo(this, false);
            }
            CircleInstance.Radius = 16f;
            CircleInstance.Visible = false;
            if (TurretInstance.Parent == null)
            {
                TurretInstance.CopyAbsoluteToRelative();
                TurretInstance.AttachTo(this, false);
            }
            if (TurretInstance.Parent == null)
            {
                TurretInstance.X = 0f;
            }
            else
            {
                TurretInstance.RelativeX = 0f;
            }
            if (TurretInstance.Parent == null)
            {
                TurretInstance.Z = 2f;
            }
            else
            {
                TurretInstance.RelativeZ = 2f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.CopyAbsoluteToRelative();
                TankShadow.AttachTo(this, false);
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.X = 5f;
            }
            else
            {
                TankShadow.RelativeX = 5f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.Y = -5f;
            }
            else
            {
                TankShadow.RelativeY = -5f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.Z = 0.1f;
            }
            else
            {
                TankShadow.RelativeZ = 0.1f;
            }
            TankShadow.TextureScale = 1f;
            TankShadow.UseAnimationRelativePosition = false;
            TankShadow.AnimationChains = AnimationChainListFile;
            TankShadow.CurrentChainName = "TankShadow";
            TankShadow.ParentRotationChangesPosition = false;
            if (SmokeInstance.Parent == null)
            {
                SmokeInstance.CopyAbsoluteToRelative();
                SmokeInstance.AttachTo(this, false);
            }
            if (SmokeInstance.Parent == null)
            {
                SmokeInstance.X = -8f;
            }
            else
            {
                SmokeInstance.RelativeX = -8f;
            }
            if (SmokeInstance.Parent == null)
            {
                SmokeInstance.Z = 3f;
            }
            else
            {
                SmokeInstance.RelativeZ = 3f;
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
            TurretInstance.RemoveFromManagers();
            if (TankShadow != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(TankShadow);
            }
            SmokeInstance.RemoveFromManagers();
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                TurretInstance.AssignCustomVariables(true);
                SmokeInstance.AssignCustomVariables(true);
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Z = 1f;
            }
            else
            {
                SpriteInstance.RelativeZ = 1f;
            }
            SpriteInstance.TextureScale = 1f;
            SpriteInstance.AnimationChains = AnimationChainListFile;
            SpriteInstance.CurrentChainName = "OrangeBody";
            CircleInstance.Radius = 16f;
            CircleInstance.Visible = false;
            if (TurretInstance.Parent == null)
            {
                TurretInstance.X = 0f;
            }
            else
            {
                TurretInstance.RelativeX = 0f;
            }
            if (TurretInstance.Parent == null)
            {
                TurretInstance.Z = 2f;
            }
            else
            {
                TurretInstance.RelativeZ = 2f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.X = 5f;
            }
            else
            {
                TankShadow.RelativeX = 5f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.Y = -5f;
            }
            else
            {
                TankShadow.RelativeY = -5f;
            }
            if (TankShadow.Parent == null)
            {
                TankShadow.Z = 0.1f;
            }
            else
            {
                TankShadow.RelativeZ = 0.1f;
            }
            TankShadow.TextureScale = 1f;
            TankShadow.UseAnimationRelativePosition = false;
            TankShadow.AnimationChains = AnimationChainListFile;
            TankShadow.CurrentChainName = "TankShadow";
            TankShadow.ParentRotationChangesPosition = false;
            if (SmokeInstance.Parent == null)
            {
                SmokeInstance.X = -8f;
            }
            else
            {
                SmokeInstance.RelativeX = -8f;
            }
            if (SmokeInstance.Parent == null)
            {
                SmokeInstance.Z = 3f;
            }
            else
            {
                SmokeInstance.RelativeZ = 3f;
            }
            SpriteInstanceCurrentChainName = "OrangeBody";
            DefaultSpeed = 100f;
            MudSpeed = 50f;
            RoadSpeed = 200f;
            TreadSpacing = 5f;
            MaxHealth = 100f;
            BoostDurationInSeconds = 1f;
            BoostTimeoutInSeconds = 5f;
            BoostPenaltySpeedMultiplier = 0.1f;
            BoostPenaltyDurationInSeconds = 1f;
            LowHealthThreshold = 0.3f;
            BoostSpeedMultiplier = 5f;
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
            TurretInstance.ConvertToManuallyUpdated();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(TankShadow);
            SmokeInstance.ConvertToManuallyUpdated();
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            ContentManagerName = contentManagerName;
            // Set the content manager for Gum
            var contentManagerWrapper = new FlatRedBall.Gum.ContentManagerWrapper();
            contentManagerWrapper.ContentManagerName = contentManagerName;
            RenderingLibrary.Content.LoaderManager.Self.ContentLoader = contentManagerWrapper;
            // Access the GumProject just in case it's async loaded
            var throwaway = GlobalContent.GumProject;
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
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TankStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/tank/animationchainlistfile.achx", ContentManagerName))
                {
                    registerUnload = true;
                }
                AnimationChainListFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/tank/animationchainlistfile.achx", ContentManagerName);
            }
            GreasyPlatypusSlapper.Entities.Turret.LoadStaticContent(contentManagerName);
            GreasyPlatypusSlapper.Entities.Effects.Smoke.LoadStaticContent(contentManagerName);
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TankStaticUnload", UnloadStaticContent);
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
        public FlatRedBall.Instructions.Instruction InterpolateToState (TankColor stateToInterpolateTo, double secondsToTake) 
        {
            switch(stateToInterpolateTo)
            {
                case  TankColor.Orange:
                    TurretInstance.InterpolateToState(GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange, secondsToTake);
                    break;
                case  TankColor.Red:
                    TurretInstance.InterpolateToState(GreasyPlatypusSlapper.Entities.Turret.TurretColor.Red, secondsToTake);
                    break;
                case  TankColor.Green:
                    TurretInstance.InterpolateToState(GreasyPlatypusSlapper.Entities.Turret.TurretColor.Green, secondsToTake);
                    break;
                case  TankColor.Olive:
                    TurretInstance.InterpolateToState(GreasyPlatypusSlapper.Entities.Turret.TurretColor.Olive, secondsToTake);
                    break;
            }
            var instruction = new FlatRedBall.Instructions.DelegateInstruction<TankColor>(StopStateInterpolation, stateToInterpolateTo);
            instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
            this.Instructions.Add(instruction);
            return instruction;
        }
        public void StopStateInterpolation (TankColor stateToStop) 
        {
            switch(stateToStop)
            {
                case  TankColor.Orange:
                    break;
                case  TankColor.Red:
                    break;
                case  TankColor.Green:
                    break;
                case  TankColor.Olive:
                    break;
            }
            CurrentTankColorState = stateToStop;
        }
        public void InterpolateBetween (TankColor firstState, TankColor secondState, float interpolationValue) 
        {
            #if DEBUG
            if (float.IsNaN(interpolationValue))
            {
                throw new System.Exception("interpolationValue cannot be NaN");
            }
            #endif
            bool setTurretInstanceCurrentTurretColorState = true;
            Entities.Turret.TurretColor TurretInstanceCurrentTurretColorStateFirstValue= GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
            Entities.Turret.TurretColor TurretInstanceCurrentTurretColorStateSecondValue= GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
            switch(firstState)
            {
                case  TankColor.Orange:
                    if (interpolationValue < 1)
                    {
                        this.SpriteInstanceCurrentChainName = "OrangeBody";
                    }
                    TurretInstanceCurrentTurretColorStateFirstValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
                    break;
                case  TankColor.Red:
                    if (interpolationValue < 1)
                    {
                        this.SpriteInstanceCurrentChainName = "RedBody";
                    }
                    TurretInstanceCurrentTurretColorStateFirstValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Red;
                    break;
                case  TankColor.Green:
                    if (interpolationValue < 1)
                    {
                        this.SpriteInstanceCurrentChainName = "GreenBody";
                    }
                    TurretInstanceCurrentTurretColorStateFirstValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Green;
                    break;
                case  TankColor.Olive:
                    if (interpolationValue < 1)
                    {
                        this.SpriteInstanceCurrentChainName = "OliveBody";
                    }
                    TurretInstanceCurrentTurretColorStateFirstValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Olive;
                    break;
            }
            switch(secondState)
            {
                case  TankColor.Orange:
                    if (interpolationValue >= 1)
                    {
                        this.SpriteInstanceCurrentChainName = "OrangeBody";
                    }
                    TurretInstanceCurrentTurretColorStateSecondValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
                    break;
                case  TankColor.Red:
                    if (interpolationValue >= 1)
                    {
                        this.SpriteInstanceCurrentChainName = "RedBody";
                    }
                    TurretInstanceCurrentTurretColorStateSecondValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Red;
                    break;
                case  TankColor.Green:
                    if (interpolationValue >= 1)
                    {
                        this.SpriteInstanceCurrentChainName = "GreenBody";
                    }
                    TurretInstanceCurrentTurretColorStateSecondValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Green;
                    break;
                case  TankColor.Olive:
                    if (interpolationValue >= 1)
                    {
                        this.SpriteInstanceCurrentChainName = "OliveBody";
                    }
                    TurretInstanceCurrentTurretColorStateSecondValue = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Olive;
                    break;
            }
            if (setTurretInstanceCurrentTurretColorState)
            {
                TurretInstance.InterpolateBetween(TurretInstanceCurrentTurretColorStateFirstValue, TurretInstanceCurrentTurretColorStateSecondValue, interpolationValue);
            }
            if (interpolationValue < 1)
            {
                mCurrentTankColorState = (int)firstState;
            }
            else
            {
                mCurrentTankColorState = (int)secondState;
            }
        }
        public static void PreloadStateContent (TankColor state, string contentManagerName) 
        {
            ContentManagerName = contentManagerName;
            switch(state)
            {
                case  TankColor.Orange:
                    {
                        object throwaway = "OrangeBody";
                    }
                    {
                        object throwaway = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Orange;
                    }
                    break;
                case  TankColor.Red:
                    {
                        object throwaway = "RedBody";
                    }
                    {
                        object throwaway = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Red;
                    }
                    break;
                case  TankColor.Green:
                    {
                        object throwaway = "GreenBody";
                    }
                    {
                        object throwaway = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Green;
                    }
                    break;
                case  TankColor.Olive:
                    {
                        object throwaway = "OliveBody";
                    }
                    {
                        object throwaway = GreasyPlatypusSlapper.Entities.Turret.TurretColor.Olive;
                    }
                    break;
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
            TurretInstance.SetToIgnorePausing();
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(TankShadow);
            SmokeInstance.SetToIgnorePausing();
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
            TurretInstance.MoveToLayer(layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(TankShadow);
            }
            FlatRedBall.SpriteManager.AddToLayer(TankShadow, layerToMoveTo);
            SmokeInstance.MoveToLayer(layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
