open LegendSword100.State
open LegendSword100.UI
open LegendSword100.Logic
open System

[<EntryPoint>]
let main argv =
  let random = System.Random ()
  let rec gameLoop (state: GameState) = 
    displayState state
    match getCommand () with
    | "1" -> 
      "=============================" |> printfn "%s" 
      let result = upgrade state random
      result.Message |> printfn "%s" 
      gameLoop result.State
    | "2" ->
      "=============================" |> printfn "%s" 
      let result = sell state
      result.Message |> printfn "%s" 
      gameLoop result.State
    | "4" -> 
      "=============================" |> printfn "%s" 
      "게임을 종료합니다." |> printfn "%s" 
      0
    | _ -> 
      "Not Implemented" |> printfn "%s" 
      gameLoop state
  gameLoop StateGenerator.initialGameState
