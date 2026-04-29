module BlacksmithClicker.UI

open BlacksmithClicker.State


let private displayWidth = 85
let private sectionDivider = String.replicate displayWidth "="
let private innerDivider = String.replicate displayWidth "-"

let private printRow (text: string) =
  let innerWidth = displayWidth - 6 // Except '||(space)(space)||'
  printfn "|| %-*s ||" innerWidth text

let displayState (state: GameState) (lastMessage: string) =
  // 1. Display NOTIFICATION section.
  printfn "%s" sectionDivider
  printRow (sprintf "!! NOTIFICATION: %s" lastMessage)

  // 2. Display STATUS section.
  printfn "%s" sectionDivider
  printRow "!! STATUS"
  printRow (sprintf "Current Sword: %s (Lv.%d)" state.CurrentSword.Name state.CurrentSword.Level)
  printRow (sprintf "Balance: %d Gold" state.Gold)
  
  printfn "%s" innerDivider
  let row1 = sprintf "Upgrade Cost: %d Gold  | Upgrade Probability: %d%%" state.CurrentSword.UpgradeCost state.CurrentSword.UpgradeProbability
  let row2 = sprintf "Selling Price: %d Gold | Permanent Purchase Cost: %d" state.CurrentSword.SellingPrice state.CurrentSword.PermanentPurchaseCost
  printRow row1
  printRow row2

  // 3. Display OPTIONS section.
  printfn "%s" sectionDivider
  printRow "!! OPTIONS"
  printRow "1. Upgrade Sword | 2. Sell Sword | 3. Purchase sword permanently"
  printRow "4. Initialize Game | 5. Quit Game"

  // 4. Display guide line for option selection.
  printfn "%s" innerDivider
  printf ">> Select option: "

let displayTerminationMessage (message: string) = 
  printfn "%s" sectionDivider
  printRow message
  printfn "%s" sectionDivider

let getCommand () =
  System.Console.ReadLine()
