open BlacksmithClicker.State
open BlacksmithClicker.UI
open BlacksmithClicker.Logic
open System

[<EntryPoint>]
let main argv =
  let random = System.Random ()
  let rec gameLoop (state: GameState) (lastMessage: string) = 
    displayState state lastMessage
    match getCommand (), state with
    | _, s when s.CurrentSword.Level >= Constants.maxSwordLevel ->
      displayTerminationMessage "Congratulations. You get the Lv.100 legend sword. It's over!"
      0
    | "1", _ -> 
      let result = upgrade state random
      gameLoop result.State result.Message
    | "2", _ ->
      let result = sell state
      gameLoop result.State result.Message
    | "3", _ ->
      let result = purchasePermanently state
      gameLoop result.State result.Message
    | "4", _ ->
      gameLoop StateGenerator.initialGameState "The game has been initialized."
    | "5", _ ->
      displayTerminationMessage "The game is being terminated."
      0
    | _, _ -> 
      gameLoop state "This is an unsupported option."
  gameLoop StateGenerator.initialGameState "Welcome to Blacksmith Clicker!"
