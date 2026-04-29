module BlacksmithClicker.Logic

open BlacksmithClicker.State
open System

let private lackOfGold (state: GameState): Result = 
  {
    Message = "You don't have enough Gold."
    Color = Some ConsoleColor.Red
    State = state
  }

let private succeed (state: GameState): Result =
  {
    Message = "Upgrade succeeds."
    Color = Some ConsoleColor.Green
    State = StateGenerator.upgradeSucceedState state
  }

let private failed (state: GameState): Result =
  {
    Message = "The sword has been destroyed."
    Color = Some ConsoleColor.Red
    State = StateGenerator.upgradeFailedState state
  }

let private executeUpgrade (sword: Sword) (random: Random): bool =
  let probability = sword.UpgradeProbability
  uint (random.NextDouble() * 100.0) <= probability

let upgrade (state: GameState) (random: Random): Result =
  if state.Gold < state.CurrentSword.UpgradeCost then lackOfGold state
  elif executeUpgrade state.CurrentSword random then succeed state
  else failed state

let sell (state: GameState): Result =
  {
    Message = "The sword has been sold."
    Color = Some ConsoleColor.Green
    State = StateGenerator.salesCompletedState state
  }

let purchasePermanently (state: GameState): Result = 
  if state.Gold < state.CurrentSword.PermanentPurchaseCost then lackOfGold state
  else {
    Message = "Permanently purchased current sword."
    Color = Some ConsoleColor.Green
    State = StateGenerator.permanetlyPurchasedState state
  }