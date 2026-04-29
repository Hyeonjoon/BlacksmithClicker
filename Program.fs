open BlacksmithClicker.State
open BlacksmithClicker.UI
open BlacksmithClicker.Logic
open System

[<EntryPoint>]
let main argv =
  let random = System.Random ()
  let rec gameLoop (state: GameState) (lastMessage: string) (lastMessageColor: ConsoleColor option) = 
    displayState state lastMessage lastMessageColor
    match getCommand (), state with
    | _, s when s.CurrentSword.Level >= Constants.maxSwordLevel ->
      displayTerminationMessage "Congratulations. You get the Lv.100 legend sword. It's over!" (Some ConsoleColor.Green)
      0
    | "1", _ -> 
      let result = upgrade state random
      gameLoop result.State result.Message result.Color
    | "2", _ ->
      let result = sell state
      gameLoop result.State result.Message result.Color
    | "3", _ ->
      let result = purchasePermanently state
      gameLoop result.State result.Message result.Color
    | "4", _ ->
      gameLoop StateGenerator.initialGameState "The game has been initialized." (Some ConsoleColor.Cyan)
    | "5", _ ->
      displayTerminationMessage "The game is being terminated." (Some ConsoleColor.Red)
      0
    | _, _ -> 
      gameLoop state "This is an unsupported option." (Some ConsoleColor.Red)
  gameLoop StateGenerator.initialGameState "Welcome to Blacksmith Clicker!" (Some ConsoleColor.Green)
