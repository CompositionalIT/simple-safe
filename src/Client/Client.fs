module Client

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fulma
open Shared
open Thoth.Fetch

type Model =
    { CurrentValue : int }

type Msg =
    | Increment
    | Decrement
    | InitialCountLoaded of Counter

let initialCounter () = Fetch.get<_, Counter> "/api/init"

let init () =
    let initialModel = { CurrentValue = 0 }
    let loadCountCmd = Cmd.OfPromise.perform initialCounter () InitialCountLoaded
    initialModel, loadCountCmd

let update msg model =
    match msg with
    | Increment ->
        { CurrentValue = model.CurrentValue + 1 }, Cmd.none
    |  Decrement ->
        { CurrentValue = model.CurrentValue - 1 }, Cmd.none
    | InitialCountLoaded initialCount ->
        { CurrentValue = initialCount.Value }, Cmd.none

let safeComponents =
    span [ ] [
        str "Powered by: "
        span [ ] [
            // CSS value using Fable.React DSL
            a [ Href "https://github.com/SAFE-Stack/SAFE-template"; Style [ FontWeight "Bold" ] ] [ str "SAFE 2.0, " ]

            // CSS custom value
            a [ Href "https://saturnframework.github.io"; Style [ Custom("color", "brown") ] ] [ str "Saturn, " ]

            // Completely custom HTML attribute
            a [ Href "http://fable.io"; HTMLAttr.Custom("Style", "color:green") ] [ str "Fable, " ]
            a [ Href "https://elmish.github.io" ] [ str "Elmish, " ]
            a [ Href "https://fulma.github.io/Fulma" ] [ str "and Fulma." ];
        ]
    ]

let show = function
    | { CurrentValue = 0 } -> "Loading..."
    | { CurrentValue = counter } -> string counter

let makeButton txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]

let view model dispatch =
    div [] [
        Navbar.navbar [ Navbar.Color IsPrimary ] [
            Navbar.Item.div [ ] [
                Heading.h2 [ ] [
                    str "SAFE Template"
                ]
            ]
        ]

        Container.container [] [
            Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ] [
                Heading.h3 [] [ str ("Press buttons to manipulate counter: " + show model) ]
            ]
            Columns.columns [] [
                Column.column [] [
                    makeButton "-" (fun _ -> dispatch Decrement)
                ]
                Column.column [] [
                    makeButton "+" (fun _ -> dispatch Increment)
                ]
            ]

            Footer.footer [ ] [
                Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ] [
                    safeComponents
                ]
            ]
        ]
    ]

Program.mkProgram init update view
|> Program.withConsoleTrace
|> Program.withReactSynchronous "elmish-app"
|> Program.run
