namespace LegendSword100.State

type Sword = {
    Level: uint
    Name: string
    UpgradeCost: uint
    SellingPrice: uint
}

type GameState = {
    CurrentSword: Sword
    Gold: uint
    SucceedGame: bool
}

module Constants =
  let minSwordLevel, maxSwordLevel = 1u, 100u
  let initialGold = 100000
