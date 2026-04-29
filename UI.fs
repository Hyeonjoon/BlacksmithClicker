module BlacksmithClicker.UI

open BlacksmithClicker.State
open System


let private displayWidth = 85
let private sectionDivider = String.replicate displayWidth "="
let private innerDivider = String.replicate displayWidth "-"

let private printRow (text: string) (color: ConsoleColor option) =
    let innerWidth = displayWidth - 6 // Except '||(space)(space)||'
    printf "|| "
    
    match color with
    | Some c -> 
        Console.ForegroundColor <- c
        printf "%-*s" innerWidth text
        Console.ResetColor()
    | None -> 
        printf "%-*s" innerWidth text
        
    printfn " ||"

let displayState (state: GameState) (lastMessage: string) (color: ConsoleColor option) =
  // 0. Clear UI.
  System.Console.Clear()

  // 1. Display NOTIFICATION section.
  printfn "%s" sectionDivider
  printRow "!! NOTIFICATION" (Some ConsoleColor.Yellow)
  printRow lastMessage color

  // 2. Display STATUS section.
  printfn "%s" sectionDivider
  printRow "!! STATUS" (Some ConsoleColor.Yellow)
  printRow (sprintf "Current Sword: %s (Lv.%d)" state.CurrentSword.Name state.CurrentSword.Level) None
  printRow (sprintf "Balance: %d Gold" state.Gold) None
  
  printfn "%s" innerDivider
  let row1 = sprintf "Upgrade Cost: %d Gold  | Upgrade Probability: %d%%" state.CurrentSword.UpgradeCost state.CurrentSword.UpgradeProbability
  let row2 = sprintf "Selling Price: %d Gold | Permanent Purchase Cost: %d" state.CurrentSword.SellingPrice state.CurrentSword.PermanentPurchaseCost
  printRow row1 None
  printRow row2 None

  // 3. Display OPTIONS section.
  printfn "%s" sectionDivider
  printRow "!! OPTIONS" (Some ConsoleColor.Yellow)
  printRow "1. Upgrade Sword | 2. Sell Sword | 3. Purchase sword permanently" None
  printRow "4. Initialize Game | 5. Quit Game" None

  // 4. Display guide line for option selection.
  printfn "%s" innerDivider
  printf ">> Select option: "

let displayTerminationMessage (message: string) (color: ConsoleColor option) = 
  System.Console.Clear()
  printfn "%s" sectionDivider
  printRow message color
  printfn "%s" sectionDivider

let getCommand () =
  System.Console.ReadLine()
