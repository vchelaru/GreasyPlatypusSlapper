#if ANDROID || IOS || DESKTOP_GL
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using GreasyPlatypusSlapper.Entities;
using GreasyPlatypusSlapper.Entities.Effects;
using GreasyPlatypusSlapper.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
namespace GreasyPlatypusSlapper.Screens
{
    public partial class GameScreen : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        protected static FlatRedBall.TileGraphics.LayeredTileMap TestLevel;
        protected static FlatRedBall.Gum.GumIdb GameScreenGum;
        
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Tank> TankList;
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Bullet> BulletList;
        private GreasyPlatypusSlapper.Entities.CameraEntity CameraEntityInstance;
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Effects.TreadEffect> TreadEffects;
        private GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime PlayerSelectionUIInstance;
        public GameScreen () 
        	: base ("GameScreen")
        {
        }
        public override void Initialize (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            TankList = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Tank>();
            TankList.Name = "TankList";
            BulletList = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Bullet>();
            BulletList.Name = "BulletList";
            CameraEntityInstance = new GreasyPlatypusSlapper.Entities.CameraEntity(ContentManagerName, false);
            CameraEntityInstance.Name = "CameraEntityInstance";
            TreadEffects = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Effects.TreadEffect>();
            TreadEffects.Name = "TreadEffects";
            PlayerSelectionUIInstance = GameScreenGum.GetGraphicalUiElementByName("PlayerSelectionUIInstance") as GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime;
            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers () 
        {
            TestLevel.AddToManagers(mLayer);
            GameScreenGum.InstanceInitialize(); FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged += GameScreenGum.HandleResolutionChanged;
            Factories.TankFactory.Initialize(ContentManagerName);
            Factories.BulletFactory.Initialize(ContentManagerName);
            Factories.TreadEffectFactory.Initialize(ContentManagerName);
            Factories.TankFactory.AddList(TankList);
            Factories.BulletFactory.AddList(BulletList);
            Factories.TreadEffectFactory.AddList(TreadEffects);
            CameraEntityInstance.AddToManagers(mLayer);
            base.AddToManagers();
            AddToManagersBottomUp();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled) 
        {
            if (!IsPaused)
            {
                
                for (int i = TankList.Count - 1; i > -1; i--)
                {
                    if (i < TankList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        TankList[i].Activity();
                    }
                }
                for (int i = BulletList.Count - 1; i > -1; i--)
                {
                    if (i < BulletList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        BulletList[i].Activity();
                    }
                }
                CameraEntityInstance.Activity();
                for (int i = TreadEffects.Count - 1; i > -1; i--)
                {
                    if (i < TreadEffects.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        TreadEffects[i].Activity();
                    }
                }
            }
            else
            {
            }
            base.Activity(firstTimeCalled);
            if (!IsActivityFinished)
            {
                CustomActivity(firstTimeCalled);
            }
        }
        public override void Destroy () 
        {
            base.Destroy();
            Factories.TankFactory.Destroy();
            Factories.BulletFactory.Destroy();
            Factories.TreadEffectFactory.Destroy();
            TestLevel.Destroy();
            TestLevel = null;
            FlatRedBall.SpriteManager.RemoveDrawableBatch(GameScreenGum); FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged -= GameScreenGum.HandleResolutionChanged;
            GameScreenGum = null;
            
            TankList.MakeOneWay();
            BulletList.MakeOneWay();
            TreadEffects.MakeOneWay();
            for (int i = TankList.Count - 1; i > -1; i--)
            {
                TankList[i].Destroy();
            }
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            if (CameraEntityInstance != null)
            {
                CameraEntityInstance.Destroy();
                CameraEntityInstance.Detach();
            }
            for (int i = TreadEffects.Count - 1; i > -1; i--)
            {
                TreadEffects[i].Destroy();
            }
            if (PlayerSelectionUIInstance != null)
            {
                PlayerSelectionUIInstance.RemoveFromManagers();
            }
            TankList.MakeTwoWay();
            BulletList.MakeTwoWay();
            TreadEffects.MakeTwoWay();
            FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Clear();
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp () 
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            for (int i = TankList.Count - 1; i > -1; i--)
            {
                TankList[i].Destroy();
            }
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            CameraEntityInstance.RemoveFromManagers();
            for (int i = TreadEffects.Count - 1; i > -1; i--)
            {
                TreadEffects[i].Destroy();
            }
            if (PlayerSelectionUIInstance != null)
            {
                PlayerSelectionUIInstance.RemoveFromManagers();
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                CameraEntityInstance.AssignCustomVariables(true);
            }
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            for (int i = 0; i < TankList.Count; i++)
            {
                TankList[i].ConvertToManuallyUpdated();
            }
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].ConvertToManuallyUpdated();
            }
            CameraEntityInstance.ConvertToManuallyUpdated();
            for (int i = 0; i < TreadEffects.Count; i++)
            {
                TreadEffects[i].ConvertToManuallyUpdated();
            }
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
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
            TestLevel = FlatRedBall.TileGraphics.LayeredTileMap.FromTiledMapSave("content/screens/gamescreen/levels/testlevel.tmx", contentManagerName);
            Gum.Wireframe.GraphicalUiElement.IsAllLayoutSuspended = true;  GameScreenGum = new FlatRedBall.Gum.GumIdb();  GameScreenGum.LoadFromFile("content/gumproject/screens/gamescreengum.gusx");  GameScreenGum.AssignReferences();Gum.Wireframe.GraphicalUiElement.IsAllLayoutSuspended = false; GameScreenGum.Element.UpdateLayout(); GameScreenGum.Element.UpdateLayout();
            GreasyPlatypusSlapper.Entities.CameraEntity.LoadStaticContent(contentManagerName);
            CustomLoadStaticContent(contentManagerName);
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "TestLevel":
                    return TestLevel;
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "TestLevel":
                    return TestLevel;
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "TestLevel":
                    return TestLevel;
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
    }
}
