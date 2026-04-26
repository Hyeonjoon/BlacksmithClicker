open LegendSword100.State
open LegendSword100.UI
open LegendSword100.Logic
open System

[<EntryPoint>]
let main argv =
  let random = System.Random ()
  let rec gameLoop (state: GameState) = 
    displayState state
    let cmd = getCommand ()
    let result = upgrade state random
    result.Message |> printfn "%s" 
    gameLoop result.State
  gameLoop StateGenerator.initialGameState
