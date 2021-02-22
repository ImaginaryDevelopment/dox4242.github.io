module Client

open System

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fetch.Types
open Thoth.Fetch
open Fulma
open Thoth.Json

open Shared

let inline importAll<'t> x = Fable.Core.JsInterop.importAll<'t> x

// importAll "bootstrap.min.css"
importAll "pako"


// The model holds data that you want to keep track of while the application is running
// in this case, we are keeping track of a counter
// we mark it as optional, because initially it will not be available from the client
// the initial value will be requested from server
type Model = {
            RwsModel: Rws.Main.Model
            }
    with
        static member Empty = {
            RwsModel= Rws.Main.Model.Empty
        }

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
    | RwsMsg of Rws.Main.Msg

// let initialCounter () = Fetch.fetchAs<unit, Counter> "/api/init"

// defines the initial state and initial command (= side-effect) of the application
let init () : Model * Cmd<Msg> =
    let initialModel = Model.Empty
    initialModel, Cmd.none

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | RwsMsg msg ->
        let rm, cmd = Rws.Main.update msg model.RwsModel
        if rm <> model.RwsModel then
            {model with RwsModel = rm}, cmd |> Cmd.map Msg.RwsMsg
        else model, cmd |> Cmd.map Msg.RwsMsg
    // | _ -> model, Cmd.none

let navbar =
        Navbar.navbar [ Navbar.CustomClass "navbar-inverse navbar-fixed-top" ] [
            div [ Class "container-fluid" ] [
                div [Class "navbar-header"][
                    button  [   Type "button"; Class "navbar-toggle collapsed"
                                Data("toggle","collapse")
                                Data("target","#nav-collapsable-content")
                                AriaExpanded false
                            ][
                        span [Class "sr-only"][unbox "Toggle navigation"]
                        span [Class "icon-bar"][]
                        span [Class "icon-bar"][]
                        span [Class "icon-bar"][]
                    ]
                    a[Class"navbar-brand";Href "../../index.html"][unbox "Royal Advisory Board"]
                ]
                div [Class "collapse navbar-collapse"; Id "nav-collapsable-content"][
                    ul[Class "nav navbar-nav"][
                        yield li[][
                            a[Href "../../index.html"][unbox "Home"]
                        ]
                        let ddm title items =
                            let dda = a[Href "#";Class "dropdown-toggle"; Role "button"; AriaHasPopup true; AriaExpanded false]
                            let inline simpleLink (text:string) addr = a [Href addr][unbox text]
                            li[Class "dropdown"][
                                dda [
                                    unbox <| sprintf "%s " title
                                    span [Class "caret"][]
                                ]
                                ul [Class "dropdown-menu"](
                                    items
                                    |> List.map(fun (text,addr) -> li[][simpleLink text addr])

                                )
                            ]

                        yield ddm "Realm Grinder" [
                                "Realm Grinder","http://www.kongregate.com/games/divinegames/realm-grinder"
                                "Divine Games","http://www.divinegames.it/"
                                "G00f's Not a Wiki","http://musicfamily.org/realm/"
                                "Discord Chat - Realm Grinder and more","https://discord.gg/3YvX9hN"
                        ]
                        yield ddm "Useful Tools" [
                            "Realm Weather Service", "../rws/index.html"
                            "Lara Crypt", "../arch/index.html"
                            "Save Editor", "../edit/edit.html"
                        ]
                        yield ddm "Seasonal Tools" [
                            "Meggnetic Resonance Imager", "../mri/index.html"
                        ]
                    ]
                ]
            ]
        ]

let view (model : Model) (dispatch : Msg -> unit) =
    div [] [
        navbar

        Container.container [] [
            h2 [Id "title"; Class "page-header"][unbox "Realm Weather Service ";small [][unbox "Because Luck is a Fallacy"]]
            div [Class "panel panel-primary"][
                div[Class "panel-heading"][
                    unbox """Your court meteorologist looks at you expectantly. "Your Majesty, I will need the kingdom's records in order to produce a forecast," he says."""
                ]
                Rws.Main.saveInput model.RwsModel.SaveInput (RwsMsg >> dispatch)
            ]
            div [Class "alert alert-info"; HTMLAttr.Custom ("v-show","breathMessage != ''")][
                unbox "Dragon's breath forecast fixed, Maelstrom and Limited wish forecast added (Limited wish is a bit inaccurate due to issues)"
            ]
            div [Class "row"][div [Class "col-xs-12"][
                div [Id "buildings"][]
            ]]
            br[]
            Rws.Main.view model.RwsModel (RwsMsg >> dispatch)

        ]

        Footer.footer [ ]
                [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ unbox "footer content goes here"] ] ]


// Fable.Core.JsInterop.importAll "punycode"
// Fable.Core.JsInterop.importAll "popper"
// Fable.Core.JsInterop.importAll "bootstrap"
#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
