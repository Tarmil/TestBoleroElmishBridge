module TestElmishBridge.Server.BridgeService

open Giraffe
open Elmish
open Elmish.Bridge
open TestElmishBridge.Shared

let init _ _ = (), Cmd.none

// let hub = ServerHub<unit, ServerMsg, ClientMsg>()

let update sendClient message model =
    match message with
    | GetHello name ->
        sendClient (Hello $"Hello there, {name}!")
        // hub.BroadcastClient(Hello $"Hello there, {name}!")
        model, Cmd.none

let server : HttpHandler =
    Bridge.mkServer WebSocket.endpoint init update
    |> Bridge.register GetHello
    // |> Bridge.withServerHub hub
    |> Bridge.run Giraffe.server
