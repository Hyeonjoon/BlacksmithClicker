module LegendSword100.Logic

open LegendSword100.State
open System

let private lackOfGold (state: GameState): Result = 
  {
    Message = "You don't have enough Gold for upgrade."
    State = state
  }

let private succeed (state: GameState): Result =
  {
    Message = "Upgrade succeeds."
    State = StateGenerator.upgradeSucceedState state
  }

let private failed (state: GameState): Result =
  {
    Message = "The sword has been destroyed."
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
    State = StateGenerator.salesCompletedState state
  }