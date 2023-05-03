module TestElmishBridge.Client.Main

open System
open Elmish
open Elmish.Bridge
open Bolero
open Bolero.Templating.Client
open TestElmishBridge.Shared

type Model =
    { text: string
      input: string }

type Message =
    | Input of string
    | ClickHello
    | FromServer of ClientMsg

let init _ = { text = ""; input = "Bridge user" }, Cmd.none

let update message model =
    match message with
    | Input x -> { model with input = x }, Cmd.none
    | ClickHello ->
        model, Cmd.bridgeSend (GetHello model.input) |> Cmd.map FromServer
    | FromServer (Hello text) ->
        { model with text = text }, Cmd.none

type View = Template<"view.html">

let view model dispatch =
    View()
        .Input(model.input, dispatch << Input)
        .Send(fun _ -> dispatch ClickHello)
        .Text(model.text)
        .Elt()

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        let bridgeEndpoint = UriBuilder(this.NavigationManager.Uri, Scheme = "ws", Path = WebSocket.endpoint).ToString()
        Program.mkProgram init update view
        |> Program.withBridgeConfig
               (Bridge.endpoint bridgeEndpoint
                |> Bridge.withMapping FromServer)
#if DEBUG
        |> Program.withHotReload
#endif
