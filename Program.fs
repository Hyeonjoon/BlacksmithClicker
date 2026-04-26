open LegendSword100.State
open LegendSword100.UI

[<EntryPoint>]
let main argv =
  let initialState = StateInitializer.initialGameState
  displayState initialState
  0
