    using System.Linq;
    namespace GreasyPlatypusSlapper.GumRuntimes
    {
        public partial class GameScreenGumRuntime : Gum.Wireframe.GraphicalUiElement
        {
            #region State Enums
            public enum VariableState
            {
                Default,
                PlayerSelection,
                Game
            }
            #endregion
            #region State Fields
            VariableState mCurrentVariableState;
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
                            PlayerSelectionUIInstance.CurrentReadyToStartState = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.ReadyToStart.No;
                            PlayerSelectionUIInstance.CurrentVariableState = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.VariableState.Default;
                            PlayerSelectionUIInstance.CurrentVisibilityState = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.Visibility.Shown;
                            break;
                        case  VariableState.PlayerSelection:
                            break;
                        case  VariableState.Game:
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
                bool setPlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue = false;
                bool setPlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue = false;
                PlayerSelectionUIRuntime.ReadyToStart PlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue= PlayerSelectionUIRuntime.ReadyToStart.Yes;
                PlayerSelectionUIRuntime.ReadyToStart PlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue= PlayerSelectionUIRuntime.ReadyToStart.Yes;
                bool setPlayerSelectionUIInstanceCurrentVariableStateFirstValue = false;
                bool setPlayerSelectionUIInstanceCurrentVariableStateSecondValue = false;
                PlayerSelectionUIRuntime.VariableState PlayerSelectionUIInstanceCurrentVariableStateFirstValue= PlayerSelectionUIRuntime.VariableState.Default;
                PlayerSelectionUIRuntime.VariableState PlayerSelectionUIInstanceCurrentVariableStateSecondValue= PlayerSelectionUIRuntime.VariableState.Default;
                bool setPlayerSelectionUIInstanceCurrentVisibilityStateFirstValue = false;
                bool setPlayerSelectionUIInstanceCurrentVisibilityStateSecondValue = false;
                PlayerSelectionUIRuntime.Visibility PlayerSelectionUIInstanceCurrentVisibilityStateFirstValue= PlayerSelectionUIRuntime.Visibility.Shown;
                PlayerSelectionUIRuntime.Visibility PlayerSelectionUIInstanceCurrentVisibilityStateSecondValue= PlayerSelectionUIRuntime.Visibility.Shown;
                switch(firstState)
                {
                    case  VariableState.Default:
                        setPlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue = true;
                        PlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.ReadyToStart.No;
                        setPlayerSelectionUIInstanceCurrentVariableStateFirstValue = true;
                        PlayerSelectionUIInstanceCurrentVariableStateFirstValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.VariableState.Default;
                        setPlayerSelectionUIInstanceCurrentVisibilityStateFirstValue = true;
                        PlayerSelectionUIInstanceCurrentVisibilityStateFirstValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.Visibility.Shown;
                        break;
                    case  VariableState.PlayerSelection:
                        break;
                    case  VariableState.Game:
                        break;
                }
                switch(secondState)
                {
                    case  VariableState.Default:
                        setPlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue = true;
                        PlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.ReadyToStart.No;
                        setPlayerSelectionUIInstanceCurrentVariableStateSecondValue = true;
                        PlayerSelectionUIInstanceCurrentVariableStateSecondValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.VariableState.Default;
                        setPlayerSelectionUIInstanceCurrentVisibilityStateSecondValue = true;
                        PlayerSelectionUIInstanceCurrentVisibilityStateSecondValue = GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime.Visibility.Shown;
                        break;
                    case  VariableState.PlayerSelection:
                        break;
                    case  VariableState.Game:
                        break;
                }
                if (setPlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue && setPlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue)
                {
                    PlayerSelectionUIInstance.InterpolateBetween(PlayerSelectionUIInstanceCurrentReadyToStartStateFirstValue, PlayerSelectionUIInstanceCurrentReadyToStartStateSecondValue, interpolationValue);
                }
                if (setPlayerSelectionUIInstanceCurrentVariableStateFirstValue && setPlayerSelectionUIInstanceCurrentVariableStateSecondValue)
                {
                    PlayerSelectionUIInstance.InterpolateBetween(PlayerSelectionUIInstanceCurrentVariableStateFirstValue, PlayerSelectionUIInstanceCurrentVariableStateSecondValue, interpolationValue);
                }
                if (setPlayerSelectionUIInstanceCurrentVisibilityStateFirstValue && setPlayerSelectionUIInstanceCurrentVisibilityStateSecondValue)
                {
                    PlayerSelectionUIInstance.InterpolateBetween(PlayerSelectionUIInstanceCurrentVisibilityStateFirstValue, PlayerSelectionUIInstanceCurrentVisibilityStateSecondValue, interpolationValue);
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
            #endregion
            #region State Interpolate To
            public FlatRedBall.Glue.StateInterpolation.Tweener InterpolateTo (GreasyPlatypusSlapper.GumRuntimes.GameScreenGumRuntime.VariableState fromState,GreasyPlatypusSlapper.GumRuntimes.GameScreenGumRuntime.VariableState toState, double secondsToTake, FlatRedBall.Glue.StateInterpolation.InterpolationType interpolationType, FlatRedBall.Glue.StateInterpolation.Easing easing, object owner = null) 
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
            #endregion
            #region State Animations
            #endregion
            public override void StopAnimations () 
            {
                base.StopAnimations();
                PlayerSelectionUIInstance.StopAnimations();
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
                            Name = "PlayerSelectionUIInstance.ReadyToStartState",
                            Type = "ReadyToStartState",
                            Value = PlayerSelectionUIInstance.CurrentReadyToStartState
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerSelectionUIInstance.State",
                            Type = "State",
                            Value = PlayerSelectionUIInstance.CurrentVariableState
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerSelectionUIInstance.VisibilityState",
                            Type = "VisibilityState",
                            Value = PlayerSelectionUIInstance.CurrentVisibilityState
                        }
                        );
                        break;
                    case  VariableState.PlayerSelection:
                        break;
                    case  VariableState.Game:
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
                            Name = "PlayerSelectionUIInstance.ReadyToStartState",
                            Type = "ReadyToStartState",
                            Value = PlayerSelectionUIInstance.CurrentReadyToStartState
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerSelectionUIInstance.State",
                            Type = "State",
                            Value = PlayerSelectionUIInstance.CurrentVariableState
                        }
                        );
                        newState.Variables.Add(new Gum.DataTypes.Variables.VariableSave()
                        {
                            SetsValue = true,
                            Name = "PlayerSelectionUIInstance.VisibilityState",
                            Type = "VisibilityState",
                            Value = PlayerSelectionUIInstance.CurrentVisibilityState
                        }
                        );
                        break;
                    case  VariableState.PlayerSelection:
                        break;
                    case  VariableState.Game:
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
                        if (state.Name == "PlayerSelection") this.mCurrentVariableState = VariableState.PlayerSelection;
                        if (state.Name == "Game") this.mCurrentVariableState = VariableState.Game;
                    }
                }
                base.ApplyState(state);
            }
            public GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime PlayerSelectionUIInstance { get; set; }
            public GameScreenGumRuntime (bool fullInstantiation = true, bool tryCreateFormsObject = true) 
            {
                if (fullInstantiation)
                {
                    Gum.DataTypes.ElementSave elementSave = Gum.Managers.ObjectFinder.Self.GumProjectSave.Screens.First(item => item.Name == "GameScreenGum");
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
                PlayerSelectionUIInstance = this.GetGraphicalUiElementByName("PlayerSelectionUIInstance") as GreasyPlatypusSlapper.GumRuntimes.PlayerSelectionUIRuntime;
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
