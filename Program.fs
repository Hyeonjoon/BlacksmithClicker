open LegendSword100.State

[<EntryPoint>]
let main argv =
  let swords = DataLoader.swords 
  Map.find 1u swords |> printfn "%A"
  Map.find 20u swords |> printfn "%A"
  Map.find 50u swords |> printfn "%A"
  Map.find 100u swords |> printfn "%A"
  0
