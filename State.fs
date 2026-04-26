namespace LegendSword100.State

open System.IO

type Sword = {
  Level: uint
  Name: string
  UpgradeCost: uint64
  SellingPrice: uint64
  UpgradeProbability: uint
}

type GameState = {
  mutable CurrentSword: Sword
  mutable Gold: uint64
}

module Constants =
  let minSwordLevel, maxSwordLevel = 1u, 100u
  let initialGold = 100000UL

module StateInitializer = 
  let csvPath = "SwordList.csv"
  
  let private parseLine (line: string) =
    let row = line.Split(',')
    {
      Level = uint row.[0]
      Name = row.[1]
      UpgradeCost = uint64 row.[2]
      SellingPrice = uint64 row.[3]
      UpgradeProbability = uint row.[4]
    }

  let private loadSwordData () =
    if File.Exists(csvPath) then
      File.ReadAllLines(csvPath)
      |> Seq.skip 1 // Remove CSV column header
      |> Seq.map parseLine
      |> Seq.map (fun sword -> sword.Level, sword)
      |> Map.ofSeq
    else
      failwith "Failed to find SwordList.csv file."

  let swords = loadSwordData ()

  let initialGameState = {
    GameState.CurrentSword = Map.find Constants.minSwordLevel swords
    Gold = Constants.initialGold
  }
