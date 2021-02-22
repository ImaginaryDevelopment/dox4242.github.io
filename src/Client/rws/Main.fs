module Rws.Main
open System

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fulma

open Shared
open Infrastructure

// don't open Fable.Core.JS it conflicts with Set<'t>
type Promise<'t> = Fable.Core.JS.Promise<'t>

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
                HighlightedBuildings:Building Set
                Save:DecodedSave option
            }
    with
        static member Empty = {
            SaveInput = String.Empty
            ProcessedSave = String.Empty
            SelectedTab = RwsTab.LightningStrike
            HighlightedBuildings = Set.empty
            Save = None
        }

type Msg =
    | TabSelect of RwsTab
    | InputChanged of string
    | EnterSave
    | CopySave
    | ClearSave

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | InputChanged v ->
        {model with SaveInput = v}, Cmd.none
    | ClearSave ->
        {model with SaveInput = String.Empty}, Cmd.none
    | CopySave ->
        model, Cmd.none
    | EnterSave ->
        if isValueString model.SaveInput then
            printfn "Save is: %s" model.SaveInput
            {model with Save = Helpers.Imports.Savecodec2.getSaveHandler.Decode(model.SaveInput) |> Some }, Cmd.none
        else
            model, Cmd.none
    | TabSelect _ -> model, Cmd.none

let copy text = // not using dispatch/command. might be bad idea
        try
            Browser.Navigator.navigator.clipboard
            |> function
                | Some clipboard -> clipboard.writeText text |> ignore<Promise<unit>>
                | None -> eprintfn "No clipboard found"
        with _ -> ()

let saveInput (saveText:string) dispatch =
    let inputId = "saveInput"
    div[Class "panel-body input-group panelSaveInput"][
        label[Id "saveInputLabel";Class "input-group-addon"; HtmlFor "saveInput"][
            a [ Data("toggle","popover");Data("trigger","hover");Data("placement","bottom")
                Data("content","'Export your save from Realm Grinder and paste it in this field to view a forecast of eggs that you will find in the Easter event.'>(?)" )
            ][
                unbox "Save (?)"
            ]
        ]
        input [Id inputId; Class "form-control"; Type "text"; Name "saveInput"; Value saveText;OnChange (fun e -> e.Value |> InputChanged |> dispatch)]
        div [Class "input-group-btn"][
            let btn id (title:string) cls (clickMsg:Msg) =
                button [Id id; Class <| "btn btn-" + cls; Type "Button";OnClick (fun _ -> dispatch clickMsg)][unbox title]
            yield btn "doReEnter" "Re-Enter save" "success" EnterSave
            yield button [  Id "doSaveCopy";Class <| "btn btn-info"; Type "Button"
                            OnClick (fun _ -> copy saveText)
                            OnPaste (fun _ -> dispatch EnterSave)
                        ][unbox "Copy save"]
            // yield btn "doSaveCopy" "Copy save" "info" CopySave
            yield btn "doSaveClear" "Clear save" "danger" ClearSave
        ]
    ]

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