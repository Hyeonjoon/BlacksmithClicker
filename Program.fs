open LegendSword100.State
open LegendSword100.UI
open LegendSword100.Logic
open System

[<EntryPoint>]
let main argv =
  let random = System.Random ()
  let rec gameLoop (state: GameState) = 
    displayState state
    match getCommand (), state with
    | _, s when s.CurrentSword.Level >= Constants.maxSwordLevel ->
      "=============================" |> printfn "%s" 
      "Congratulations. You get the Lv.100 legend sword." |> printfn "%s" 
      "It's over!" |> printfn "%s" 
      0
    | "1", _ -> 
      "=============================" |> printfn "%s" 
      let result = upgrade state random
      result.Message |> printfn "%s" 
      gameLoop result.State
    | "2", _ ->
      "=============================" |> printfn "%s" 
      let result = sell state
      result.Message |> printfn "%s" 
      gameLoop result.State
    | "3", _ ->
      "=============================" |> printfn "%s"
      "The game has been initialized." |> printfn "%s"
      gameLoop StateGenerator.initialGameState
    | "4", _ -> 
      "=============================" |> printfn "%s" 
      "The game is being terminated." |> printfn "%s" 
      0
    | _, _ -> 
      "This is an unsupported option." |> printfn "%s" 
      gameLoop state
  gameLoop StateGenerator.initialGameState
