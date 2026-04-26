module LegendSword100.Logic

open LegendSword100.State
open System

let private lackOfGold (state: GameState): Result = 
  {
    Message = "골드가 부족합니다."
    State = state
  }

let private succeed (state: GameState): Result =
  {
    Message = "강화에 성공하였습니다."
    State = StateGenerator.upgradeSucceedState state
  }

let private failed (state: GameState): Result =
  {
    Message = "검이 파괴되었습니다."
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
    Message = "판매를 완료했습니다."
    State = StateGenerator.salesCompletedState state
  }