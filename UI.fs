module LegendSword100.UI

open LegendSword100.State

let displayState (state: GameState) =
  printfn "-----------------------------"
  printfn "Current Sword: %s (Lv.%d)" state.CurrentSword.Name state.CurrentSword.Level
  printfn "Balance: %d Gold" state.Gold
  printfn "Upgrade Cost: %d Gold | Selling Price: %d Gold | Upgrade Probability: %d%%" state.CurrentSword.UpgradeCost state.CurrentSword.SellingPrice state.CurrentSword.UpgradeProbability
  printfn "1. Upgrade Sword | 2. Sell Sword | 3. Initialize Game | 4. Quit Game"
  printf ">> "

let getCommand () =
  System.Console.ReadLine()
