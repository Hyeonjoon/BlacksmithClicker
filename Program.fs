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
      "축하합니다. 100강 검을 획득했습니다." |> printfn "%s" 
      "게임을 종료합니다." |> printfn "%s" 
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
      "상태를 초기화합니다." |> printfn "%s"
      gameLoop StateGenerator.initialGameState
    | "4", _ -> 
      "=============================" |> printfn "%s" 
      "게임을 종료합니다." |> printfn "%s" 
      0
    | _, _ -> 
      "지원하지 않는 옵션입니다." |> printfn "%s" 
      gameLoop state
  gameLoop StateGenerator.initialGameState
