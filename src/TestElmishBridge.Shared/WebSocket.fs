namespace TestElmishBridge.Shared

type ServerMsg =
    | GetHello of name: string

type ClientMsg =
    | Hello of name: string

module WebSocket =
    let endpoint = "/socket"