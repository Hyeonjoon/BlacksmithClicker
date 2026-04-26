module LegendSword100.Logic

open LegendSword100.State
open System

let private lackOfGold (state: GameState): UpgradeResult = 
  {
    UpgradeResult.Succeed = false
    Message = "골드가 부족합니다."
    State = state
  }

let private succeed (state: GameState): UpgradeResult =
  {
    UpgradeResult.Succeed = true
    Message = "강화에 성공하였습니다."
    State = StateGenerator.upgradeSucceedState state
  }

let private failed (state: GameState): UpgradeResult =
  {
    UpgradeResult.Succeed = false
    Message = "검이 파괴되었습니다."
    State = StateGenerator.upgradeFailedState state
  }

let private executeUpgrade (sword: Sword) (random: Random): bool =
  let probability = sword.UpgradeProbability
  uint (random.NextDouble() * 100.0) <= probability

let upgrade (state: GameState) (random: Random): UpgradeResult =
    if state.Gold < state.CurrentSword.UpgradeCost then lackOfGold state
    elif executeUpgrade state.CurrentSword random then succeed state
    else failed state
