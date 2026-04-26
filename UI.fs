module LegendSword100.UI

open LegendSword100.State

let displayState (state: GameState) =
  printfn "-----------------------------"
  printfn "현재 검: %s (+%d)" state.CurrentSword.Name state.CurrentSword.Level
  printfn "보유 골드: %d" state.Gold
  printfn "강화 비용: %d | 판매 가격: %d" state.CurrentSword.UpgradeCost state.CurrentSword.SellingPrice
  printfn "1. 강화 | 2. 판매 | 3. 종료"
  printfn ">>"

let getCommand () =
  System.Console.ReadLine()
