namespace BlacksmithClicker.State

open System.IO

type Sword = {
  Level: uint
  Name: string
  UpgradeCost: uint64
  SellingPrice: uint64
  UpgradeProbability: uint
  PermanentPurchaseCost: uint64
}

type GameState = {
  DefaultSword: Sword
  CurrentSword: Sword
  Gold: uint64
}

type Result = {
  Message: string
  State: GameState
}

module Constants =
  let minSwordLevel, maxSwordLevel = 1u, 100u
  let initialGold = 100000UL

module StateGenerator = 
  let csvPath = "SwordList.csv"
  
  let private parseLine (line: string) =
    let row = line.Split(',')
    {
      Level = uint row.[0]
      Name = row.[1]
      UpgradeCost = uint64 row.[2]
      SellingPrice = uint64 row.[3]
      UpgradeProbability = uint row.[4]
      PermanentPurchaseCost = uint64 row.[5]
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

  let private getSword (level: uint): Sword = Map.find level swords

  let initialGameState = {
    GameState.DefaultSword = getSword Constants.minSwordLevel
    CurrentSword = getSword Constants.minSwordLevel
    Gold = Constants.initialGold
  }

  let upgradeSucceedState (previousState: GameState) = {
    GameState.DefaultSword = previousState.DefaultSword
    CurrentSword = getSword (previousState.CurrentSword.Level + 1u)
    Gold = previousState.Gold - previousState.CurrentSword.UpgradeCost
  }

  let upgradeFailedState (previousState: GameState) = {
    GameState.DefaultSword = previousState.DefaultSword
    CurrentSword = previousState.DefaultSword
    Gold = previousState.Gold - previousState.CurrentSword.UpgradeCost
  }

  let salesCompletedState (previousState: GameState) = {
    GameState.DefaultSword = previousState.DefaultSword
    CurrentSword = previousState.DefaultSword
    Gold = previousState.Gold + previousState.CurrentSword.SellingPrice
  }

  let permanetlyPurchasedState (previousState: GameState) = {
    GameState.DefaultSword = previousState.CurrentSword
    CurrentSword = previousState.CurrentSword
    Gold = previousState.Gold - previousState.CurrentSword.PermanentPurchaseCost
  }
