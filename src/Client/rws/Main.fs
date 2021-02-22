module Rws.Main
open System

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fulma

open Shared



[<RequireQualifiedAccess>]
type RwsTab =
    | LightningStrike
    | Miracle
    | DragonsBreath
    | Maelstrom
    | LimitedWish
    | Catalyst
    | WorldlyDesires

type Model = {
                SaveInput:string // current input value
                ProcessedSave:string // last successfully parsed input
                SelectedTab:RwsTab
                HighlightedBuildings: Building Set
            }
    with
        static member Empty = {
            SaveInput = String.Empty
            ProcessedSave = String.Empty
            SelectedTab = RwsTab.LightningStrike
            HighlightedBuildings = Set.empty
        }

type Msg =
    | TabSelect of RwsTab
    | MaelstromMsg of Maelstrom.Msg

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | MaelstromMsg (Maelstrom.Msg.InputChanged v) ->
        {model with SaveInput = v}, Cmd.none
    | MaelstromMsg (Maelstrom.Msg.ClearSave) ->
        {model with SaveInput = String.Empty}, Cmd.none
    | MaelstromMsg (Maelstrom.Msg.CopySave) ->
        model, Cmd.none


let view (model:Model) dispatch =
    let onClick msg = OnClick(fun _ -> dispatch msg)
    div [Class "tab"][
        let tabBtn (title:string) (tab:RwsTab) =
            button [Class "tablinks"; onClick <| Msg.TabSelect tab][unbox title]
        yield tabBtn "Lightning Strike" RwsTab.LightningStrike
        yield tabBtn "Miracle" RwsTab.Miracle
        yield tabBtn "Dragon's Breath" RwsTab.DragonsBreath
        yield tabBtn "Maelstrom" RwsTab.Maelstrom
        yield tabBtn "Limited Wish" RwsTab.LimitedWish
        yield tabBtn "Catalyst" RwsTab.Catalyst
        yield tabBtn "Worldly Desires(DJC4)" RwsTab.WorldlyDesires
    ]