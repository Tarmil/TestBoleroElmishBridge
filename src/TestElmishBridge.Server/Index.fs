module TestElmishBridge.Server.Index

open Bolero
open Bolero.Html
open Bolero.Server.Html
open TestElmishBridge

let page = doctypeHtml {
    head {
        meta { attr.charset "UTF-8" }
        meta { attr.name "viewport"; attr.content "width=device-width, initial-scale=1.0" }
        title { "Bolero Elmish.Bridge test" }
        ``base`` { attr.href "/" }
    }
    body {
        div { attr.id "main"; comp<Client.Main.MyApp> }
        boleroScript
    }
}
