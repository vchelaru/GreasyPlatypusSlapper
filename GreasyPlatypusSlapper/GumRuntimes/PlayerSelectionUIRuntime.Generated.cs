    using System.Linq;
    namespace GreasyPlatypusSlapper.GumRuntimes
    {
        public partial class PlayerSelectionUIRuntime : GreasyPlatypusSlapper.GumRuntimes.ContainerRuntime
        {
            #region State Enums
            public enum VariableState
            {
                Default
            }
            public enum Visibility
            {
                Shown,
                Hidden
            }
            public enum ReadyToStart
            {
                Yes,
                No
            }
            #endregion
            #region State Fields
            VariableState mCurrentVariableState;
            Visibility mCurrentVisibilityState;
            ReadyToStart mCurrentReadyToStartState;
            #endregion
            #region State Properties
            public VariableState CurrentVariableState
            {
                get
                {
                    return mCurrentVariableState;
                }
                set
                {
                    mCurrentVariableState = value;
                    switch(mCurrentVariableState)
                    {
                        case  VariableState.Default:
                            Height = 100f;
                            HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            Width = 100f;
                            WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            XUnits = Gum.Converters.GeneralUnitType.Percentage;
                            Background.Blue = 0;
                            Background.Green = 0;
                            Background.Height = 100f;
                            Background.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            Background.Red = 0;
                            Background.Width = 100f;
                            Background.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            Instructions.FontSize = 42;
                            Instructions.Height = 10f;
                            Instructions.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            Instructions.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                            Instructions.Text = "Press Any Button To Join";
                            Instructions.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                            Instructions.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            Instructions.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                            Instructions.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                            Instructions.Y = 80f;
                            Instructions.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                            Instructions.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                            PlayerList.ChildrenLayout = Gum.Managers.ChildrenLayout.LeftToRightStack;
                            PlayerList.Height = 80f;
                            PlayerList.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            PlayerList.Width = 100f;
                            PlayerList.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            PlayerList.WrapsChildren = true;
                            StartToBegin.FontSize = 32;
                            StartToBegin.Height = 10f;
                            StartToBegin.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            StartToBegin.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                            StartToBegin.Text = "(Press Start to Begin)";
                            StartToBegin.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                            StartToBegin.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                            StartToBegin.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                            StartToBegin.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                            StartToBegin.Y = 90f;
                            StartToBegin.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                            StartToBegin.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                            break;
                    }
                }
            }
            public Visibility CurrentVisibilityState
            {
                get
                {
                    return mCurrentVisibilityState;
                }
                set
                {
                    mCurrentVisibilityState = value;
                    switch(mCurrentVisibilityState)
                    {
                        case  Visibility.Shown:
                            X = 0f;
                            break;
                        case  Visibility.Hidden:
                            X = -100f;
                            break;
                    }
                }
            }
            public ReadyToStart CurrentReadyToStartState
            {
                get
                {
                    return mCurrentReadyToStartState;
                }
                set
                {
                    mCurrentReadyToStartState = value;
                    switch(mCurrentReadyToStartState)
                    {
                        case  ReadyToStart.Yes:
                            StartToBegin.Visible = true;
                            break;
                        case  ReadyToStart.No:
                            StartToBegin.Visible = false;
                            break;
                    }
                }
            }
            #endregion
            #region State Interpolation
            public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue) 
            {
                #if DEBUG
                if (float.IsNaN(interpolationValue))
                {
                    throw new System.Exception("interpolationValue cannot be NaN");
                }
                #endif
                bool setBackgroundBlueFirstValue = false;
                bool setBackgroundBlueSecondValue = false;
                int BackgroundBlueFirstValue= 0;
                int BackgroundBlueSecondValue= 0;
                bool setBackgroundGreenFirstValue = false;
                bool setBackgroundGreenSecondValue = false;
                int BackgroundGreenFirstValue= 0;
                int BackgroundGreenSecondValue= 0;
                bool setBackgroundHeightFirstValue = false;
                bool setBackgroundHeightSecondValue = false;
                float BackgroundHeightFirstValue= 0;
                float BackgroundHeightSecondValue= 0;
                bool setBackgroundRedFirstValue = false;
                bool setBackgroundRedSecondValue = false;
                int BackgroundRedFirstValue= 0;
                int BackgroundRedSecondValue= 0;
                bool setBackgroundWidthFirstValue = false;
                bool setBackgroundWidthSecondValue = false;
                float BackgroundWidthFirstValue= 0;
                float BackgroundWidthSecondValue= 0;
                bool setHeightFirstValue = false;
                bool setHeightSecondValue = false;
                float HeightFirstValue= 0;
                float HeightSecondValue= 0;
                bool setInstructionsFontSizeFirstValue = false;
                bool setInstructionsFontSizeSecondValue = false;
                int InstructionsFontSizeFirstValue= 0;
                int InstructionsFontSizeSecondValue= 0;
                bool setInstructionsHeightFirstValue = false;
                bool setInstructionsHeightSecondValue = false;
                float InstructionsHeightFirstValue= 0;
                float InstructionsHeightSecondValue= 0;
                bool setInstructionsYFirstValue = false;
                bool setInstructionsYSecondValue = false;
                float InstructionsYFirstValue= 0;
                float InstructionsYSecondValue= 0;
                bool setPlayerListHeightFirstValue = false;
                bool setPlayerListHeightSecondValue = false;
                float PlayerListHeightFirstValue= 0;
                float PlayerListHeightSecondValue= 0;
                bool setPlayerListWidthFirstValue = false;
                bool setPlayerListWidthSecondValue = false;
                float PlayerListWidthFirstValue= 0;
                float PlayerListWidthSecondValue= 0;
                bool setStartToBeginFontSizeFirstValue = false;
                bool setStartToBeginFontSizeSecondValue = false;
                int StartToBeginFontSizeFirstValue= 0;
                int StartToBeginFontSizeSecondValue= 0;
                bool setStartToBeginHeightFirstValue = false;
                bool setStartToBeginHeightSecondValue = false;
                float StartToBeginHeightFirstValue= 0;
                float StartToBeginHeightSecondValue= 0;
                bool setStartToBeginYFirstValue = false;
                bool setStartToBeginYSecondValue = false;
                float StartToBeginYFirstValue= 0;
                float StartToBeginYSecondValue= 0;
                bool setWidthFirstValue = false;
                bool setWidthSecondValue = false;
                float WidthFirstValue= 0;
                float WidthSecondValue= 0;
                switch(firstState)
                {
                    case  VariableState.Default:
                        setBackgroundBlueFirstValue = true;
                        BackgroundBlueFirstValue = 0;
                        setBackgroundGreenFirstValue = true;
                        BackgroundGreenFirstValue = 0;
                        setBackgroundHeightFirstValue = true;
                        BackgroundHeightFirstValue = 100f;
                        if (interpolationValue < 1)
                        {
                            this.Background.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setBackgroundRedFirstValue = true;
                        BackgroundRedFirstValue = 0;
                        setBackgroundWidthFirstValue = true;
                        BackgroundWidthFirstValue = 100f;
                        if (interpolationValue < 1)
                        {
                            this.Background.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setHeightFirstValue = true;
                        HeightFirstValue = 100f;
                        if (interpolationValue < 1)
                        {
                            this.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setInstructionsFontSizeFirstValue = true;
                        InstructionsFontSizeFirstValue = 42;
                        setInstructionsHeightFirstValue = true;
                        InstructionsHeightFirstValue = 10f;
                        if (interpolationValue < 1)
                        {
                            this.Instructions.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.Text = "Press Any Button To Join";
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                        }
                        setInstructionsYFirstValue = true;
                        InstructionsYFirstValue = 80f;
                        if (interpolationValue < 1)
                        {
                            this.Instructions.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                        }
                        if (interpolationValue < 1)
                        {
                            this.Instructions.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.PlayerList.ChildrenLayout = Gum.Managers.ChildrenLayout.LeftToRightStack;
                        }
                        setPlayerListHeightFirstValue = true;
                        PlayerListHeightFirstValue = 80f;
                        if (interpolationValue < 1)
                        {
                            this.PlayerList.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setPlayerListWidthFirstValue = true;
                        PlayerListWidthFirstValue = 100f;
                        if (interpolationValue < 1)
                        {
                            this.PlayerList.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.PlayerList.WrapsChildren = true;
                        }
                        setStartToBeginFontSizeFirstValue = true;
                        StartToBeginFontSizeFirstValue = 32;
                        setStartToBeginHeightFirstValue = true;
                        StartToBeginHeightFirstValue = 10f;
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.Text = "(Press Start to Begin)";
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                        }
                        setStartToBeginYFirstValue = true;
                        StartToBeginYFirstValue = 90f;
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                        }
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        setWidthFirstValue = true;
                        WidthFirstValue = 100f;
                        if (interpolationValue < 1)
                        {
                            this.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue < 1)
                        {
                            this.XUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        break;
                }
                switch(secondState)
                {
                    case  VariableState.Default:
                        setBackgroundBlueSecondValue = true;
                        BackgroundBlueSecondValue = 0;
                        setBackgroundGreenSecondValue = true;
                        BackgroundGreenSecondValue = 0;
                        setBackgroundHeightSecondValue = true;
                        BackgroundHeightSecondValue = 100f;
                        if (interpolationValue >= 1)
                        {
                            this.Background.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setBackgroundRedSecondValue = true;
                        BackgroundRedSecondValue = 0;
                        setBackgroundWidthSecondValue = true;
                        BackgroundWidthSecondValue = 100f;
                        if (interpolationValue >= 1)
                        {
                            this.Background.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setHeightSecondValue = true;
                        HeightSecondValue = 100f;
                        if (interpolationValue >= 1)
                        {
                            this.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setInstructionsFontSizeSecondValue = true;
                        InstructionsFontSizeSecondValue = 42;
                        setInstructionsHeightSecondValue = true;
                        InstructionsHeightSecondValue = 10f;
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.Text = "Press Any Button To Join";
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                        }
                        setInstructionsYSecondValue = true;
                        InstructionsYSecondValue = 80f;
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.Instructions.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.PlayerList.ChildrenLayout = Gum.Managers.ChildrenLayout.LeftToRightStack;
                        }
                        setPlayerListHeightSecondValue = true;
                        PlayerListHeightSecondValue = 80f;
                        if (interpolationValue >= 1)
                        {
                            this.PlayerList.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        setPlayerListWidthSecondValue = true;
                        PlayerListWidthSecondValue = 100f;
                        if (interpolationValue >= 1)
                        {
                            this.PlayerList.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.PlayerList.WrapsChildren = true;
                        }
                        setStartToBeginFontSizeSecondValue = true;
                        StartToBeginFontSizeSecondValue = 32;
                        setStartToBeginHeightSecondValue = true;
                        StartToBeginHeightSecondValue = 10f;
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.HorizontalAlignment = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.Text = "(Press Start to Begin)";
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.VerticalAlignment = RenderingLibrary.Graphics.VerticalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
                        }
                        setStartToBeginYSecondValue = true;
                        StartToBeginYSecondValue = 90f;
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Top;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.YUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        setWidthSecondValue = true;
                        WidthSecondValue = 100f;
                        if (interpolationValue >= 1)
                        {
                            this.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                        }
                        if (interpolationValue >= 1)
                        {
                            this.XUnits = Gum.Converters.GeneralUnitType.Percentage;
                        }
                        break;
                }
                if (setBackgroundBlueFirstValue && setBackgroundBlueSecondValue)
                {
                    Background.Blue = FlatRedBall.Math.MathFunctions.RoundToInt(BackgroundBlueFirstValue* (1 - interpolationValue) + BackgroundBlueSecondValue * interpolationValue);
                }
                if (setBackgroundGreenFirstValue && setBackgroundGreenSecondValue)
                {
                    Background.Green = FlatRedBall.Math.MathFunctions.RoundToInt(BackgroundGreenFirstValue* (1 - interpolationValue) + BackgroundGreenSecondValue * interpolationValue);
                }
                if (setBackgroundHeightFirstValue && setBackgroundHeightSecondValue)
                {
                    Background.Height = BackgroundHeightFirstValue * (1 - interpolationValue) + BackgroundHeightSecondValue * interpolationValue;
                }
                if (setBackgroundRedFirstValue && setBackgroundRedSecondValue)
                {
                    Background.Red = FlatRedBall.Math.MathFunctions.RoundToInt(BackgroundRedFirstValue* (1 - interpolationValue) + BackgroundRedSecondValue * interpolationValue);
                }
                if (setBackgroundWidthFirstValue && setBackgroundWidthSecondValue)
                {
                    Background.Width = BackgroundWidthFirstValue * (1 - interpolationValue) + BackgroundWidthSecondValue * interpolationValue;
                }
                if (setHeightFirstValue && setHeightSecondValue)
                {
                    Height = HeightFirstValue * (1 - interpolationValue) + HeightSecondValue * interpolationValue;
                }
                if (setInstructionsFontSizeFirstValue && setInstructionsFontSizeSecondValue)
                {
                    Instructions.FontSize = FlatRedBall.Math.MathFunctions.RoundToInt(InstructionsFontSizeFirstValue* (1 - interpolationValue) + InstructionsFontSizeSecondValue * interpolationValue);
                }
                if (setInstructionsHeightFirstValue && setInstructionsHeightSecondValue)
                {
                    Instructions.Height = InstructionsHeightFirstValue * (1 - interpolationValue) + InstructionsHeightSecondValue * interpolationValue;
                }
                if (setInstructionsYFirstValue && setInstructionsYSecondValue)
                {
                    Instructions.Y = InstructionsYFirstValue * (1 - interpolationValue) + InstructionsYSecondValue * interpolationValue;
                }
                if (setPlayerListHeightFirstValue && setPlayerListHeightSecondValue)
                {
                    PlayerList.Height = PlayerListHeightFirstValue * (1 - interpolationValue) + PlayerListHeightSecondValue * interpolationValue;
                }
                if (setPlayerListWidthFirstValue && setPlayerListWidthSecondValue)
                {
                    PlayerList.Width = PlayerListWidthFirstValue * (1 - interpolationValue) + PlayerListWidthSecondValue * interpolationValue;
                }
                if (setStartToBeginFontSizeFirstValue && setStartToBeginFontSizeSecondValue)
                {
                    StartToBegin.FontSize = FlatRedBall.Math.MathFunctions.RoundToInt(StartToBeginFontSizeFirstValue* (1 - interpolationValue) + StartToBeginFontSizeSecondValue * interpolationValue);
                }
                if (setStartToBeginHeightFirstValue && setStartToBeginHeightSecondValue)
                {
                    StartToBegin.Height = StartToBeginHeightFirstValue * (1 - interpolationValue) + StartToBeginHeightSecondValue * interpolationValue;
                }
                if (setStartToBeginYFirstValue && setStartToBeginYSecondValue)
                {
                    StartToBegin.Y = StartToBeginYFirstValue * (1 - interpolationValue) + StartToBeginYSecondValue * interpolationValue;
                }
                if (setWidthFirstValue && setWidthSecondValue)
                {
                    Width = WidthFirstValue * (1 - interpolationValue) + WidthSecondValue * interpolationValue;
                }
                if (interpolationValue < 1)
                {
                    mCurrentVariableState = firstState;
                }
                else
                {
                    mCurrentVariableState = secondState;
                }
            }
            public void InterpolateBetween (Visibility firstState, Visibility secondState, float interpolationValue) 
            {
                #if DEBUG
                if (float.IsNaN(interpolationValue))
                {
                    throw new System.Exception("interpolationValue cannot be NaN");
                }
                #endif
                bool setXFirstValue = false;
                bool setXSecondValue = false;
                float XFirstValue= 0;
                float XSecondValue= 0;
                switch(firstState)
                {
                    case  Visibility.Shown:
                        setXFirstValue = true;
                        XFirstValue = 0f;
                        break;
                    case  Visibility.Hidden:
                        setXFirstValue = true;
                        XFirstValue = -100f;
                        break;
                }
                switch(secondState)
                {
                    case  Visibility.Shown:
                        setXSecondValue = true;
                        XSecondValue = 0f;
                        break;
                    case  Visibility.Hidden:
                        setXSecondValue = true;
                        XSecondValue = -100f;
                        break;
                }
                if (setXFirstValue && setXSecondValue)
                {
                    X = XFirstValue * (1 - interpolationValue) + XSecondValue * interpolationValue;
                }
                if (interpolationValue < 1)
                {
                    mCurrentVisibilityState = firstState;
                }
                else
                {
                    mCurrentVisibilityState = secondState;
                }
            }
            public void InterpolateBetween (ReadyToStart firstState, ReadyToStart secondState, float interpolationValue) 
            {
                #if DEBUG
                if (float.IsNaN(interpolationValue))
                {
                    throw new System.Exception("interpolationValue cannot be NaN");
                }
                #endif
                switch(firstState)
                {
                    case  ReadyToStart.Yes:
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.Visible = true;
                        }
                        break;
                    case  ReadyToStart.No:
                        if (interpolationValue < 1)
                        {
                            this.StartToBegin.Visible = false;
                        }
                        break;
                }
                switch(secondState)
                {
                    case  ReadyToStart.Yes:
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.Visible = true;
                        }
                        break;
                    case  ReadyToStart.No:
                        if (interpolationValue >= 1)
                        {
                            this.StartToBegin.Visible = false;
                        }
                        break;
                }
                if (interpolationValue < 1)
                {
                    mCurrentReadyToStartState = firstState;
                }
                else
                {
                    mCurrentReadyToStartState = secondState;
                }
            }
            #endregion
            #region State Interpolate To
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.VariableState fromState,GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.VariableState toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null) 
            {
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from:0, to:1, duration:(float)secondsToTake, type:interpolationType, easing:easing );
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(fromState, toState, newPosition);
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (VariableState toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = this.ElementSave.States.First(item => item.Name == toState.ToString());
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentVariableState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateToRelative (VariableState toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = AddToCurrentValuesWithState(toState);
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentVariableState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.Visibility fromState,GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.Visibility toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null) 
            {
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from:0, to:1, duration:(float)secondsToTake, type:interpolationType, easing:easing );
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(fromState, toState, newPosition);
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (Visibility toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = this.ElementSave.Categories.First(item => item.Name == "Visibility").States.First(item => item.Name == toState.ToString());
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentVisibilityState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateToRelative (Visibility toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = AddToCurrentValuesWithState(toState);
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentVisibilityState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.ReadyToStart fromState,GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.ReadyToStart toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null) 
            {
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from:0, to:1, duration:(float)secondsToTake, type:interpolationType, easing:easing );
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(fromState, toState, newPosition);
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (ReadyToStart toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = this.ElementSave.Categories.First(item => item.Name == "ReadyToStart").States.First(item => item.Name == toState.ToString());
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentReadyToStartState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateToRelative (ReadyToStart toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null ) 
            {
                Gum.DataTypes.Variables.StateSave current = GetCurrentValuesOnState(toState);
                Gum.DataTypes.Variables.StateSave toAsStateSave = AddToCurrentValuesWithState(toState);
                FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: (float)secondsToTake, type: interpolationType, easing: easing);
                if (owner == null)
                {
                    tweener.Owner = this;
                }
                else
                {
                    tweener.Owner = owner;
                }
                tweener.PositionChanged = newPosition => this.InterpolateBetween(current, toAsStateSave, newPosition);
                tweener.Ended += ()=> this.CurrentReadyToStartState = toState;
                tweener.Start();
                StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                return tweener;
            }
            #endregion
            #region State Animations
            private System.Collections.Generic.IEnumerable<FlatRedBall.Instructions.Instruction> ShowAnimationInstructions (object target) 
            {
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction( ()=> this.CurrentVisibilityState = Visibility.Hidden);
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime;
                    toReturn.Target = target;
                    yield return toReturn;
                }
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction(  () => this.InterpolateTo(Visibility.Shown, 1, FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear, FlatRedBall.Glue.StateInterpolation.Easing.Out, ShowAnimation));
                    toReturn.Target = target;
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + 0;
                    yield return toReturn;
                }
            }
            private System.Collections.Generic.IEnumerable<FlatRedBall.Instructions.Instruction> ShowAnimationRelativeInstructions (object target) 
            {
                {
                }
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction(() =>
                    {
                        var relativeStart = ElementSave.AllStates.FirstOrDefault(item => item.Name == "Visibility/Hidden").Clone();
                        var relativeEnd = ElementSave.AllStates.FirstOrDefault(item => item.Name == "Visibility/Shown").Clone();
                        Gum.DataTypes.Variables.StateSaveExtensionMethods.SubtractFromThis(relativeEnd, relativeStart);
                        var difference = relativeEnd;
                        Gum.DataTypes.Variables.StateSave first = GetCurrentValuesOnState(Visibility.Shown);
                        Gum.DataTypes.Variables.StateSave second = first.Clone();
                        Gum.DataTypes.Variables.StateSaveExtensionMethods.AddIntoThis(second, difference);
                        FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: 1, type: FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear, easing: FlatRedBall.Glue.StateInterpolation.Easing.Out);
                        tweener.Owner = this;
                        tweener.PositionChanged = newPosition => this.InterpolateBetween(first, second, newPosition);
                        tweener.Start();
                        StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                    }
                    );
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + 0;
                    toReturn.Target = target;
                    yield return toReturn;
                }
            }
            private FlatRedBall.Gum.Animation.GumAnimation showAnimation;
            public FlatRedBall.Gum.Animation.GumAnimation ShowAnimation
            {
                get
                {
                    if (showAnimation == null)
                    {
                        showAnimation = new FlatRedBall.Gum.Animation.GumAnimation(1, ShowAnimationInstructions);
                    }
                    return showAnimation;
                }
            }
            private FlatRedBall.Gum.Animation.GumAnimation showAnimationRelative;
            public FlatRedBall.Gum.Animation.GumAnimation ShowAnimationRelative
            {
                get
                {
                    if (showAnimationRelative == null)
                    {
                        showAnimationRelative = new FlatRedBall.Gum.Animation.GumAnimation(1, ShowAnimationRelativeInstructions);
                    }
                    return showAnimationRelative;
                }
            }
            private System.Collections.Generic.IEnumerable<FlatRedBall.Instructions.Instruction> HideAnimationInstructions (object target) 
            {
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction( ()=> this.CurrentVisibilityState = Visibility.Shown);
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime;
                    toReturn.Target = target;
                    yield return toReturn;
                }
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction(  () => this.InterpolateTo(Visibility.Hidden, 1, FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear, FlatRedBall.Glue.StateInterpolation.Easing.Out, HideAnimation));
                    toReturn.Target = target;
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + 0;
                    yield return toReturn;
                }
            }
            private System.Collections.Generic.IEnumerable<FlatRedBall.Instructions.Instruction> HideAnimationRelativeInstructions (object target) 
            {
                {
                }
                {
                    var toReturn = new FlatRedBall.Instructions.DelegateInstruction(() =>
                    {
                        var relativeStart = ElementSave.AllStates.FirstOrDefault(item => item.Name == "Visibility/Shown").Clone();
                        var relativeEnd = ElementSave.AllStates.FirstOrDefault(item => item.Name == "Visibility/Hidden").Clone();
                        Gum.DataTypes.Variables.StateSaveExtensionMethods.SubtractFromThis(relativeEnd, relativeStart);
                        var difference = relativeEnd;
                        Gum.DataTypes.Variables.StateSave first = GetCurrentValuesOnState(Visibility.Hidden);
                        Gum.DataTypes.Variables.StateSave second = first.Clone();
                        Gum.DataTypes.Variables.StateSaveExtensionMethods.AddIntoThis(second, difference);
                        FlatRedBall.Glue.StateInterpolation.Tweener tweener = new FlatRedBall.Glue.StateInterpolation.Tweener(from: 0, to: 1, duration: 1, type: FlatRedBall.Glue.StateInterpolation.InterpolationType.Linear, easing: FlatRedBall.Glue.StateInterpolation.Easing.Out);
                        tweener.Owner = this;
                        tweener.PositionChanged = newPosition => this.InterpolateBetween(first, second, newPosition);
                        tweener.Start();
                        StateInterpolationPlugin.TweenerManager.Self.Add(tweener);
                    }
                    );
                    toReturn.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + 0;
                    toReturn.Target = target;
                    yield return toReturn;
                }
            }
            private FlatRedBall.Gum.Animation.GumAnimation hideAnimation;
            public FlatRedBall.Gum.Animation.GumAnimation HideAnimation
            {
                get
                {
                    if (hideAnimation == null)
                    {
                        hideAnimation = new FlatRedBall.Gum.Animation.GumAnimation(1, HideAnimationInstructions);
                    }
                    return hideAnimation;
                }
            }
            private FlatRedBall.Gum.Animation.GumAnimation hideAnimationRelative;
            public FlatRedBall.Gum.Animation.GumAnimation HideAnimationRelative
            {
                get
                {
                    if (hideAnimationRelative == null)
                    {
                        hideAnimationRelative = new FlatRedBall.Gum.Animation.GumAnimation(1, HideAnimationRelativeInstructions);
                    }
                    return hideAnimationRelative;
                }
            }
            #endregion
            public override void StopAnimations () 
            {
                base.StopAnimations();
                ShowAnimation.Stop();
                HideAnimation.Stop();
            }
            #region Get Current Values on State
            private Gum.DataTypes.Variables.StateSave GetCurrentValuesOnState (VariableState state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  VariableState.Default:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Height",
                            Type = "float",
                            Value = Height
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Height Units",
                            Type = "DimensionUnitType",
                            Value = HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Width",
                            Type = "float",
                            Value = Width
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Width Units",
                            Type = "DimensionUnitType",
                            Value = WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X Units",
                            Type = "PositionUnitType",
                            Value = XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Blue",
                            Type = "int",
                            Value = Background.Blue
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Green",
                            Type = "int",
                            Value = Background.Green
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Height",
                            Type = "float",
                            Value = Background.Height
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Height Units",
                            Type = "DimensionUnitType",
                            Value = Background.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Red",
                            Type = "int",
                            Value = Background.Red
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Width",
                            Type = "float",
                            Value = Background.Width
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Width Units",
                            Type = "DimensionUnitType",
                            Value = Background.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.FontSize",
                            Type = "int",
                            Value = Instructions.FontSize
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Height",
                            Type = "float",
                            Value = Instructions.Height
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Height Units",
                            Type = "DimensionUnitType",
                            Value = Instructions.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.HorizontalAlignment",
                            Type = "HorizontalAlignment",
                            Value = Instructions.HorizontalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Text",
                            Type = "string",
                            Value = Instructions.Text
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.VerticalAlignment",
                            Type = "VerticalAlignment",
                            Value = Instructions.VerticalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Width Units",
                            Type = "DimensionUnitType",
                            Value = Instructions.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.X Origin",
                            Type = "HorizontalAlignment",
                            Value = Instructions.XOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.X Units",
                            Type = "PositionUnitType",
                            Value = Instructions.XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y",
                            Type = "float",
                            Value = Instructions.Y
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y Origin",
                            Type = "VerticalAlignment",
                            Value = Instructions.YOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y Units",
                            Type = "PositionUnitType",
                            Value = Instructions.YUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Children Layout",
                            Type = "ChildrenLayout",
                            Value = PlayerList.ChildrenLayout
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Height",
                            Type = "float",
                            Value = PlayerList.Height
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Height Units",
                            Type = "DimensionUnitType",
                            Value = PlayerList.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Width",
                            Type = "float",
                            Value = PlayerList.Width
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Width Units",
                            Type = "DimensionUnitType",
                            Value = PlayerList.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Wraps Children",
                            Type = "bool",
                            Value = PlayerList.WrapsChildren
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.FontSize",
                            Type = "int",
                            Value = StartToBegin.FontSize
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Height",
                            Type = "float",
                            Value = StartToBegin.Height
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Height Units",
                            Type = "DimensionUnitType",
                            Value = StartToBegin.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.HorizontalAlignment",
                            Type = "HorizontalAlignment",
                            Value = StartToBegin.HorizontalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Text",
                            Type = "string",
                            Value = StartToBegin.Text
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.VerticalAlignment",
                            Type = "VerticalAlignment",
                            Value = StartToBegin.VerticalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Width Units",
                            Type = "DimensionUnitType",
                            Value = StartToBegin.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.X Origin",
                            Type = "HorizontalAlignment",
                            Value = StartToBegin.XOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.X Units",
                            Type = "PositionUnitType",
                            Value = StartToBegin.XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y",
                            Type = "float",
                            Value = StartToBegin.Y
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y Origin",
                            Type = "VerticalAlignment",
                            Value = StartToBegin.YOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y Units",
                            Type = "PositionUnitType",
                            Value = StartToBegin.YUnits
                        }
                        );
                        break;
                }
                return newState;
            }
            private Gum.DataTypes.Variables.StateSave AddToCurrentValuesWithState (VariableState state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  VariableState.Default:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Height",
                            Type = "float",
                            Value = Height + 100f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Height Units",
                            Type = "DimensionUnitType",
                            Value = HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Width",
                            Type = "float",
                            Value = Width + 100f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Width Units",
                            Type = "DimensionUnitType",
                            Value = WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X Units",
                            Type = "PositionUnitType",
                            Value = XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Blue",
                            Type = "int",
                            Value = Background.Blue + 0
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Green",
                            Type = "int",
                            Value = Background.Green + 0
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Height",
                            Type = "float",
                            Value = Background.Height + 100f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Height Units",
                            Type = "DimensionUnitType",
                            Value = Background.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Red",
                            Type = "int",
                            Value = Background.Red + 0
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Width",
                            Type = "float",
                            Value = Background.Width + 100f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Background.Width Units",
                            Type = "DimensionUnitType",
                            Value = Background.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.FontSize",
                            Type = "int",
                            Value = Instructions.FontSize + 42
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Height",
                            Type = "float",
                            Value = Instructions.Height + 10f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Height Units",
                            Type = "DimensionUnitType",
                            Value = Instructions.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.HorizontalAlignment",
                            Type = "HorizontalAlignment",
                            Value = Instructions.HorizontalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Text",
                            Type = "string",
                            Value = Instructions.Text
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.VerticalAlignment",
                            Type = "VerticalAlignment",
                            Value = Instructions.VerticalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Width Units",
                            Type = "DimensionUnitType",
                            Value = Instructions.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.X Origin",
                            Type = "HorizontalAlignment",
                            Value = Instructions.XOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.X Units",
                            Type = "PositionUnitType",
                            Value = Instructions.XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y",
                            Type = "float",
                            Value = Instructions.Y + 80f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y Origin",
                            Type = "VerticalAlignment",
                            Value = Instructions.YOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "Instructions.Y Units",
                            Type = "PositionUnitType",
                            Value = Instructions.YUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Children Layout",
                            Type = "ChildrenLayout",
                            Value = PlayerList.ChildrenLayout
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Height",
                            Type = "float",
                            Value = PlayerList.Height + 80f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Height Units",
                            Type = "DimensionUnitType",
                            Value = PlayerList.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Width",
                            Type = "float",
                            Value = PlayerList.Width + 100f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Width Units",
                            Type = "DimensionUnitType",
                            Value = PlayerList.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerList.Wraps Children",
                            Type = "bool",
                            Value = PlayerList.WrapsChildren
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.FontSize",
                            Type = "int",
                            Value = StartToBegin.FontSize + 32
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Height",
                            Type = "float",
                            Value = StartToBegin.Height + 10f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Height Units",
                            Type = "DimensionUnitType",
                            Value = StartToBegin.HeightUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.HorizontalAlignment",
                            Type = "HorizontalAlignment",
                            Value = StartToBegin.HorizontalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Text",
                            Type = "string",
                            Value = StartToBegin.Text
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.VerticalAlignment",
                            Type = "VerticalAlignment",
                            Value = StartToBegin.VerticalAlignment
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Width Units",
                            Type = "DimensionUnitType",
                            Value = StartToBegin.WidthUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.X Origin",
                            Type = "HorizontalAlignment",
                            Value = StartToBegin.XOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.X Units",
                            Type = "PositionUnitType",
                            Value = StartToBegin.XUnits
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y",
                            Type = "float",
                            Value = StartToBegin.Y + 90f
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y Origin",
                            Type = "VerticalAlignment",
                            Value = StartToBegin.YOrigin
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Y Units",
                            Type = "PositionUnitType",
                            Value = StartToBegin.YUnits
                        }
                        );
                        break;
                }
                return newState;
            }
            private Gum.DataTypes.Variables.StateSave GetCurrentValuesOnState (Visibility state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  Visibility.Shown:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X",
                            Type = "float",
                            Value = X
                        }
                        );
                        break;
                    case  Visibility.Hidden:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X",
                            Type = "float",
                            Value = X
                        }
                        );
                        break;
                }
                return newState;
            }
            private Gum.DataTypes.Variables.StateSave AddToCurrentValuesWithState (Visibility state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  Visibility.Shown:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X",
                            Type = "float",
                            Value = X + 0f
                        }
                        );
                        break;
                    case  Visibility.Hidden:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "X",
                            Type = "float",
                            Value = X + -100f
                        }
                        );
                        break;
                }
                return newState;
            }
            private Gum.DataTypes.Variables.StateSave GetCurrentValuesOnState (ReadyToStart state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  ReadyToStart.Yes:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Visible",
                            Type = "bool",
                            Value = StartToBegin.Visible
                        }
                        );
                        break;
                    case  ReadyToStart.No:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Visible",
                            Type = "bool",
                            Value = StartToBegin.Visible
                        }
                        );
                        break;
                }
                return newState;
            }
            private Gum.DataTypes.Variables.StateSave AddToCurrentValuesWithState (ReadyToStart state) 
            {
                Gum.DataTypes.Variables.StateSave newState = new Gum.DataTypes.Variables.StateSave();
                switch(state)
                {
                    case  ReadyToStart.Yes:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Visible",
                            Type = "bool",
                            Value = StartToBegin.Visible
                        }
                        );
                        break;
                    case  ReadyToStart.No:
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "StartToBegin.Visible",
                            Type = "bool",
                            Value = StartToBegin.Visible
                        }
                        );
                        break;
                }
                return newState;
            }
            #endregion
            public override void ApplyState (Gum.DataTypes.Variables.StateSave state) 
            {
                bool matches = this.ElementSave.AllStates.Contains(state);
                if (matches)
                {
                    var category = this.ElementSave.Categories.FirstOrDefault(item => item.States.Contains(state));
                    if (category == null)
                    {
                        if (state.Name == "Default") this.mCurrentVariableState = VariableState.Default;
                    }
                    else if (category.Name == "Visibility")
                    {
                        if(state.Name == "Shown") this.mCurrentVisibilityState = Visibility.Shown;
                        if(state.Name == "Hidden") this.mCurrentVisibilityState = Visibility.Hidden;
                    }
                    else if (category.Name == "ReadyToStart")
                    {
                        if(state.Name == "Yes") this.mCurrentReadyToStartState = ReadyToStart.Yes;
                        if(state.Name == "No") this.mCurrentReadyToStartState = ReadyToStart.No;
                    }
                }
                base.ApplyState(state);
            }
            private GreasyPlatypusSlapper.GumRuntimes.ColoredRectangleRuntime Background { get; set; }
            private GreasyPlatypusSlapper.GumRuntimes.TextRuntime Instructions { get; set; }
            private GreasyPlatypusSlapper.GumRuntimes.ContainerRuntime PlayerList { get; set; }
            private GreasyPlatypusSlapper.GumRuntimes.TextRuntime StartToBegin { get; set; }
            public PlayerSelectionUIRuntime (bool fullInstantiation = true, bool tryCreateFormsObject = true) 
            	: base(false, tryCreateFormsObject)
            {
                if (fullInstantiation)
                {
                    Gum.DataTypes.ElementSave elementSave = Gum.Managers.ObjectFinder.Self.GumProjectSave.Components.First(item => item.Name == "PlayerSelectionUI");
                    this.ElementSave = elementSave;
                    string oldDirectory = FlatRedBall.IO.FileManager.RelativeDirectory;
                    FlatRedBall.IO.FileManager.RelativeDirectory = FlatRedBall.IO.FileManager.GetDirectory(Gum.Managers.ObjectFinder.Self.GumProjectSave.FullFileName);
                    GumRuntime.ElementSaveExtensions.SetGraphicalUiElement(elementSave, this, RenderingLibrary.SystemManagers.Default);
                    FlatRedBall.IO.FileManager.RelativeDirectory = oldDirectory;
                }
            }
            public override void SetInitialState () 
            {
                base.SetInitialState();
                this.CurrentVariableState = VariableState.Default;
                CallCustomInitialize();
            }
            public override void CreateChildrenRecursively (Gum.DataTypes.ElementSave elementSave, RenderingLibrary.SystemManagers systemManagers) 
            {
                base.CreateChildrenRecursively(elementSave, systemManagers);
                this.AssignReferences();
            }
            private void AssignReferences () 
            {
                Background = this.GetGraphicalUiElementByName("Background") as GreasyPlatypusSlapper.GumRuntimes.ColoredRectangleRuntime;
                Instructions = this.GetGraphicalUiElementByName("Instructions") as GreasyPlatypusSlapper.GumRuntimes.TextRuntime;
                PlayerList = this.GetGraphicalUiElementByName("PlayerList") as GreasyPlatypusSlapper.GumRuntimes.ContainerRuntime;
                StartToBegin = this.GetGraphicalUiElementByName("StartToBegin") as GreasyPlatypusSlapper.GumRuntimes.TextRuntime;
            }
            public override void AddToManagers (RenderingLibrary.SystemManagers managers, RenderingLibrary.Graphics.Layer layer) 
            {
                base.AddToManagers(managers, layer);
            }
            private void CallCustomInitialize () 
            {
                CustomInitialize();
            }
            partial void CustomInitialize();
        }
    }
