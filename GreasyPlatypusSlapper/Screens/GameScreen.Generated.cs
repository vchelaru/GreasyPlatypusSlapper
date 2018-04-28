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
        
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Tank> TankList;
        private GreasyPlatypusSlapper.Entities.Tank Tank1Test;
        private GreasyPlatypusSlapper.Entities.Tank Tank2Test;
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Bullet> BulletList;
        private GreasyPlatypusSlapper.Entities.CameraEntity CameraEntityInstance;
        private FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Effects.TreadEffect> TreadEffects;
        public GameScreen () 
        	: base ("GameScreen")
        {
        }
        public override void Initialize (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            TankList = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Tank>();
            TankList.Name = "TankList";
            Tank1Test = new GreasyPlatypusSlapper.Entities.Tank(ContentManagerName, false);
            Tank1Test.Name = "Tank1Test";
            Tank2Test = new GreasyPlatypusSlapper.Entities.Tank(ContentManagerName, false);
            Tank2Test.Name = "Tank2Test";
            BulletList = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Bullet>();
            BulletList.Name = "BulletList";
            CameraEntityInstance = new GreasyPlatypusSlapper.Entities.CameraEntity(ContentManagerName, false);
            CameraEntityInstance.Name = "CameraEntityInstance";
            TreadEffects = new FlatRedBall.Math.PositionedObjectList<GreasyPlatypusSlapper.Entities.Effects.TreadEffect>();
            TreadEffects.Name = "TreadEffects";
            
            
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
            Factories.BulletFactory.Initialize(ContentManagerName);
            Factories.TreadEffectFactory.Initialize(ContentManagerName);
            Factories.BulletFactory.AddList(BulletList);
            Factories.TreadEffectFactory.AddList(TreadEffects);
            Tank1Test.AddToManagers(mLayer);
            Tank2Test.AddToManagers(mLayer);
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
            Factories.BulletFactory.Destroy();
            Factories.TreadEffectFactory.Destroy();
            TestLevel.Destroy();
            TestLevel = null;
            
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
            TankList.Add(Tank1Test);
            if (Tank1Test.Parent == null)
            {
                Tank1Test.X = 200f;
            }
            else
            {
                Tank1Test.RelativeX = 200f;
            }
            if (Tank1Test.Parent == null)
            {
                Tank1Test.Y = -200f;
            }
            else
            {
                Tank1Test.RelativeY = -200f;
            }
            if (Tank1Test.Parent == null)
            {
                Tank1Test.Z = 1f;
            }
            else
            {
                Tank1Test.RelativeZ = 1f;
            }
            TankList.Add(Tank2Test);
            if (Tank2Test.Parent == null)
            {
                Tank2Test.X = 400f;
            }
            else
            {
                Tank2Test.RelativeX = 400f;
            }
            if (Tank2Test.Parent == null)
            {
                Tank2Test.Y = -200f;
            }
            else
            {
                Tank2Test.RelativeY = -200f;
            }
            if (Tank2Test.Parent == null)
            {
                Tank2Test.Z = 1f;
            }
            else
            {
                Tank2Test.RelativeZ = 1f;
            }
            Tank2Test.SpriteInstanceCurrentChainName = "GrayBody";
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
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                Tank1Test.AssignCustomVariables(true);
                Tank2Test.AssignCustomVariables(true);
                CameraEntityInstance.AssignCustomVariables(true);
            }
            if (Tank1Test.Parent == null)
            {
                Tank1Test.X = 200f;
            }
            else
            {
                Tank1Test.RelativeX = 200f;
            }
            if (Tank1Test.Parent == null)
            {
                Tank1Test.Y = -200f;
            }
            else
            {
                Tank1Test.RelativeY = -200f;
            }
            if (Tank1Test.Parent == null)
            {
                Tank1Test.Z = 1f;
            }
            else
            {
                Tank1Test.RelativeZ = 1f;
            }
            if (Tank2Test.Parent == null)
            {
                Tank2Test.X = 400f;
            }
            else
            {
                Tank2Test.RelativeX = 400f;
            }
            if (Tank2Test.Parent == null)
            {
                Tank2Test.Y = -200f;
            }
            else
            {
                Tank2Test.RelativeY = -200f;
            }
            if (Tank2Test.Parent == null)
            {
                Tank2Test.Z = 1f;
            }
            else
            {
                Tank2Test.RelativeZ = 1f;
            }
            Tank2Test.SpriteInstanceCurrentChainName = "GrayBody";
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
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "TestLevel":
                    return TestLevel;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "TestLevel":
                    return TestLevel;
            }
            return null;
        }
    }
}
